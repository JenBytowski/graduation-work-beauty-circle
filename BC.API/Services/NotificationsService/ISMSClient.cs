using System.Threading.Tasks;

namespace BC.API.Services.NotificationsService
{
  public interface ISMSClient
  {
    Task SendSMS(SMS sms);
  }
}
