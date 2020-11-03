using System;

namespace BC.API.Services.MastersListService.Exceptions
{
  public class CantUpdateMasterException : ApplicationException
  {
    public CantUpdateMasterException(string message) : base(message)
    {
    }

    public CantUpdateMasterException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
