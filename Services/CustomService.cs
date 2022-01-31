using System;
using System.Collections.Generic;
using System.Linq;
using Technovert.Bankapp.Web.API.Models;
using static Technovert.Bankapp.Web.API.Enums.CustomerEnum;

namespace Technovert.Bankapp.Web.API.Services
{
    public class CustomService : ICustomService
    {
        private readonly BankDbContext _context;
        public CustomService(BankDbContext context)
        {
            _context = context;
        }

        // Bank Transfer Rates
        private static float intraBankRTGS = 0;
        private static float intraBankIMPS = 5;
        private static float interBankRTGS = 2;
        private static float interBankIMPS = 6;
        public float IntraBankRTGS
        {
            get { return intraBankRTGS; }
            set { intraBankRTGS = value; }
        }
        public float IntraBankIMPS
        {
            get { return intraBankIMPS; }
            set { intraBankIMPS = value; }
        }
        public float InterBankRTGS
        {
            get { return interBankRTGS; }
            set { interBankRTGS = value; }
        }
        public float InterBankIMPS
        {
            get { return interBankIMPS; }
            set { interBankIMPS = value; }
        }

        // GENERATION
        public string GenerateAccountID(string accountHolderName)
        {
            return (accountHolderName.Substring(0, 4) + DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        public string GenerateEmployeeID(string employeeName)
        {
            var random = new Random();
            return ("EMP" + employeeName.Substring(0, 4) + random.Next(10000, 99999).ToString());
        }

        public string GenerateTransactionID(string bankIFSCCode, string accountId)
        {
            return ("TXN" + bankIFSCCode + accountId + DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss"));
        }
        public string GenerateAccountNumber()
        {
            var random = new Random();
            string accNo = random.Next(1, 9).ToString();

            for (int i = 0; i < 9; i++)
            {
                accNo = String.Concat(accNo, random.Next(10).ToString());
            }

            return accNo;
        }

        public int GeneratePin()
        {
            Random random = new Random();
            return random.Next(1000, 9999);

        }

        // VALIDATION
        public bool ValidateBank(string ifscCode)
        {
            try
            {
                return _context.Banks.Any(b => b.IFSCCode == ifscCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public bool ValidateAccount(string accNo)
        {
            try
            {
                return _context.Customers.Any(c => c.AccountNumber == accNo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public bool ValidatePin(string accNumber, int pin)
        {
            try
            {
                return _context.Customers.Any(a => a.AccountNumber == accNumber && a.Pin == pin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
