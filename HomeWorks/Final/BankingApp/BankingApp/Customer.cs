using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BankingApp
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CardDetail CardDetails { get; set; }
        public string PinCode { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();

        // Parameterless constructor required for deserialization
        public Customer() { }

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
            Balance = 0; // Initialize with zero balance
        }

        public bool ValidatePin(string pin) => PinCode == pin;

        public void LoadTransactionHistory()
        {
            string logFilePath = "logs/customers.json"; // Path to the JSON file

            if (File.Exists(logFilePath))
            {
                try
                {
                    var json = File.ReadAllText(logFilePath);
                    var customer = JsonSerializer.Deserialize<Customer>(json); // Deserialize to a single Customer object

                    if (customer == null)
                    {
                        Console.WriteLine("No customer data found in the file.");
                        TransactionHistory = new List<Transaction>(); // Initialize with an empty list
                        return;
                    }

                    // Check if the current customer's CardDetails match the deserialized customer's CardDetails
                    if (customer.CardDetails.CardNumber == CardDetails.CardNumber)
                    {
                        TransactionHistory = customer.TransactionHistory; // Assign the TransactionHistory
                    }
                    else
                    {
                        TransactionHistory = new List<Transaction>(); // Initialize with an empty list if customer is not found
                    }
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine($"Error reading transaction history: {jsonEx.Message}");
                    TransactionHistory = new List<Transaction>(); // Initialize with an empty list on JSON error
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    TransactionHistory = new List<Transaction>(); // Initialize with an empty list on general error
                }
            }
            else
            {
                // If the file doesn't exist, initialize with an empty list
                TransactionHistory = new List<Transaction>();
            }
        }


        public void Deposit(decimal amount)
        {
            Balance += amount;
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Deposit",
                AmountGEL = amount,
                AmountUSD = 0,
                AmountEUR = 0
            };
            TransactionHistory.Add(transaction); // Add to TransactionHistory
            // BankingData.SaveCustomers(); // Uncomment this if you want to save after each operation
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            Balance -= amount;
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Withdrawal",
                AmountGEL = amount,
                AmountUSD = 0,
                AmountEUR = 0
            };
            TransactionHistory.Add(transaction); // Add to TransactionHistory
            // BankingData.SaveCustomers(); // Uncomment this if you want to save after each operation
        }

        public void ChangePin(string newPin)
        {
            PinCode = newPin;
            // BankingData.SaveCustomers(); // Uncomment this if you want to save after changing PIN
        }
    }
}
