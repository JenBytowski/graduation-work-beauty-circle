using System;

namespace BC.API.Services.BookingService.Exceptions
{
  public class CantFoundWindowByTimeException : ApplicationException
  {
    public CantFoundWindowByTimeException(string message) : base(message)
    {}
  }
}
