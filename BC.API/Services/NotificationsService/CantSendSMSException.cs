using System;

namespace BC.API.Services.NotificationsService
{
  public class CantSendSMSException : ApplicationException
  {
    public CantSendSMSException(string message) : base(message)
    { }

    public CantSendSMSException(string message, Exception innerException) : base(message, innerException)
    { }
  }
}
