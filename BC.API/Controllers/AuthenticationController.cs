using BC.API.Services.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    public async Task<AuthenticationResponse> AuthentificatebyVK(string code)
    {
      return await _authenticationService.AuthenticatebyVK(code);
    }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate-by-instagram")]
        [ApiExplorerSettings(GroupName = "authentication")]
        public async Task<AuthenticationResponse> AuthentificatebyInstagram(string code)
        {
            return await _authenticationService.AuthenticatebyInstagram(code);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate-by-google")]
        [ApiExplorerSettings(GroupName = "authentication")]
        public async Task<AuthenticationResponse> AuthentificatebyGoogle(string code)
        {
            return await _authenticationService.AuthenticatebyGoogle(code);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("get-sms-authentication-code")]
        [ApiExplorerSettings(GroupName = "authentication")]
        public async Task GetSMSAuthentificationCode(string phone)
        {
            await _authenticationService.SendSMSAuthenticationCode(phone);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate-by-phone")]
        [ApiExplorerSettings(GroupName = "authentication")]
        public async Task<AuthenticationResponse> AuthentificatebyPhone(SMSCodeAuthenticationResponse model)
        {
            return await _authenticationService.AuthenticatebyPhone(model);
        }
    }
}
