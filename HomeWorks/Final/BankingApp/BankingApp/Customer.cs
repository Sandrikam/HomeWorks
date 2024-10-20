using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }
        public CardDetail CardDetails { get; set; }
        public string PinCode { get; private set; }
        public decimal Balance { get; private set; }
        private List<Transaction> transactions;

        public Customer(string firstName, string lastName, string cardNumber, string expirationDate, string cvc, string pinCode)
        {
            FirstName = firstName;
            LastName = lastName;
            CardDetails = new CardDetail
            {
                CardNumber = cardNumber,
                ExpirationDate = expirationDate,
                Cvc = cvc
            };
            PinCode = pinCode;
            Balance = 0;
            transactions = new List<Transaction>();
        }

        public bool ValidatePin(string pin) => PinCode == pin;

        public List<string> GetLastTransactions() => transactions
            .OrderByDescending(t => t.TransactionDate)
            .Take(5)
            .Select(t => $"{t.TransactionType}: {t.AmountGEL} GEL on {t.TransactionDate}")
            .ToList();

        public void Deposit(decimal amount)
        {
            Balance += amount;
            transactions.Add(new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Deposit",
                AmountGEL = amount,
                AmountUSD = 0,
                AmountEUR = 0
            });
            //BankingData.SaveCustomers();
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            Balance -= amount;
            transactions.Add(new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Withdrawal",
                AmountGEL = amount,
                AmountUSD = 0,
                AmountEUR = 0
            });
            //BankingData.SaveCustomers();
        }

        public void ChangePin(string newPin)
        {
            PinCode = newPin;
            //BankingData.SaveCustomers();
        }
    }
}
