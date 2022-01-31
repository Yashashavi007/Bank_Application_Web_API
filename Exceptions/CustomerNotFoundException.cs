using System;

namespace Technovert.Bankapp.Web.API.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message)
        {
        }
    }
}
