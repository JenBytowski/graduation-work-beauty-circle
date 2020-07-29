using BC.API.Services.AuthentificationService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BC.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    { 
        readonly AuthentificationService _authentificationService;

        public AuthentificationController(AuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        [HttpGet]
        [Route("authentificate-user")]
        public async Task<object> AuthentificateUser()
        {
            return Ok();
        }
    }
}