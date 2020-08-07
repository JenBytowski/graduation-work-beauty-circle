using BC.API.Services.SMSService;

namespace BC.API.Infrastructure
{
  public class RocketSMS : SMS
  {
    public int TimeStamp { get; set; }

    public bool Priority { get; set; }
  }
}
