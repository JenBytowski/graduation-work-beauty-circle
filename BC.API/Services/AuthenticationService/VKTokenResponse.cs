using System.Text.Json.Serialization;

namespace BC.API.Services.AuthenticationService
{
  public class VKTokenResponse
  {
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
  }
}
