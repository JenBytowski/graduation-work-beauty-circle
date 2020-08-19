using System.Text.Json.Serialization;

namespace BC.API.Services.AuthenticationService
{
  public class GoogleTokenResponse
  {
    [JsonPropertyName("id_token")]
    public string Token { get; set; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
  }
}
