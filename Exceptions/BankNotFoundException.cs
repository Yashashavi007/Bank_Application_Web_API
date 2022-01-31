using System;

namespace Technovert.Bankapp.Web.API.Exceptions
{
    public class BankNotFoundException : Exception
    {
        public BankNotFoundException(string message) : base(message)
        {
        }
    }
}
