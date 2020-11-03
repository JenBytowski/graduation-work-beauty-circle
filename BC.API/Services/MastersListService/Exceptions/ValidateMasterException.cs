using System;

namespace BC.API.Services.MastersListService.Exceptions
{
  public class ValidateMasterException : ApplicationException

  {
  public ValidateMasterException(string message) : base(message)
  {
  }

  public ValidateMasterException(string message, Exception innerException) : base(message, innerException)
  {
  }
  }
}
