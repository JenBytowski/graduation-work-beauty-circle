using System.Text.Json.Serialization;

namespace BC.API.Services.AuthentificationService
{
    public class VKAuthentificationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
	}

    public class GoogleAuthentificationResponse
    {
        [JsonPropertyName("id_token")]
        public string Token { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }

    public class InstagramAuthentificationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("user_id")]
        public double UserId { get; set; }
    }

    public class SMSCodeAuthentificationResponse
    {
        public string Phone { get; set; }

        public string Code { get; set; }
    }
}
