using System;

namespace BC.API.Services.MastersListService.Exceptions
{
  public class CantFindMasterException : ApplicationException
  {
    public CantFindMasterException(string message) : base(message)
    {
    }

    public CantFindMasterException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
