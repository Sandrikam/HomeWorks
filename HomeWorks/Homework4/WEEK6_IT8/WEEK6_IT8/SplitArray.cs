using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEEK6_IT8
{
    internal class SplitArray
    {
        public void Run()
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
    }   
}
