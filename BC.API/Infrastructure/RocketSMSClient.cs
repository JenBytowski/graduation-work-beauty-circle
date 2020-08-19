using System;
using System.Net.Http;
using System.Threading.Tasks;
using BC.API.Infrastructure;
using BC.API.Services.SMSService;
using Microsoft.Extensions.Configuration;

namespace BC.API.Services.Infrastructure
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
      var credentials = _configuration.GetSection("RocketSMSCredentials").Get<RocketSMSOptions>();
      var response = await client.GetAsync($"https://api.rocketsms.by/simple/send?username={credentials.Username}&password={credentials.PasswordHash}&sender={sms.Sender}&phone={sms.Phone}&text={sms.Text}&priority=true");

      if (!response.IsSuccessStatusCode)
      {
        var exception = new CantSendSMSException("Cant send sms by RocketSMS");
        exception.Data.Add("sms", sms);
        exception.Data.Add("response string", await response.Content.ReadAsStringAsync());

        throw exception;
      }
    }

    public class RocketSMSOptions
    {
      public string Username { get; set; }

      public string PasswordHash { get; set; }
    }
  }
}
