using Swashbuckle.AspNetCore.Annotations;

namespace BC.API.Services.AuthenticationService
{
    public class AuthenticationResponse
    {
        [SwaggerSchema("Access API token")]
        public string Token { get; set; }

        [SwaggerSchema("Unique username")]
        public string Username { get; set; }
    }
}
