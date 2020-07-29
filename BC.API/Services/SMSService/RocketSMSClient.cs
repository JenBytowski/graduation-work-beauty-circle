using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BC.API.Services.SMSService
{
    public class RocketSMSClient
    {
        readonly IConfiguration _configuration;

        public RocketSMSClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendSMS(SMS smsRequest)
        {
            var client = new HttpClient();
            var credentials = _configuration.GetSection("RocketSMSCredentials").Get<RocketSMSOptions>();
            var response = await client.GetAsync($"https://api.rocketsms.by/simple/send?username={credentials.Username}&password={credentials.PasswordHash}&sender={smsRequest.Sender}&phone={smsRequest.Phone}&text={smsRequest.Text}");

            return response.StatusCode != HttpStatusCode.OK ? false : true;
        }
    }
}
