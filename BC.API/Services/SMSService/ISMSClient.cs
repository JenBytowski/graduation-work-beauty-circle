using System.Threading.Tasks;

namespace BC.API.Services.SMSService
{
    public interface ISMSClient
    {
        Task<bool> SendSMS(SMS message);
    }
}
