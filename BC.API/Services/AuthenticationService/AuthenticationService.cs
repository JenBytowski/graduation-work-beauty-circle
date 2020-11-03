using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BC.API.Domain;
using BC.API.Events;
using BC.API.Infrastructure.Interfaces;
using BC.API.Services.AuthenticationService.Data;
using BC.API.Services.AuthenticationService.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StrongCode.Seedwork.EventBus;

namespace BC.API.Services.AuthenticationService
{
  public class AuthenticationService
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _rolesManager;
    private readonly IConfiguration _configuration;
    private readonly ISMSClient _smsClient;
    private readonly IEventBus _eventBus;

    public AuthenticationService(UserManager<User> userManager, ISMSClient smsClient,
      IConfiguration configuration, IEventBus eventBus, RoleManager<Role> rolesManager)
    {
      _userManager = userManager;
      _smsClient = smsClient;
      _configuration = configuration;
      _eventBus = eventBus;
      _rolesManager = rolesManager;
    }

    public async Task EnsureDataInitialized()
    {
      foreach (var role in UserRoles.AllRoles)
      {
        if (! await this._rolesManager.RoleExistsAsync(role))
        {
          await this._rolesManager.CreateAsync(new Role { Name = role} );
        }
      }
    }

    public async Task<AuthenticationResponse> AuthenticateByVK(string authCode, string redirectUrl, string role)
    {
      var client = new HttpClient();
      var authCredentials = _configuration.GetSection("VKCredentials").Get<SocialMediaAuthCredentials>();
      var requestURI = "https://oauth.vk.com/access_token" +
                       $"?client_id={authCredentials.ClientId}" +
                       $"&client_secret={authCredentials.ClientSecret}" +
                       $"&redirect_uri={redirectUrl}" +
                       $"&code={authCode}";

      var response = await client.PostAsync(requestURI, null);

      if (!response.IsSuccessStatusCode)
      {
        var exception = new AuthenticationException("Cant authenticate by vk. Cant get auth token.");
        exception.Data.Add("request uri", requestURI);
        exception.Data.Add("response string", await response.Content.ReadAsStringAsync());

        throw exception;
      }

      try
      {
        var parsedResponse =
          JsonSerializer.Deserialize<VKTokenResponse>(await response.Content.ReadAsStringAsync());

        var user = await _userManager.FindByLoginAsync("vk", parsedResponse.UserId.ToString()) ??
                   await CreateUserByVK(parsedResponse);

        await this.AddToRoleIfNotYet(user, UserRoles.ReturnIfValidOrDefault(role));
        
        var jwToken = await GenerateJWToken(user);

        return new AuthenticationResponse {Token = jwToken, Username = user.UserName};
      }
      catch (Exception ex)
      {
        throw new AuthenticationException("Cant authenticate by vk", ex);
      }
    }

    public async Task<AuthenticationResponse> AuthenticateByGoogle(string authCode, string redirectUrl, string role)
    {
      var client = new HttpClient();
      var googleCredentials = _configuration.GetSection("GoogleCredentials").Get<SocialMediaAuthCredentials>();

      var jsonRequest = JsonSerializer.Serialize(new
      {
        client_id = googleCredentials.ClientId,
        client_secret = googleCredentials.ClientSecret,
        code = authCode,
        redirect_uri = redirectUrl,
        grant_type = "authorization_code"
      });
      var response = await client.PostAsync("https://oauth2.googleapis.com/token",
        new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

      if (!response.IsSuccessStatusCode)
      {
        var exception = new AuthenticationException("Cant authenticate by google. Cant get auth token.");
        exception.Data.Add("request body", jsonRequest);
        exception.Data.Add("response string", await response.Content.ReadAsStringAsync());

        throw exception;
      }

      try
      {
        var jsonResponse =
          JsonSerializer.Deserialize<GoogleTokenResponse>(await response.Content.ReadAsStringAsync());
        var encodedToken = new JwtSecurityTokenHandler().ReadJwtToken(jsonResponse.Token);

        var userid = encodedToken.Claims.First(clm => clm.Type == "sub").Value;
        var user = await _userManager.FindByLoginAsync("google", userid) ??
                   await CreateUserByGoogle(encodedToken);
        await this.AddToRoleIfNotYet(user, UserRoles.ReturnIfValidOrDefault(role));
        
        var jwToken = await GenerateJWToken(user);

        return new AuthenticationResponse {Token = jwToken, Username = user.UserName};
      }
      catch (Exception ex)
      {
        throw new AuthenticationException("Cant authenticate by google", ex);
      }
    }

    public async Task<AuthenticationResponse> AuthenticateByInstagram(string authCode, string redirectUrl, string role)
    {
      var httpClient = new HttpClient();
      var credentials = _configuration.GetSection("InstagramCredentials").Get<SocialMediaAuthCredentials>();

      var requestBody = new List<KeyValuePair<string, string>>
      {
        new KeyValuePair<string, string>("client_id", credentials.ClientId),
        new KeyValuePair<string, string>("client_secret", credentials.ClientSecret),
        new KeyValuePair<string, string>("grant_type", "authorization_code"),
        new KeyValuePair<string, string>("redirect_uri", redirectUrl),
        new KeyValuePair<string, string>("code", authCode)
      };

      var content = new FormUrlEncodedContent(requestBody);
      content.Headers.Clear();
      content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

      var response = await httpClient.PostAsync("https://api.instagram.com/oauth/access_token", content);

      if (!response.IsSuccessStatusCode)
      {
        var exception = new AuthenticationException("Cant authenticate by instagram. Cant get auth token.");
        exception.Data.Add("request body", requestBody);
        exception.Data.Add("response string", await response.Content.ReadAsStringAsync());

        throw exception;
      }

      try
      {
        var parsedResponse =
          JsonSerializer.Deserialize<InstagramTokenResponse>(await response.Content.ReadAsStringAsync());

        var user = await _userManager.FindByLoginAsync("instagram", parsedResponse.UserId.ToString()) ??
                   await CreateUserByInstagram(parsedResponse);
        await this.AddToRoleIfNotYet(user, UserRoles.ReturnIfValidOrDefault(role));

        return new AuthenticationResponse {Token = await GenerateJWToken(user), Username = user.UserName};
      }
      catch (Exception ex)
      {
        throw new AuthenticationException("Cant authenticate by instagram", ex);
      }
    }

    public async Task AuthenticateByPhoneStep1(string phone, string role)
    {
      try
      {
        var user = _userManager.Users.SingleOrDefault(usr => usr.PhoneNumber == phone) ?? await CreateUserByPhone(phone); 
        await this.AddToRoleIfNotYet(user, UserRoles.ReturnIfValidOrDefault(role));
        
        var smsCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone");
        await SendSMS(phone, $"Ваш код: {smsCode}. Расскажите его всем друзьям и покажите его соседу");
      }
      catch (Exception ex)
      {
        throw new AuthenticationException("Cant authenticate by instagram", ex);
      }
    }

    public async Task<AuthenticationResponse> AuthenticateByPhoneStep2(AuthenticatebyPhoneStep2Req req)
    {
      try
      {
        var user = _userManager.Users.Single(usr => usr.PhoneNumber == req.Phone);
        var checkCodeResult = await _userManager.VerifyTwoFactorTokenAsync(user, "Phone", req.Code);

        if (!checkCodeResult)
        {
          throw new InvalidAuthenticationCodeException("Invalid authentication code");
        }

        return new AuthenticationResponse {Token = await GenerateJWToken(user), Username = user.UserName};
      }
      catch (Exception ex)
      {
        throw new AuthenticationException("Cant authenticate by phone", ex);
      }
    }

    private async Task AddToRoleIfNotYet(User user, string role)
    {
      if (await this._userManager.IsInRoleAsync(user, role))
      {
        return;
      }

      var addRoleResult = await _userManager.AddToRoleAsync(user, role);

      if (!addRoleResult.Succeeded)
      {
        throw new CantCreateUserException(addRoleResult.Errors.First().Description);
      }
      
      this._eventBus.Publish(new UserAssignedToRoleEvent { UserId = user.Id, UserName = user.UserName, Role = role});
    }

    private async Task SendSMS(string phone, string text)
    {
      try
      {
        var sms = new SMS {Sender = "Beauty-Test", Phone = phone, Text = text};

        await _smsClient.SendSMS(sms);
      }
      catch (CantSendSMSException ex)
      {
        throw new CantSendSMSException("Cant send sms with authentication code", ex);
      }
    }

    private async Task<User> CreateUserByVK(VKTokenResponse response)
    {
      var username = Guid.NewGuid().ToString();
      var createUserResult = await _userManager.CreateAsync(new User
      {
        UserName = username, Email = response.Email, EmailConfirmed = true
      });

      if (!createUserResult.Succeeded)
      {
        throw new CantCreateUserException(createUserResult.Errors.First().Description);
      }

      var user = await _userManager.FindByNameAsync(username);

      var addLoginResult = await _userManager.AddLoginAsync(user,
        new UserLoginInfo("vk", response.UserId.ToString(), "vk"));

      if (!addLoginResult.Succeeded)
      {
        throw new CantCreateUserException(addLoginResult.Errors.First().Description);
      }
      
      this._eventBus.Publish(new UserCreatedEvent {UserId = user.Id, UserName = user.UserName});

      return user;
    }

    private async Task<User> CreateUserByGoogle(JwtSecurityToken userInfo)
    {
      var username = Guid.NewGuid().ToString();
      var userId = userInfo.Claims.First(clm => clm.Type == "sub").Value;
      var userEmail = userInfo.Claims.First(clm => clm.Type == "email").Value;

      var createUserResult = await _userManager.CreateAsync(new User
      {
        UserName = username,
        Email = userEmail,
        EmailConfirmed = true
      });

      if (!createUserResult.Succeeded)
      {
        throw new CantCreateUserException(createUserResult.Errors.First().Description);
      }

      var user = await _userManager.FindByNameAsync(username);
      
      var addLoginResult = await _userManager.AddLoginAsync(user,
        new UserLoginInfo("google", userId, "google"));

      if (!addLoginResult.Succeeded)
      {
        throw new CantCreateUserException(addLoginResult.Errors.First().Description);
      }
      
      this._eventBus.Publish(new UserCreatedEvent {UserId = user.Id, UserName = user.UserName});

      return user;
    }

    private async Task<User> CreateUserByInstagram(InstagramTokenResponse response)
    {
      var username = Guid.NewGuid().ToString();
      var createUserResult = await _userManager.CreateAsync(new User {UserName = username});

      if (!createUserResult.Succeeded)
      {
        throw new CantCreateUserException(createUserResult.Errors.First().Description);
      }

      var user = await _userManager.FindByNameAsync(username);

      var addLoginResult = await _userManager.AddLoginAsync(user,
        new UserLoginInfo("instagram", response.UserId.ToString(), "instagram"));

      if (!addLoginResult.Succeeded)
      {
        throw new CantCreateUserException(addLoginResult.Errors.First().Description);
      }
      
      this._eventBus.Publish(new UserCreatedEvent {UserId = user.Id, UserName = user.UserName});

      return user;
    }

    private async Task<User> CreateUserByPhone(string phone)
    {
      var username = Guid.NewGuid().ToString();
      var createUserResult =
        await _userManager.CreateAsync(new User {UserName = username, PhoneNumber = phone, PhoneNumberConfirmed = true});

      if (!createUserResult.Succeeded)
      {
        throw new CantCreateUserException(createUserResult.Errors.First().Description);
      }

      var user = await _userManager.FindByNameAsync(username);

      this._eventBus.Publish(new UserCreatedEvent {UserId = user.Id, UserName = user.UserName});

      return user;
    }

    private async Task<string> GenerateJWToken(User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      var claimIdentity = (await _userManager.GetClaimsAsync(user)).Any()
        ? new List<Claim>
        {
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim("UserName", user.UserName)
        }
        : await _userManager.GetClaimsAsync(user);

      var tokenOptions = _configuration.GetSection("JWTokenOptions").Get<TokenOptions>();

      var token = new JwtSecurityToken(
        issuer: tokenOptions.Issuer,
        audience: tokenOptions.Audience,
        claims: claimIdentity,
        notBefore: DateTime.Now,
        expires: DateTime.Now.AddMinutes(100),
        signingCredentials: new SigningCredentials(
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)), SecurityAlgorithms.HmacSha256)
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
