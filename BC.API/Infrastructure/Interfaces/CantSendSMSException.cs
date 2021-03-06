﻿using System;

namespace BC.API.Infrastructure.Interfaces
{
  public class CantSendSMSException : ApplicationException
  {
    public CantSendSMSException(string message) : base(message)
    { }

    public CantSendSMSException(string message, Exception innerException) : base(message, innerException)
    { }
  }
}
