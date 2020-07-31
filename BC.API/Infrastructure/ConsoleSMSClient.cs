using BC.API.Services.SMSService;
using System;
using System.Threading.Tasks;

namespace BC.API.Infrastructure
{
    public class ConsoleSMSClient : ISMSClient
    {
        public async Task<bool> SendSMS(SMS message)
        {
            throw new NotImplementedException();
        }
    }
}
