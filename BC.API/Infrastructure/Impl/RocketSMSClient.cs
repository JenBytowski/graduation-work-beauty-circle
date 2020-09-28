using System;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BC.API.Infrastructure.Impl
{
  public class RocketSMSClient : ISMSClient
  {
    readonly IConfiguration _configuration;

    public RocketSMSClient(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task SendSMS(SMS sms)
    {
      if (sms == null)
      {
        throw new ArgumentNullException(nameof(sms));
      }

      var client = new HttpClient();
      var credentials = _configuration.GetSection("RocketSMSCredentials").Get<RocketSmsOptions>();
      var response = await client.GetAsync($"https://api.rocketsms.by/simple/send?username={credentials.Username}&password={credentials.PasswordHash}&sender={sms.Sender}&phone={sms.Phone}&text={sms.Text}&priority=true");

      if (!response.IsSuccessStatusCode)
      {
        var exception = new CantSendSMSException("Cant send sms by RocketSMS");
        exception.Data.Add("sms", sms);
        exception.Data.Add("response string", await response.Content.ReadAsStringAsync());

        throw exception;
      }
    }
  }
}
