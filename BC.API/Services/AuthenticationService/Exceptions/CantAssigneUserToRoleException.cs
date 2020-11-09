using System;

namespace BC.API.Services.AuthenticationService.Exceptions
{
  public class CantAssigneUserToRoleException : ApplicationException
  {
    public CantAssigneUserToRoleException(string message) : base(message)
    { }
  }
}
