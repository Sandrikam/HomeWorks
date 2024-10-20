using System;
using System.Collections.Generic;
using WEEK6_IT8;

namespace WEEK6_IT8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ContactApp      = new ContactApp();
            var SplitArray      = new SplitArray();
            var ElementsInArray = new ElementsInArray();
            var GetTopResult = new GetTopResult();

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Split Array");
                Console.WriteLine("2. Contacts App");
                Console.WriteLine("3. Elements in Array");
                Console.WriteLine("4. Top Result");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SplitArray.Run();
                        break;
                    case "2":
                        ContactApp.Run();
                        break;
                    case "3":
                        ElementsInArray.Run();
                        break;
                    case "4":
                        GetTopResult.Run();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
        
    }
}
