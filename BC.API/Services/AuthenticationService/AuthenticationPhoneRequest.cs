namespace BC.API.Services.AuthenticationService
{
  public class AuthenticationPhoneRequest
  {
    public string Phone { get; set; }
    public string Role { get; set; }
  }
  
  public class AuthenticatebyPhoneStep2Req
  {
    public string Phone { get; set; }

    public string Code { get; set; }
  }
}
