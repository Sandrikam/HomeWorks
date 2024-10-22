using System;
using System.Collections.Generic;

namespace BankingApp
{
    internal class Program
    {
        private static Dictionary<string, Customer> customers;

        static void Main(string[] args)
        {
            LoadCustomerData();

            Console.WriteLine("Welcome to the Banking App!");

            // Get card details
            Console.Write("Enter Card Number: ");
            string cardNumber = Console.ReadLine().Trim();
            Console.WriteLine(cardNumber);

            Console.Write("Enter CVC: ");
            string cvc = Console.ReadLine().Trim();
            Console.WriteLine(cvc);
            
            Console.Write("Enter Expiration Date (MM/YY): ");
            string expirationDate = Console.ReadLine().Trim();
            Console.WriteLine(expirationDate);

            Console.WriteLine("Available card numbers:");
            foreach (var key in customers.Keys)
            {
                Console.WriteLine(key);
            }


            // Validate card details
            if (customers.ContainsKey(cardNumber))
            {
                var customer = customers[cardNumber];  // Get the customer for the card number
                Console.WriteLine(customer);

                // Debugging output
                Console.WriteLine($"Validating CVC: {customer.CardDetails.Cvc} == {cvc}");
                Console.WriteLine($"Validating Expiration Date: {customer.CardDetails.ExpirationDate} == {expirationDate}");

                if (customer.CardDetails.Cvc == cvc &&
                    customer.CardDetails.ExpirationDate == expirationDate)
                {
                    Console.Write("Enter PIN: ");
                    string pin = Console.ReadLine().Trim();  // Trim whitespace

                    if (customer.ValidatePin(pin))
                    {
                        ShowMenu(customer);
                    }
                    else
                    {
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
                Console.WriteLine("Please Provide Correct Card Details.");
            }
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
                        break;
                    case "2":
                        Console.Write("Enter Amount to Withdraw: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                        {
                            customer.Withdraw(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount entered.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Last 5 Transactions: ");
                        customer.GetLastTransactions().ForEach(Console.WriteLine);
                        break;
                    case "4":
                        Console.Write("Enter Amount to Deposit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            customer.Deposit(depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount entered.");
                        }
                        break;
                    case "5":
                        Console.Write("Enter New PIN: ");
                        string newPin = Console.ReadLine();
                        customer.ChangePin(newPin);
                        Console.WriteLine("PIN changed successfully.");
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void LoadCustomerData()
        {
            customers = BankingData.LoadCustomers();
        }
    }
}
