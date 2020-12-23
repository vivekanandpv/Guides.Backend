using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guides.Backend.Exceptions.Auth
{
    public class AuthException : Exception
    {

    }
    
    public class GeneralAuthException : AuthException
    {
    }
    
    public class LoginFailedException : AuthException
    {
    }
    
    public class RegistrationFailedException : AuthException
    {
    }
    
    public class DomainValidationException : AuthException
    {
    }
    
    public class AdminActionNotSupportedException : AuthException
    {
    }
    
    public class UserActionNotSupportedException : AuthException
    {
    }
    
    public class UserActionPreventedException : AuthException
    {
    }
}
