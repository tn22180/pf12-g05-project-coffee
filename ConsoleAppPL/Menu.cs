using System;
using Persistence;
using BL;
namespace ConsoleAppPL
{
    public class Menu{
        public short MainMenu(){
            short choice = 0;
            string line = "=======================================================";
            Console.WriteLine(line);
            Console.WriteLine("COFFE SELLING MANAGEMENT");
            Console.WriteLine(line);
            Console.WriteLine("1. Coffee and Food Management");
            Console.WriteLine("2. Order");
            Console.WriteLine("3. Print Invoice");
            Console.WriteLine("4. Exit");
            Console.WriteLine(line);
            do
            {
                Console.Write("Your Choice :");
                try{
                    choice = Int16.Parse(Console.ReadLine());
                }
                catch{
                    Console.WriteLine("Your choice is wrong!");
                    continue;
                }
            }while(choice <=0 || choice > 4);
            return choice;
        }
        public short MenuChild(){
            short choice = 0;
            string line = "=======================================================";
            Console.WriteLine(line);
            Console.WriteLine("Coffee and Food Management");
            Console.WriteLine(line);
            Console.WriteLine("1. Search Item By Id");
            Console.WriteLine("2. Search Item by Name");
            Console.WriteLine("3. Display All Item");
            Console.WriteLine("4. Exit");
            Console.WriteLine(line);
            do
            {
                Console.Write("Your Choice :");
                try{
                    choice = Int16.Parse(Console.ReadLine());
                }
                catch{
                    Console.WriteLine("Your choice is wrong!");
                    continue;
                }
            }while( choice <=0 || choice > 4);
            return choice;
        }
    }
}