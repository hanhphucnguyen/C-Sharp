/*
 * Program:         LibraryWriter using JavaScriptSerializer
 * File:            Program.cs
 * Date:            May 14, 2019
 * Author:          T. Haworth
 * Description:     Demonstrates using the JavaScriptSerializer class to write the 
 *                  member data of an object to a file in JSON format.
 *                  
 *                  The key part of this example is the WriteLibToJson() method which
 *                  is at the bottom of this file.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryInventory; // Library class
using System.Web.Script.Serialization;  // JavaScriptSerializer class
using System.IO;    // File class

namespace LibraryWriter
{
    class Program
    {
        // The Main method which prompts creates a Library object, obtains inputs from the 
        // user to populate the object and then writes the object to a JSON file
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a New Library...\n");

            Library lib = new Library();

            Console.WriteLine("Please enter the following information...\n");
            Console.Write("Library Name: ");
            lib.Name = Console.ReadLine();
            Console.Write("Library Address: ");
            lib.Address = Console.ReadLine();

            // Outer loop to let the user add any number of items to the library 
            bool done;
            do
            {
                Console.WriteLine("\n\nAdding a New Item...\n");

                Item item = new Item();

                Console.WriteLine("Please nter the following inmformation...\n");

                // Inner loop to validate the Type value entered 
                // It must be "book", "cd" or "dvd"
                bool valid;
                do
                {
                    Console.Write("Item Type (book/cd/dvd): ");
                    item.Type = Console.ReadLine();

                    valid = (item.Type == "book" || item.Type == "cd" || item.Type == "dvd");

                    if (!valid)
                        Console.WriteLine("ERROR: 'Item Type' must be 'book', 'cd' or 'dvd'.");

                } while (!valid);

                Console.Write("Item Title: ");
                item.Title = Console.ReadLine();

                Console.Write("Author: ");
                item.Author = Console.ReadLine();

                Console.Write("Reference Number: ");
                item.RefNum = Console.ReadLine();

                // Inner loop to validate the OnLoan value entered 
                // It must be "true", "false" or an empty or null string
                do
                {
                    Console.WriteLine("On Loan? (true/false or press enter if unknown): ");
                    string data = Console.ReadLine();

                    valid = (data == "true" || data == "false" || data == "");

                    if (valid)
                        item.OnLoan = GetValueOrNull<bool>(data);
                    else
                        Console.WriteLine("ERROR: 'On Loan' must be 'true', 'false' or nothing.");
                } while (!valid);

                // Inner loop to validate the ReplacementCost value entered 
                // It must be a number or an empty or null string 
                do
                {
                    Console.WriteLine("Replacement Cost: $");
                    string data = Console.ReadLine();

                    double temp;
                    valid = double.TryParse(data, out temp) || data == "";

                    if (valid)
                        item.ReplacementCost = GetValueOrNull<double>(data);
                    else
                        Console.WriteLine("ERROR: 'Replacement Cost' must be a number or nothing.");

                } while (!valid);

                lib.AddItem(item);

                Console.Write("\nAdd another item? (y/n): ");
                done = Console.ReadKey().KeyChar != 'y';

                WriteLibToJson(lib);

            } while (!done);
        } // end Main()


        // A helper method to convert a string to one of two types:
        // 1. a value of type T (if value represents a valid value of type T)
        // 2. null (if value is "" or null)
        // When you call the method you must provide a type for the method's type 
        // parameter like this: GetValueOfNull<bool>(data)
        private static T? GetValueOrNull<T>(string value) where T : struct
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
                return (T)Convert.ChangeType(value, typeof(T));
        }


        // A helper method to accept an object of type Library, convert the object's  
        // data to a string containing JSON code and then write the string to a file 
        // This version of the method uses the JavaScriptSerializer class
        private static void WriteLibToJson(Library lib)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string json = ser.Serialize(lib);
            File.WriteAllText("c:\\library.json", json);
        }

    } // end class Program
}
