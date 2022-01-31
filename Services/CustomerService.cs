using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Technovert.Bankapp.Web.API.Exceptions;
using Technovert.Bankapp.Web.API.Models;
using static Technovert.Bankapp.Web.API.Enums.TransactionEnum;

namespace Technovert.Bankapp.Web.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankDbContext _context;
        private readonly IAccountService _accountService;

        public CustomerService(BankDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public void Deposit(string accountNumber, float amount)
        {
            var account = _context.Customers.Single(c => c.AccountNumber == accountNumber);
            
            if(account == null)
            {
                throw new CustomerNotFoundException("Deposit cannot be performed as no such account exists!!!");
            }
            else if (amount < 500)
            {
                throw new MinimumBalanceException("Minimum Deposit amount is 500 Rs.");
            }
            else if (amount < 0)
            {
                throw new NegativeBalanceException("Invalid amount for deposit!!!");
            }
            else
            {
                if(account.Status == Enums.CustomerEnum.AccountStatus.Close)
                {
                    throw new AccountClosedException($"No transaction allowed. Account {accountNumber} closed!!!");
                }
                else
                {
                    account.Balance += amount;
                    _accountService.UpdateTransactionHistory(account.ID, account.ID, TransactionType.Deposit, amount);
                    _context.SaveChanges();
                }
            }
            
        }
        public void Deposit(string accountNumber, string currency, float amount)
        {
            var account = _context.Customers.Single(c => c.AccountNumber == accountNumber);
            var conversionRate = GetConversionRate(currency);
            account.Balance += amount * conversionRate;
            _accountService.UpdateTransactionHistory(account.ID, account.ID, TransactionType.Deposit, amount * conversionRate);
            _context.SaveChanges();
        }

        public void Withdraw(string accountNumber, float amount)
        {
            var account = _context.Customers.Single(c => c.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new CustomerNotFoundException("Withdraw cannot be performed as no such account exists!!!");
            }
            else if (amount < account.Balance)
            {
                throw new MinimumBalanceException("Withdrawal amount exceeds Minimum Balance!!!");
            }
            else if (amount < 0)
            {
                throw new NegativeBalanceException("Invalid amount for Withdrawal!!!");
            }
            else
            {
                if (account.Status == Enums.CustomerEnum.AccountStatus.Close)
                {
                    throw new AccountClosedException($"No transaction allowed. Account {accountNumber} closed!!!");
                }
                else
                {
                    account.Balance -= amount;
                    _accountService.UpdateTransactionHistory(account.ID, account.ID, TransactionType.Withdraw, amount);
                    _context.SaveChanges();
                }
            }
        }

        public void Transfer(string fromAccountNumber, string toAccountNumber, float amount)
        {
            var senderAccount = _context.Customers.Single(c => c.AccountNumber == fromAccountNumber);
            var receiverAccount = _context.Customers.SingleOrDefault(c => c.AccountNumber == toAccountNumber);
            if(senderAccount == null)
            {
                throw new CustomerNotFoundException($"Account {fromAccountNumber} does not exists!!!");
            }
            else if(receiverAccount == null)
            {
                throw new CustomerNotFoundException($"Account {toAccountNumber} does not exists!!!");
            }
            else if (amount < 0)
            {
                throw new NegativeBalanceException("Invalid amount for Transfer!!!");
            }
            else
            {
                var transferCharge = CalculateTransferCharge(senderAccount.BankIFSCCode, receiverAccount.BankIFSCCode, amount);

                senderAccount.Balance -= amount + amount * (transferCharge / 100);
                receiverAccount.Balance += amount;

                _accountService.UpdateTransactionHistory(senderAccount.ID, receiverAccount.ID, TransactionType.Transfer, amount);
                _accountService.UpdateTransactionHistory(receiverAccount.ID, senderAccount.ID, TransactionType.Transfer, amount);
                _context.SaveChanges();
            }            
        }

        private float CalculateTransferCharge(string senderBank, string receiverBank, float amount)
        {
            if (senderBank.Substring(0, 3) == receiverBank.Substring(0, 3))
            {
                if (amount <= 100000)
                {
                    return _accountService.getIntraBankIMPS();
                }
                else
                {
                    return _accountService.getIntraBankRTGS();
                }
            }
            else
            {
                if (amount <= 100000)
                {
                    return _accountService.getInterBankIMPS();
                }
                else
                {
                    return _accountService.getInterBankRTGS();
                }
            }
        }

        private float GetConversionRate(string currencyName)
        {
            TextInfo text = new CultureInfo("en-US", false).TextInfo;
            return _context.Currencies.SingleOrDefault(c => c.Name == text.ToTitleCase(currencyName)).ConversionRate;
        }


    }
}
