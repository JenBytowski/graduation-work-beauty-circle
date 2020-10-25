namespace BC.API.Services.AuthenticationService
{
  public class AuthenticationCodeRequest
  {
    public string Code { get; set; }

    public string RedirectUrl { get; set; }
    
    public string Role { get; set; }
  }
}
