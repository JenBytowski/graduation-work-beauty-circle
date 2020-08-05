using System;

namespace BC.API.Services.AuthenticationService.Exceptions
{
    public class InvalidAuthenticationCodeException : ApplicationException
    {
        public InvalidAuthenticationCodeException(string message) : base(message)
        { }
    }
}
