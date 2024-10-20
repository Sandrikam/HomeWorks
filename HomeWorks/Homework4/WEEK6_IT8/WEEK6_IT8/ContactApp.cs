using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WEEK6_IT8
{
    internal class ContactApp
    {
        private static Dictionary<string, string> contacts = new Dictionary<string, string>();
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Update Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. View Contacts");
                Console.WriteLine("5. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        UpdateContact();
                        break;
                    case "3":
                        DeleteContact();
                        break;
                    case "4":
                        ViewContacts();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void AddContact()
        {
            Console.WriteLine("Enter contact name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter contact phone number:");
            var phone = Console.ReadLine();

            if (!contacts.ContainsKey(name))
            {
                contacts[name] = phone;
                Console.WriteLine("Contact added successfully.");
            }
            else
            {
                Console.WriteLine("Contact with this name already exists.");
            }
        }

        static void UpdateContact()
        {
            Console.WriteLine("Enter the name of the contact to update:");
            var name = Console.ReadLine();

            if (contacts.ContainsKey(name))
            {
                var newPhone = Console.ReadLine();
                contacts[name] = newPhone;
                Console.WriteLine("Updated Contact");
            }
            else
            {
                Console.WriteLine("Contact Does not Exist!");
            }

        }

        static void DeleteContact()
        {
            Console.WriteLine("Enter the name of the contact to Delete:");
            var name = Console.ReadLine();

            if (contacts.Remove(name))
            {
                Console.WriteLine("Deleted Contact Successfully");
            }
            else
            {
                Console.WriteLine("Contact Does not Exist!");
            }
        }

        static void ViewContacts()
        {
            Console.WriteLine("list of Contacts: ");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Key}, Phone: {contact.Value}");
            }

            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
            }
        }

    }
}
