using BC.API.Services.SMSService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BC.API.Services.AuthentificationService
{
    public class AuthentificationService
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly IConfiguration _configuration;
        readonly RocketSMSClient _smsClient;

        public AuthentificationService(UserManager<IdentityUser> userManager, RocketSMSClient smsClient, IConfiguration configuration)
        {
            _userManager = userManager;
            _smsClient = smsClient;
            _configuration = configuration;
        }

        public async Task<AuthentificationResponse> AuthentificatebyVK(string authCode)
        {
            var client = new HttpClient();
            var authCredentials = _configuration.GetSection("VKCredentials").Get<SocialMediaAuthCredentials>();

            var response = await client.PostAsync("https://oauth.vk.com/access_token" +
                                                  $"?client_id={authCredentials.ClientId}" +
                                                  $"&client_secret={authCredentials.ClientSecret}" +
                                                  $"&redirect_uri={authCredentials.RedirectUrl}" +
                                                  $"&code={authCode}",
                null);

            var parsedResponse = JsonSerializer.Deserialize<VKAuthentificationResponse>(await response.Content.ReadAsStringAsync());

            var user = await _userManager.FindByEmailAsync(parsedResponse.Email) ?? await CreateUserbyVK(parsedResponse);
            var jwToken = await GenerateJWToken(user);

            return new AuthentificationResponse { Token = jwToken, Username = user.UserName };
        }

        public async Task<AuthentificationResponse> AuthentificatebyGoogle(string authCode)
        {
            var client = new HttpClient();
            var googleCredentials = _configuration.GetSection("GoogleCredentials").Get<SocialMediaAuthCredentials>();

            var jsonRequest = JsonSerializer.Serialize(new
            {
                client_id = googleCredentials.ClientId,
                client_secret = googleCredentials.ClientSecret,
                code = authCode,
                redirect_uri = "https://localhost:44396/UI/MainPage",
                grant_type = "authorization_code"
            });
            var response = await client.PostAsync("https://oauth2.googleapis.com/token", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            var jsonResponse = JsonSerializer.Deserialize<GoogleAuthentificationResponse>(await response.Content.ReadAsStringAsync());
            var encodedToken = new JwtSecurityTokenHandler().ReadJwtToken(jsonResponse.Token);
           
            var userEmail = encodedToken.Claims.First(clm => clm.Type == "email").Value;
            var user = await _userManager.FindByEmailAsync(userEmail) ?? await CreateUserbyGoogle(encodedToken);
            var jwToken = await GenerateJWToken(user);

            return new AuthentificationResponse { Token = jwToken, Username = user.UserName };
        }

        public async Task<AuthentificationResponse> AuthentificatebyInstagram(string authCode)
        {
            var httpClient = new HttpClient();
            var credentials = _configuration.GetSection("InstagramCredentials").Get<SocialMediaAuthCredentials>();

            var requestBody = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", credentials.ClientId),
                new KeyValuePair<string, string>("client_secret", credentials.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", credentials.RedirectUrl),
                new KeyValuePair<string, string>("code", authCode)
            };

            var content = new FormUrlEncodedContent(requestBody);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = await httpClient.PostAsync("https://api.instagram.com/oauth/access_token", content);
            var parsedResponse = JsonSerializer.Deserialize<InstagramAuthentificationResponse>(await response.Content.ReadAsStringAsync());

            var user = await _userManager.FindByIdAsync(parsedResponse.UserId.ToString()) ??
                       await CreateUserbyInstagram(parsedResponse);

            return new AuthentificationResponse { Token = await GenerateJWToken(user), Username = user.UserName };
        }

        public async Task<bool> GetSMSAuthentificationCode(string phone)
        {
            var user = _userManager.Users.FirstOrDefault(usr => usr.PhoneNumber == phone) ?? await CreateUserbyPhone(phone);
            var smsCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone");

            var sms = new SMS
            {
                Sender = "Beauty-Test",
                Phone = user.PhoneNumber,
                Text = $"Ваш код: {smsCode}. Расскажите его всем друзьям и покажите его соседу"
            };

            var result = await _smsClient.SendSMS(sms);

            return !result 
                ? throw new Exception() 
                : true;
        }

        public async Task<AuthentificationResponse> AuthentificatebyPhone(SMSCodeAuthentificationResponse model)
        {
            var user = _userManager.Users.First(usr => usr.PhoneNumber == model.Phone);
            var checkCodeResult = await _userManager.VerifyTwoFactorTokenAsync(user, "Phone", model.Code);

            if (!checkCodeResult)
            {
                return null;
            }

            return new AuthentificationResponse { Token = await GenerateJWToken(user), Username = user.UserName };
        }

        private async Task<IdentityUser> CreateUserbyVK(VKAuthentificationResponse response)
        {
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = response.Email,
                Email = response.Email,
                EmailConfirmed = true
            });

            return result.Succeeded 
                ? await _userManager.FindByEmailAsync(response.Email) 
                : throw new Exception(result.Errors.First().Description);
        }

        private async Task<IdentityUser> CreateUserbyGoogle(JwtSecurityToken userInfo)
        {
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = userInfo.Claims.First(clm => clm.Type == "sub").Value,
                Email = userInfo.Claims.First(clm => clm.Type == "email").Value,
                EmailConfirmed = true
            });

            return result.Succeeded
                ? await _userManager.FindByEmailAsync(userInfo.Claims.First(clm => clm.Type == "email").Value)
                : throw new Exception(result.Errors.First().Description);
        }

        private async Task<IdentityUser> CreateUserbyInstagram(InstagramAuthentificationResponse response)
        {
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = response.UserId.ToString(),
            });

            return result.Succeeded
                ? await _userManager.FindByIdAsync(response.UserId.ToString())
                : throw new Exception(result.Errors.First().Description);
        }

        private async Task<IdentityUser> CreateUserbyPhone(string phone)
        {
            var result = await _userManager.CreateAsync(new IdentityUser { UserName = phone, PhoneNumber = phone });

            return result.Succeeded 
                ? _userManager.Users.First(usr => usr.PhoneNumber == phone) 
                : throw new Exception(result.Errors.First().Description);
        }

        private async Task<string> GenerateJWToken(IdentityUser user)
        {
            var claimIdentity = (await _userManager.GetClaimsAsync(user)).Any() ?
                new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id), new Claim("UserName", user.UserName) } :
                await _userManager.GetClaimsAsync(user);
            var tokenOptions = _configuration.GetSection("JWTokenOptions").Get<TokenOptions>();
            var token = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claimIdentity,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
