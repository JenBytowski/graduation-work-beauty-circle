using System;

namespace BC.API.Services.AuthenticationService.Exceptions
{
    public class CantCreateUserException : ApplicationException
    {
        public CantCreateUserException(string message) : base(message)
        { }
    }
}
