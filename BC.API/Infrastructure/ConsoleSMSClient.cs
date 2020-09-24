using System;
using System.Threading.Tasks;
using BC.API.Services.NotificationsService;
using AuthenticationService = BC.API.Services.AuthenticationService;

namespace BC.API.Infrastructure
{
  public class ConsoleSMSClient : AuthenticationService.ISMSClient, ISMSClient
  {
    public async Task SendSMS(AuthenticationService.SMS sms)
    {
      Console.WriteLine(sms.Text);
    }

    public async Task SendSMS(SMS sms)
    {
      Console.WriteLine(sms.Text);
    }
  }
}
