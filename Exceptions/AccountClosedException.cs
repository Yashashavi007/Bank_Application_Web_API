using System;

namespace Technovert.Bankapp.Web.API.Exceptions
{
    public class AccountClosedException : Exception
    {
        public AccountClosedException(string message) : base(message)
        {
        }
    }
}
