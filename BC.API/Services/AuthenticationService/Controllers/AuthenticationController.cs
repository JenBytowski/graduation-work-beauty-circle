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
      return await _authenticationService.AuthenticateByVK(request.Code, request.RedirectUrl, request.Role);
    }

    [HttpPost]
    [Route("authenticate-by-instagram")]
    public async Task<AuthenticationResponse> AuthenticatebyInstagram(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticateByInstagram(request.Code, request.RedirectUrl, request.Role);
    }

    [HttpPost]
    [Route("authenticate-by-google")]
    public async Task<AuthenticationResponse> AuthenticatebyGoogle(AuthenticationCodeRequest request)
    {
      return await _authenticationService.AuthenticateByGoogle(request.Code, request.RedirectUrl, request.Role);
    }

    [HttpPost]
    [Route("authenticate-by-phone-step-1")]
    public async Task AuthenticatebyPhoneStep1(AuthenticationPhoneRequest request)
    {
      await _authenticationService.AuthenticateByPhoneStep1(request.Phone, request.Role);
    }

    [HttpPost]
    [Route("authenticate-by-phone-step-2")]
    public async Task<AuthenticationResponse> AuthenticatebyPhoneStep2(AuthenticatebyPhoneStep2Req req)
    {
      return await _authenticationService.AuthenticateByPhoneStep2(req);
    }
  }
}
