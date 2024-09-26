using System;

namespace WEEK5_IT8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Call the function to check if a number is even or odd
            CheckEvenOrOdd();

            // Call the function to perform arithmetic operations
            PerformArithmeticOperations();
            // Call Function to Switch Variables
            switchVariables();

            //Call to generate Multiplication Table
            generateMultiplicTable();

            //Call to Print all evens ans squares
            printEvenSquare();
        }

        static void CheckEvenOrOdd()
        {
            Console.WriteLine($"Enter Number to Check: ");
            var input = Console.ReadLine();
            var dividend = Convert.ToInt32(input);
            var result = dividend % 2;

            switch (result)
            {
                case 0:
                    Console.WriteLine("Yes");
                    break;
                default:
                    Console.WriteLine("NO");
                    break;
            }
        }

        static void PerformArithmeticOperations()
        {
            Console.WriteLine($"Enter Number X: ");
            var x = int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter Number Y: ");
            var y = int.Parse(Console.ReadLine());

            Console.WriteLine($"Enter operator such as - + / *");
            var oper = char.Parse(Console.ReadLine());

            var result = 0;
            switch (oper)
            {
                case '+':
                    result = x + y;
                    Console.WriteLine(result);
                    break;
                case '-':
                    var minuend = x > y ? x : y;
                    var subtrahend = x < y ? x : y;
                    result = minuend - subtrahend;
                    Console.WriteLine(result);
                    break;
                case '*':
                    result = x * y;
                    Console.WriteLine(result);
                    break;
                case '/':
                    var dvidend = x > y ? x : y;
                    var devide = x < y ? x : y;
                    if (devide != 0)
                    {
                        result = dvidend / devide;
                        Console.WriteLine(result);
                    }
                    else
                    {
                        Console.WriteLine("Cannot divide by zero.");
                    }

                    break;
            }
        }

        static void switchVariables()
        {

            Console.WriteLine($"Enter Number X: ");
            var x = int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter Number Y: ");
            var y = int.Parse(Console.ReadLine());

            var z = x;

            x = y;

            y = z;

            Console.WriteLine($"X={x}; Y={y}");
        }

        static void generateMultiplicTable()
        {

            Console.WriteLine($"Enter Number: ");
            var x = int.Parse(Console.ReadLine());

            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"{x}*{i}={x * i}");

            }
        }

        static void printEvenSquare()
        {

            Console.WriteLine($"Enter Number: ");
            var n = int.Parse(Console.ReadLine());

            for (int i = 1; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    var square = i * i; // Calculate the square of the number
                    Console.WriteLine(square);
                }


            }
        }
    }
}

