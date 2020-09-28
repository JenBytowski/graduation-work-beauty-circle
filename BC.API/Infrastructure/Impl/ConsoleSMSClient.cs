using System;
using System.Threading.Tasks;
using BC.API.Infrastructure.Interfaces;

namespace BC.API.Infrastructure.Impl
{
  public class ConsoleSMSClient : ISMSClient
  {
    public async Task SendSMS(SMS sms)
    {
      Console.WriteLine(sms.Text);
    }
  }
}
