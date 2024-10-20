using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BankingApp
{
    public static class BankingData
    {
        private const string DataFilePath = "customers.json";

        public static Dictionary<string, Customer> LoadCustomers()
        {
            if (File.Exists(DataFilePath))
            {
                try
                {
                    var jsonData = File.ReadAllText(DataFilePath);
                    var customers = JsonSerializer.Deserialize<Dictionary<string, Customer>>(jsonData);

                    // Check for null to avoid potential null reference exceptions
                    return customers ?? new Dictionary<string, Customer>();
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error reading JSON data: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while loading customers: {ex.Message}");
                }
            }
            else
            {
                var customers = SeedData(); // Load initial data if JSON file doesn't exist
                SaveCustomers(customers); // Save initial data to JSON
                return customers;
            }
            return new Dictionary<string, Customer>();
        }

        private static Dictionary<string, Customer> SeedData()
        {
            var customers = new Dictionary<string, Customer>
            {
                { "1234567890123456", new Customer("Artur", "Zakharyan", "1234-5678-9012-3456", "12/25", "123", "2307") },
                { "9876543210987654", new Customer("Sardion", "Maisuryan", "9876-5432-1098-7654", "11/26", "456", "2308") },
                { "9786534190875643", new Customer("Jorik", "Kapanyan", "9786-5342-1908-7564", "11/29", "125", "1967") }            
            };

            return customers;
        }

        public static void SaveCustomers(Dictionary<string, Customer> customers)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(DataFilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customers data: {ex.Message}");
            }
        }
    }
}
