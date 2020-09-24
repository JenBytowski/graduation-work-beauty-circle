using System.Threading.Tasks;

namespace BC.API.Services.AuthenticationService
{
  public interface ISMSClient
  {
    Task SendSMS(SMS sms);
  }
}
