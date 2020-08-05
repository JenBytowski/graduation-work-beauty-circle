using BC.API.Infrastructure;
using BC.API.Services.SMSService;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BC.API.Services.Infrastructure
{
    public class RocketSMSClient : ISMSClient
    {
        readonly IConfiguration _configuration;

        public RocketSMSClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendSMS(SMS message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var client = new HttpClient();
            var credentials = _configuration.GetSection("RocketSMSCredentials").Get<RocketSMSOptions>();
            var sms = ParseSMStoRocketSMS(message);
            var response = await client.GetAsync($"https://api.rocketsms.by/simple/send?username={credentials.Username}&password={credentials.PasswordHash}&sender={sms.Sender}&phone={sms.Phone}&text={sms.Text}&priority={sms.Priority}");

            if (!response.IsSuccessStatusCode)
            {
                var exception = new CantSendSMSException("Cant send sms by RocketSMS");
                exception.Data.Add("sms", sms);
                exception.Data.Add("response string", await response.Content.ReadAsStringAsync());

                throw exception;
            }
        }

        private RocketSMS ParseSMStoRocketSMS(SMS sms)
        {
            return new RocketSMS
            {
                Sender = sms.Sender,
                Phone = sms.Phone,
                Text = sms.Text,
                Priority = true
            };
        }
    }
}
