using System;

namespace BC.API.Services.BookingService.Exceptions
{
  public class BookingException : ApplicationException
  {
    public BookingException(string message) : base(message)
    {
    }

    public BookingException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
