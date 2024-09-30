using System;
using System.Collections.Generic;

namespace WEEK6_IT8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SplitArray();

            contactsApp();
        }

        static void SplitArray()
        {
            Console.WriteLine("Enter Length of array:");
            var n = int.Parse(Console.ReadLine());
            int[] nums = new int[n];

            // Fill the array with numbers from 0 to n
            for (int i = 0; i <= n; i++)
            {
                nums[i] = i;
            }

            // Create lists to hold evens and odds
            var evens = new List<int>();
            var odds = new List<int>();

            for (int j = 0; j < nums.Length; j++)
            {
                var number = nums[j];

                if (number % 2 == 0)
                {
                    evens.Add(number); // Add the even number to the list
                    //Console.WriteLine($"Evens: {number}");
                }
                else
                {
                    odds.Add(number); // Add the odd number to the list
                    //Console.WriteLine($"Odds: {number}");
                }
            }

            // Convert lists back to arrays if needed
            int[] evenArray = evens.ToArray();
            int[] oddArray = odds.ToArray();

            // Optionally print the final arrays
            Console.WriteLine("Even numbers: " + string.Join(", ", evenArray));
            Console.WriteLine("Odd numbers: " + string.Join(", ", oddArray));
        }

        static void contactsApp()
        {
            Console.WriteLine("Welcome To Contacts");
            Dictionary<int, string> Contacts = new Dictionary<int, string>();
            Contacts.Add(557391010, "Sandro");
            Contacts.Add(599694141, "Kaladze");
            Contacts.Add(555555555, "El Presidente");

            Console.WriteLine(Contacts(1));


        }
    }
}
