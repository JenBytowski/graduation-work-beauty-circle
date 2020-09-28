using BC.API.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BC.API.Controllers
{
  [Route("authentication")]
  [ApiExplorerSettings(GroupName = "authentication")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("authenticate-by-vk")]
    public async Task<AuthenticationResponse> AuthenticatebyVK(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticatebyVK(request.Code, request.RedirectUrl);
    }

    [HttpPost]
    [Route("authenticate-by-instagram")]
    public async Task<AuthenticationResponse> AuthenticatebyInstagram(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticatebyInstagram(request.Code, request.RedirectUrl);
    }

    [HttpPost]
    [Route("authenticate-by-google")]
    public async Task<AuthenticationResponse> AuthenticatebyGoogle(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticatebyGoogle(request.Code, request.RedirectUrl);
    }

    [HttpPost]
    [Route("get-sms-authentication-code")]
    public async Task GetSMSAuthenticationCode(AuthenticationPhoneRequest request)
    {
      await _authenticationService.SendSMSAuthenticationCode(request.Phone);
    }

    [HttpPost]
    [Route("authenticate-by-phone")]
    public async Task<AuthenticationResponse> AuthenticatebyPhone(SMSCodeAuthenticationResponse model)
    {
      return await _authenticationService.AuthenticatebyPhone(model);
    }
  }
}
