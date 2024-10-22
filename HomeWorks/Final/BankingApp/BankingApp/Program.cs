using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BankingApp
{
    internal class Program
    {
        private static Dictionary<string, Customer> customers;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            LoadCustomerData();
            Console.WriteLine("Welcome to the Banking App!");

            // Get card details
            Console.Write("Enter Card Number: ");
            string cardNumber = Console.ReadLine().Trim();
            Console.Write("Enter CVC: ");
            string cvc = Console.ReadLine().Trim();
            Console.Write("Enter Expiration Date (MM/YY): ");
            string expirationDate = Console.ReadLine().Trim();

            // Validate card details
            if (customers.ContainsKey(cardNumber))
            {
                var customer = customers[cardNumber];  // Get the customer for the card number
                Console.WriteLine(customer);

                // Debug
                ////Console.WriteLine($"Validating CVC: {customer.CardDetails.Cvc} == {cvc}");
                //Console.WriteLine($"Validating Expiration Date: {customer.CardDetails.ExpirationDate} == {expirationDate}");

                if (customer.CardDetails.Cvc == cvc &&
                    customer.CardDetails.ExpirationDate == expirationDate)
                {
                    Console.Write("Enter PIN: ");
                    string pin = Console.ReadLine().Trim();

                    if (customer.ValidatePin(pin))
                    {
                        ShowMenu(customer);
                    }
                    else
                    {
                        Logger.Warn("Incorrect PIN entered for card number: {CardNumber}", cardNumber);
                        Console.WriteLine("Incorrect PIN.");
                    }
                }
                else
                {
                    Console.WriteLine("Please Provide Correct Card Details.");
                }
            }
            else
            {
                Logger.Warn("Invalid card details entered for card number: {CardNumber}", cardNumber);
                Console.WriteLine("Please Provide Correct Card Details.");
            }

            Logger.Info("Application ended.");
        }

        private static void ShowMenu(Customer customer)
        {
            Console.WriteLine($"Hello {customer.FirstName} {customer.LastName}!");
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Withdraw Amount");
                Console.WriteLine("3. Get Last 5 Transactions");
                Console.WriteLine("4. Deposit Amount");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Current Balance: {customer.Balance}");
                        LogOperation(customer, new LogEntry
                        {
                            TransactionDate = DateTime.UtcNow.ToString("o"),
                            TransactionType = "CheckBalance",
                            AmountGEL = 0,
                            AmountUSD = 0,
                            AmountEUR = 0
                        });
                        break;
                    case "2":
                        Console.Write("Enter Amount to Withdraw: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                        {
                            customer.Withdraw(withdrawAmount);
                            LogOperation(customer, new LogEntry
                            {
                                TransactionDate = DateTime.UtcNow.ToString("o"),
                                TransactionType = "Withdraw",
                                AmountGEL = withdrawAmount,
                                AmountUSD = 0,
                                AmountEUR = 0
                            });
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount entered.");
                        }
                        break;
                    case "3":
                        // Load transaction history here if needed before displaying
                        customer.LoadTransactionHistory();
                        Console.WriteLine("Last 5 Transactions:");
                        var lastTransactions = customer.TransactionHistory
                            .OrderByDescending(t => t.TransactionDate)
                            .Take(5)
                            .ToList();

                        // Print each transaction
                        foreach (var entry in lastTransactions)
                        {
                            Console.WriteLine($"Date: {entry.TransactionDate}, Type: {entry.TransactionType}, Amount: {entry.AmountGEL} GEL");
                        }
                        break;
                    case "4":
                        Console.Write("Enter Amount to Deposit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            customer.Deposit(depositAmount);
                            LogOperation(customer, new LogEntry
                            {
                                TransactionDate = DateTime.UtcNow.ToString("o"),
                                TransactionType = "Deposit",
                                AmountGEL = depositAmount,
                                AmountUSD = 0,
                                AmountEUR = 0
                            });
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount entered.");
                        }
                        break;
                    // Other cases...
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void LogOperation(Customer customer, LogEntry entry)
        {
            string logDirectoryPath = "logs"; // Define the logs directory path
            string logFilePath = Path.Combine(logDirectoryPath, "customers.json"); // Combine to get full file path

            // Ensure the logs directory exists
            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            // Read existing customer log or create a new one
            CustomerLog customerLog;

            if (File.Exists(logFilePath))
            {
                var existingLogJson = File.ReadAllText(logFilePath);
                customerLog = JsonSerializer.Deserialize<CustomerLog>(existingLogJson) ?? new CustomerLog();
            }
            else
            {
                customerLog = new CustomerLog
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    CardDetails = customer.CardDetails,
                    PinCode = customer.PinCode
                };
            }

            // Add the new log entry to the transaction history
            customerLog.TransactionHistory.Add(entry);

            // Serialize and save back to the JSON file
            var newLogJson = JsonSerializer.Serialize(customerLog, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(logFilePath, newLogJson);
        }


        private static void LoadCustomerData()
        {
            customers = BankingData.LoadCustomers();
        }
    }
}
