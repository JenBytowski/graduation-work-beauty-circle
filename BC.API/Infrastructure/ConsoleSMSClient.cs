using System;
using System.Threading.Tasks;
using BC.API.Services.SMSService;

namespace BC.API.Infrastructure
{
  public class ConsoleSMSClient : ISMSClient
  {
    public async Task SendSMS(SMS sms)
    {
      throw new NotImplementedException();
    }
  }
}
