﻿using System;
using Persistence;
using System.Collections.Generic;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {   
            short mainChoose = 0, childChoose;
            
            Console.Write("Input UserName : ");
            string userName = Console.ReadLine();
            Console.Write("Input Password : ");
            string pass = GetPassword();
            Console.WriteLine();
            Cashier cashier = new Cashier(){username = userName, password = pass, cashier_id = 1};
            CashierBl bl = new CashierBl();
            ItemBl ibl = new ItemBl();
            OrderBl obl = new OrderBl();
            List<Item> lst ;
                int login = bl.Login(cashier);
                if(login <= 0){
                    Console.WriteLine("Can't login to System");
                     }
                else{
                    Console.WriteLine(userName + " Used System...");
                do{
                Menu menu = new Menu();
                mainChoose = menu.MainMenu();
                switch(mainChoose)
                {
                    case 1:
                    do
                    {
                        childChoose = menu.MenuChild();
                        switch(childChoose){
                            case 1:
                                Console.Write("Enter Id Item you want to search : ");
                                int id;
                                if (Int32.TryParse(Console.ReadLine(), out id))
                                {
                                    Item item = ibl.SearchById(id);
                                    if(item != null)
                                    {
                                    Console.WriteLine("Item Name: " +item.item_name);
                                    Console.WriteLine("Item Price: " +item.item_price);
                                    Console.WriteLine("Item Quantity: " +item.item_quantity);
                                    Console.WriteLine("Item Description: " +item.item_description);

                                    }
                                    else{
                                        Console.WriteLine("There is no item with id : " +id);
                                        }
                                }
                                else{
                                    Console.WriteLine("Your Choice is wrong !");
                                    }
                                Console.WriteLine("\n    Press Enter key to back menu...");
                                Console.ReadLine();
                                break;
                            case 2:
                                Console.Write("Enter name item you want to search: ");
                                string name = Console.ReadLine();
                                lst = ibl.SearchByName(name);
                                 Console.WriteLine("| Name Item   ");
                                foreach(Item i in lst){
                                        Console.WriteLine("| " + i.item_name);
                                    }
                                    break;

                            case 3:
                                lst = ibl.GetAll();
                                 Console.WriteLine(" | ID | Name Item          | Price  | Quantity | Description                   |");
                                foreach(Item i in lst)
                                {
                                Console.WriteLine(" | {0,-2} | {1,-18} | {2,-6} | {3,-8} | {4,-29} |", i.item_id, i.item_name, i.item_price, i.item_quantity, i.item_description);
                                }
                                break;
                        }
                    }while(childChoose != 4);
                    break;
                    case 2:
                        Order order = new Order();
                        char c;
                            // insert table-number
                            Console.Write("Input Table : ");
                            int tab = Convert.ToInt32(Console.ReadLine());
                            order.table = new TableNumber(){table_number = tab};
                            //  insert CashierInfo
                            Console.Write("Input Id Cashier order : ");
                            int CasId;
                            if(Int32.TryParse(Console.ReadLine(), out CasId))
                            {
                                Cashier cas = bl.GetCashierInfo(CasId);
                                order.cashierInfo = cas;
                            }
                            
                            do{
                            Console.Write("Input id item want to order : ");
                            int itemID;
                            if(Int32.TryParse(Console.ReadLine(), out itemID))
                            {
                                order.listItem.Add(ibl.SearchById(itemID));
                            }
                            Console.Write("Input quantity item: ");
                            int quantity;
                            if(Int32.TryParse(Console.ReadLine(), out quantity))
                            {
                                order.listItem[0].item_quantity = quantity;
                            }
                            Console.WriteLine("Do you want to be continue ? (Y/N) : ");
                            c = Convert.ToChar(Console.ReadLine());
                        }while (c == 'y' || c == 'Y');
                        Console.WriteLine("Create Order: " + (obl.CreateOrder(order) ? "completed!" : "not complete!")); 
                    break;
                }
                
                }while(mainChoose != 3);
                
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

