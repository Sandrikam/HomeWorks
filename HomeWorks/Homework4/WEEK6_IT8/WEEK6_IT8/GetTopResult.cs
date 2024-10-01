using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEEK6_IT8
{
    internal class GetTopResult
    {
        public void Run()
        {
            int[] scores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("Enter the number of top participants to display:");
            var n = int.Parse(Console.ReadLine());

            if (n > 0 && n <= scores.Length)
            {

                Console.WriteLine("Top {0} participants:", n);

                for (int i = scores.Length - n; i < scores.Length; i++)
                {

                    Console.Write(scores[i] + " ");

                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and {0}.", scores.Length);
            }
        }
    }
}
