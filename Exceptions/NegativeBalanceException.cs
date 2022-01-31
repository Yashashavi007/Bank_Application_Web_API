using System;

namespace Technovert.Bankapp.Web.API.Exceptions
{
    public class NegativeBalanceException : Exception
    {
        public NegativeBalanceException(string message) : base(message)
        {
        }
    }
}
