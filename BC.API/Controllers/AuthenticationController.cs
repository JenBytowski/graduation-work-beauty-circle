using BC.API.Services.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BC.API.Infrastructure;

namespace BC.API.Controllers
{
  [Route("authentication")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "authentication")]
    [Route("authenticate-by-vk")]
    public async Task<AuthenticationResponse> AuthenticatebyVK(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticatebyVK(request.Code, request.RedirectUrl);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("authenticate-by-instagram")]
    [ApiExplorerSettings(GroupName = "authentication")]
    public async Task<AuthenticationResponse> AuthenticatebyInstagram(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticatebyInstagram(request.Code, request.RedirectUrl);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("authenticate-by-google")]
    [ApiExplorerSettings(GroupName = "authentication")]
    public async Task<AuthenticationResponse> AuthenticatebyGoogle(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticatebyGoogle(request.Code, request.RedirectUrl);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("get-sms-authentication-code")]
    [ApiExplorerSettings(GroupName = "authentication")]
    public async Task GetSMSAuthenticationCode(AuthenticationPhoneRequest request)
    {
      await _authenticationService.SendSMSAuthenticationCode(request.Phone);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("authenticate-by-phone")]
    [ApiExplorerSettings(GroupName = "authentication")]
    public async Task<AuthenticationResponse> AuthenticatebyPhone(SMSCodeAuthenticationResponse model)
    {
      return await _authenticationService.AuthenticatebyPhone(model);
    }
  }
}
