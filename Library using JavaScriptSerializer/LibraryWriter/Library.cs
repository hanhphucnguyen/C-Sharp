/*
 * File:            Library.cs
 * Date:            May 14, 2019
 * Author:          T. Haworth
 * Description:     Defines the Library and Item classes 
 *                  
 *                  Note that a class member like: public string Type { get; set; }
 *                  is an automatic class property.  It's automatic in the sense that 
 *                  Visual Studio will add invisible getter and setter methods for you
 *                  and when you use the member variable you're actually using the getter
 *                  and setter methods even when you do this:
 *                      object.Type = "book";       // using setter
 *                      string vale = object.Type;  // using getter
 *                  
 *                  Note that bool? means "nullable bool" meaning a member of 
 *                  this type can either hold a bool value or null. Also note that 
 *                  double? is a "nullable double".
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInventory
{
    class Item
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string RefNum { get; set; }
        public bool? OnLoan { get; set; }
        public double? ReplacementCost { get; set; }
    } // end class Item

    class Library
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Item> Inventory = new List<Item>();
        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }
    } // end class Library
}
