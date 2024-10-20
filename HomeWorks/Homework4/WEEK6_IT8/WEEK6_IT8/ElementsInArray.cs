using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEEK6_IT8
{
    internal class ElementsInArray
    {
        public void Run() 
        {
            Console.WriteLine("Enter the number of elements:");
            var n = int.Parse(Console.ReadLine());

            Console.WriteLine($"Enter {n} elements separated by spaces:");
            string[] input = Console.ReadLine().Split(' ');

            int[] numbers = Array.ConvertAll(input, int.Parse);

            // Dictionary to count occurrences
            Dictionary<int, int> counts = new Dictionary<int, int>();

            // Count each element's occurrences
            foreach (var number in numbers)
            {
                if (counts.ContainsKey(number))
                {
                    counts[number]++;
                }
                else
                {
                    counts[number] = 1;
                }
            }

            var totalSum = 0;
            foreach (var kvp in counts)
            {
                var element = kvp.Key;
                var count = kvp.Value;
                var sum = element * count;
                totalSum += sum;

                Console.WriteLine($"{element} appears {count} time{(count > 1 ? "s" : "")} sum {sum}");
            }

            Console.WriteLine($"Total sum of all elements: {totalSum}");
        }
    }
}
