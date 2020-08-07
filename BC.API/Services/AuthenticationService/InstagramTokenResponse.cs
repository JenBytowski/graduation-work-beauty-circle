using System.Text.Json.Serialization;

namespace BC.API.Services.AuthenticationService
{
  public class InstagramTokenResponse
  {
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("user_id")]
    public double UserId { get; set; }
  }
}
