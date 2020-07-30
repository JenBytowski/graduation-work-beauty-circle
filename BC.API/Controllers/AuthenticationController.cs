using BC.API.Services.AuthentificationService;
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

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-vk")]
        public async Task<AuthenticationResponse> AuthentificatebyVK(string code)
        {
            return await _authenticationService.AuthenticatebyVK(code);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-instagram")]
        public async Task<AuthenticationResponse> AuthentificatebyInstagram(string code)
        {
            return await _authenticationService.AuthenticatebyInstagram(code);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-google")]
        public async Task<AuthenticationResponse> AuthentificatebyGoogle(string code)
        {
            return await _authenticationService.AuthenticatebyGoogle(code);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("get-sms-authentication-code")]
        public async Task GetSMSAuthentificationCode(string phone)
        {
            var result = await _authenticationService.GetSMSAuthenticationCode(phone);

            if (!result)
            {
                BadRequest();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-phone")]
        public async Task<AuthenticationResponse> AuthentificatebyPhone(SMSCodeAuthenticationResponse model)
        {
            return await _authenticationService.AuthenticatebyPhone(model);
        }
    }
}