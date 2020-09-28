using System.Threading.Tasks;

namespace BC.API.Infrastructure.Interfaces
{
  public interface ISMSClient
  {
    Task SendSMS(SMS sms);
  }
}
