using System.Threading.Tasks;
using BC.API.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;

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
    [Route("authenticate-by-phone-step-1")]
    public async Task AuthenticatebyPhoneStep1(AuthenticationPhoneRequest request)
    {
      await _authenticationService.AuthenticatebyPhoneStep1(request.Phone);
    }

    [HttpPost]
    [Route("authenticate-by-phone-step-2")]
    public async Task<AuthenticationResponse> AuthenticatebyPhoneStep2(AuthenticatebyPhoneStep2Req req)
    {
      return await _authenticationService.AuthenticatebyPhoneStep2(req);
    }
  }
}
