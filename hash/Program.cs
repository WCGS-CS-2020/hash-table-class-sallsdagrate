using System;
using System.Collections.Generic;
using System.Text;

namespace hash
{
    class Program
    {
        const int size = 7; //constant prime number size
        static void Main(string[] args)
        {
            hashTable table = new hashTable(size); //declaring new instance of hashTable object
            try
            {
                while (true) //constant loop of options
                {
                    Console.Clear();
                    Console.WriteLine("1. Add to hash table\n" +
                    "2. Search hash table\n" +
                    "3. Delete hash entry\n" +
                    "4. Print hash table\n" +
                    "5. Output load factor\n");

                    switch (Console.ReadLine()) //switch case statements for actions
                    {
                        case "1":
                            Console.WriteLine("Enter 999 to stop entering items");
                            string item = input();
                            do
                            {
                                table.addItem(hashalgo(item), item); //method to add item
                                item = input();
                            } while (item != "999"); //entering 999 will stop items being added
                            Console.Clear();
                            break;
                        case "2":
                            bool intable = table.searchTable(Console.ReadLine()); //method for searching returns boolean
                            switch (intable)
                            {
                                case true:
                                    Console.WriteLine("Item in hashtable");
                                    break;
                                case false:
                                    Console.WriteLine("Item not in hashtable");
                                    break;
                            } //relevant output based on boolean
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "3":
                            string item1 = input();
                            table.deleteItem(hashalgo(item1), item1); //method for deleting item
                            break;
                        case "4":
                            table.printTable(); //method for printing entire table
                            break;
                        case "5":
                            table.loadFactor(); //method for seeing how many items are in the tbale
                            break;
                    }
                }
            }
            catch { Console.WriteLine("Error occured"); } //exception handling
        }

        static string input() //subroutine to make sure input strings are able to be hashed
        {
            string inputstring = Console.ReadLine();
            while (inputstring.Length < 3)
            {
                Console.WriteLine("Please enter a string of atleast 3 characters");
                inputstring = Console.ReadLine();
            }
            return inputstring;
        }

        static int hashalgo(string item) //basic hashing algorithm
        {
            try
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(item); //converts input into ascii
                int num = asciiBytes[0] + asciiBytes[1] + asciiBytes[2]; // adds first 3 ascii values
                return (num % size); //returns sum MOD size of hashtable
            }
            catch
            {
                return -1; //exception handling returns -1 to show error
            }
        }
    }


    class hashTable //hashtable class
    {
        LinkedList<string>[] arrTable = new LinkedList<string>[0]; //array comprising of linked lists at each index
        int size;
        public hashTable(int inputsize) //constructor
        {
            size = inputsize;
            arrTable = new LinkedList<string>[size]; //redefines array according to size constant
            for (int i = 0; i < size; i++)
            {
                arrTable[i] = new LinkedList<string>(); //iterates through every index of array creating a new linked list so as to not return null
            }
        }

        public void addItem(int num, string item) //add item method
        {
            arrTable[num].AddLast(item); //treats every index of array as a linked list, same methods used
        }

        public void deleteItem(int num, string item) //deleting item method
        {
            bool intable = searchTable(item); //makes sure item exists in hashtable before deleting it
            if (intable == true)
            {
                arrTable[num].Remove(item); //deletes item if it exists
                Console.WriteLine("Item removed");
            }
            else if (intable == false)
            {
                Console.WriteLine("Item is not in hash table");
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public bool searchTable(string item) //method for searching for an item
        {
            foreach (LinkedList<string> l in arrTable)
            {
                foreach (string s in l)
                {
                    if (s == item) { return true;} //iterates through every index and list
                }
            }
            return false;           
        }

        public void printTable() //method for printing hashtable
        {
            int count = 0;
            foreach (LinkedList<string> l in arrTable)
            {
                Console.WriteLine("Hash: {0}", Convert.ToString(count));
                count++;
                foreach (string s in l)
                {
                    Console.WriteLine(s); //iterates every
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        public void loadFactor() //method for counting number of items in hashtable
        {
            int count = 0;
            foreach (LinkedList<string> l in arrTable)
            {
                foreach (string s in l)
                {
                    count++;
                }
            }
            Console.WriteLine("{0} strings in the hash table", Convert.ToString(count));
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
