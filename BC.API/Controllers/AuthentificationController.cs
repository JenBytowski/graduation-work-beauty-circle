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
        readonly AuthentificationService _authenticationService;

        public AuthenticationController(AuthentificationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-vk")]
        public async Task<AuthentificationResponse> AuthentificatebyVK(string code)
        {
            return await _authenticationService.AuthentificatebyVK(code);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-instagram")]
        public async Task<AuthentificationResponse> AuthentificatebyInstagram(string code)
        {
            return await _authenticationService.AuthentificatebyInstagram(code);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-google")]
        public async Task<AuthentificationResponse> AuthentificatebyGoogle(string code)
        {
            return await _authenticationService.AuthentificatebyGoogle(code);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("get-sms-authentication-code")]
        public async Task GetSMSAuthentificationCode(string phone)
        {
            var result = await _authenticationService.GetSMSAuthentificationCode(phone);

            if (!result)
            {
                BadRequest();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("authenticate-by-phone")]
        public async Task<AuthentificationResponse> AuthentificatebyPhone(SMSCodeAuthentificationResponse model)
        {
            return await _authenticationService.AuthentificatebyPhone(model);
        }
    }
}