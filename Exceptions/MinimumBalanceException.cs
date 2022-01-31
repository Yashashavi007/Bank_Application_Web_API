using System;

namespace Technovert.Bankapp.Web.API.Exceptions
{
    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string message) : base(message)
        {
            
        }
    }
}
