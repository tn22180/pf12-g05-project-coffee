using System;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input UserName :");
            string userName = Console.ReadLine();
            Console.Write("Input Password :");
            string pass = GetPassword();
            Console.WriteLine();
            Cashier cashier = new Cashier(){username = userName, password = pass};
            CashierBl bl = new CashierBl();
            int login = bl.Login(cashier);
            if(login <= 0){
                Console.WriteLine("Can't login to System");
            }
            else{
                Console.WriteLine("Input ID item");
                int id = Convert.ToInt16(Console.ReadLine());
                Item item = new Item(){item_id = id};
                ItemBl itembl = new ItemBl();
                itembl.SearchById(item);
                Console.Write("Input Name : ");
                string name = Console.ReadLine();
                Item item1 = new Item(){item_name = name};
                itembl.SearchByName(item1);
            }
        }
        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
    }
}
