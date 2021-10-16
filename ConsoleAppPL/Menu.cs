using System;
using Persistence;
using BL;
namespace ConsoleAppPL
{
    public class Menu{
       public string logo = @"
        /  __ \      / _|/ _|          |_   _|                 | | | |      
        | /  \/ ___ | |_| |_ ___  ___    | |_   _  __ _ _ __   | |_| | __ _ 
        | |    / _ \|  _|  _/ _ \/ _ \   | | | | |/ _` | '_ \  |  _  |/ _` |
        | \__/\ (_) | | | ||  __/  __/   | | |_| | (_| | | | | | | | | (_| |
        \____/ \___/|_| |_| \___|\___|   \_/\__,_|\__,_|_| |_| \_| |_/\__,_| "; 
        public short MainMenu(){
            short choice = 0;
            string line = "===================================================================================";
            Console.WriteLine(line);                                    
            Console.WriteLine(logo);
            Console.WriteLine(line);
            Console.WriteLine("        1. Coffee and Drinks Management");
            Console.WriteLine("        2. Order");
            Console.WriteLine("        3. Payment");
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
            string line = "===================================================================================";
            Console.WriteLine(line);
             Console.WriteLine(logo);
            Console.WriteLine("Coffee and Food Management");
            Console.WriteLine(line);
            Console.WriteLine("        1. Search Item By Id");
            Console.WriteLine("        2. Search Item by Name");
            Console.WriteLine("        3. Display All Item");
            Console.WriteLine("        4. Exit");
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