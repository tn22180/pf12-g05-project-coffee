using System;
using Persistence;
using System.Collections.Generic;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {   
             string logo1 = @"
 =============================================================================
     /  __ \      / _|/ _|          |_   _|                 | | | |      
     | /  \/ ___ | |_| |_ ___  ___    | |_   _  __ _ _ __   | |_| | __ _ 
     | |    / _ \|  _|  _/ _ \/ _ \   | | | | |/ _` | '_ \  |  _  |/ _` |
     | \__/\ (_) | | | ||  __/  __/   | | |_| | (_| | | | | | | | | (_| |
     \____/ \___/|_| |_| \___|\___|   \_/\__,_|\__,_|_| |_| \_| |_/\__,_| ";
            string logo2 = @"
==============================================================================
         _____         _           
        |  _  |       | |          
        | | | |_ __ __| | ___ _ __ 
        | | | | '__/ _` |/ _ \ '__|
        \ \_/ / | | (_| |  __/ |   
         \___/|_|  \__,_|\___|_|   ";
          string logo3 = @"
==============================================================================
        ______                                _   
        | ___ \                              | |  
        | |_/ /_ _ _   _ _ __ ___   ___ _ __ | |_ 
        |  __/ _` | | | | '_ ` _ \ / _ \ '_ \| __|
        | | | (_| | |_| | | | | | |  __/ | | | |_ 
        \_|  \__,_|\__, |_| |_| |_|\___|_| |_|\__|
                    __/ |                         
                   |___/                             ";
            short mainChoose = 0, childChoose;
    string line = " =============================================================================";
            CashierBl bl = new CashierBl();
            ItemBl ibl = new ItemBl();
            OrderBl obl = new OrderBl();
            List<Item> lst ;
            int login;
            Console.WriteLine(logo1);
            do{
            Console.WriteLine(line);
            Console.WriteLine("                                  LOGIN");
             Console.WriteLine(line);
            string userName = CheckUserName();
            string pass = CheckPass();
            Console.WriteLine();
            Cashier cashier = new Cashier(){Username = userName, Password = pass};
                 login = bl.Login(cashier);
                if(login <= 0){
                    Console.WriteLine("Can't login to System, Try again...");
                     }
            }while(login <= 0);  
                
                if(login > 0){
                do{
                Menu menu = new Menu();
                mainChoose = menu.MainMenu();
                switch(mainChoose)
                {
                    case 1:
                    Console.WriteLine(logo1);
                    Console.WriteLine(line);
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
                                    Console.WriteLine("Item Name: " +item.ItemName);
                                    Console.WriteLine("Item Price: " +item.ItemPrice);
                                    Console.WriteLine("Item Quantity: " +item.ItemQuantity);
                                    Console.WriteLine("Item Description: " +item.ItemDescription);

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
                                 Console.WriteLine(" | ID | Name Item          | Price  | Quantity | Description             |");
                                foreach(Item i in lst){
                                        Console.WriteLine(" | {0,-2} | {1,-18} | {2,-6} | {3,-8} | {4,-23} |", i.ItemId, i.ItemName, i.ItemPrice, i.ItemQuantity, i.ItemDescription);
                                    }
                                Console.WriteLine("\n    Press Enter key to back menu...");
                                Console.ReadLine();
                                    break;
                            case 3:
                                lst = ibl.GetAll();
                                 Console.WriteLine(" | ID | Name Item          | Price  | Quantity | Description             |");
                                foreach(Item i in lst)
                                {
                                Console.WriteLine(" | {0,-2} | {1,-18} | {2,-6} | {3,-8} | {4,-23} |", i.ItemId, i.ItemName, i.ItemPrice, i.ItemQuantity, i.ItemDescription);
                                }
                                Console.WriteLine("\n    Press Enter key to back menu...");
                                Console.ReadLine();
                                break;
                        }
                    }while(childChoose != 4);
                    break;
                    case 2:
                        Console.WriteLine(logo2);
                        Console.WriteLine(line);
                        Order order = new Order();
                        List<TableNumbers> ltb;
                        char c;
                            // insert table-number
                            int tab = -1;
                          do{
                            ltb = obl.GetAllTable(1);
                             Console.WriteLine("Empty Tables:     ");
                            foreach(TableNumbers t in ltb){
                                        Console.Write(t.TableNumber+"\t");
                            }
                            try{
                                Console.Write("Choose Table: ");
                                 tab = Int32.Parse(Console.ReadLine());
                                
                                TableNumbers table1 = obl.GetTableByStatus(tab);
                                order.Table = table1;
                                if(order.Table.TableStatus == TableStatus.NO_EMPTY)
                                {
                                    Console.WriteLine("The table is booked! Please Choose another table.");
                                }
                            }
                            catch{
                                Console.WriteLine("your choose is wrong! Please Choose again.");
                                continue;
                            }
                          }while( tab < 0 || tab > 6 || order.Table.TableStatus != 1);
                        //Insert id
                            int CasId = 0;
                          do{

                            try{
                                Console.Write("Input Id Cashier order : ");
                                CasId = Int32.Parse(Console.ReadLine());
                                Cashier cas = bl.GetCashierInfo(CasId);
                                order.CashierInfo = cas;
                                if(cas == null)
                                {
                                Console.WriteLine("Don't have id Cashier : "+CasId);
                                Console.WriteLine("Input Again ....");
                                }
                            }
                            catch{
                                Console.WriteLine("Don't have id Cashier : "+CasId);
                                Console.WriteLine("Input Again ....");
                                continue;
                            }
                          }while(CasId != 1 && CasId != 2);
                        //insert item
                            do{
                                int itemID = 0;
                                bool check = true;
                                Item i = null;
                                do{
                                    try{
                                        Console.Write("Input id item want to order : ");
                                        itemID = Int32.Parse(Console.ReadLine());
                                        i = ibl.SearchById(itemID);
                                        if(i == null)
                                        {
                                            Console.WriteLine("Don't have id Item : "+itemID);
                                            Console.WriteLine("Input Again ....");
                                            check = false;
                                        }
                                        else{
                                            order.ListItem.Add(i);
                                            check = true;
                                        }
                                        
                                    }catch{
                                         Console.WriteLine("Don't have id Item : "+itemID);
                                         Console.WriteLine("Input Again ....");
                                         continue;
                                    }
                               
                                }while(check == false);
                                bool checkQuantity = true;
                                int quantity = 0;
                                do{
                                    try{
                                        Console.Write("Input quantity: ");
                                        quantity = Int32.Parse(Console.ReadLine());
                                        if(i.ItemQuantity < quantity)
                                        {
                                            Console.WriteLine("The quantity you need in stock is out");
                                            checkQuantity = false;
                                        }
                                        else{
                                            i.ItemQuantity = quantity;
                                            checkQuantity = true;
                                        }
                                    }catch{
                                        Console.WriteLine("Your choice is wrong..!");
                                        Console.WriteLine("Input Again ....");
                                        continue;
                                    }
                                }while(checkQuantity != true);
                                    Console.Write("Do you want to be continue ? (Y/N) : ");
                                    c = Convert.ToChar(Console.ReadLine());
                            }while (c == 'y' || c == 'Y');
                            
                                Console.WriteLine("Create Order: " + (obl.CreateOrder(order) ? "completed!" : "not complete!")); 
                                 Console.WriteLine("\n    Press Enter key to back menu...");
                                    Console.ReadLine();

                    break;
                    case 3:
                        Console.WriteLine(logo3);
                        Console.WriteLine(line);
                        List<TableNumbers> ltb1 ;        
                        int tabl = 0;
                            ltb1 = obl.GetAllTable(2);
                            Order orders = new Order();
                            Console.WriteLine("The Tables is booked :     ");
                                foreach(TableNumbers t in ltb1){
                                            Console.Write(t.TableNumber+"\t");
                            }
                            bool checkTab = true;
                            do{
                                try{
                                        Console.Write("\nChoose Table: ");
                                        tabl = Int32.Parse(Console.ReadLine());
                                        TableNumbers table2 = obl.GetTableByStatus(tabl);
                                        if(table2.TableStatus == 1)
                                        {
                                            Console.WriteLine("The table is not booked!");
                                            checkTab = false;
                                        }
                                        else
                                        {
                                            orders.Table = table2;
                                            checkTab = true;
                                        }
                                    }
                                catch{
                                        Console.WriteLine("your choose is wrong! Please Choose again.");
                                        continue;
                                }
                            }while(checkTab != true);
                            Order orderInfo = obl.GetOrderByTab(tabl,2);
                            if(orderInfo != null)
                            {   Console.WriteLine("==============================================================");
                                Console.WriteLine("                       Coffee Tuan Ha");
                                Console.WriteLine("==============================================================");
                                Console.WriteLine("|                           INVOICE                           |");                      
                                Console.WriteLine("==============================================================");
                                Console.WriteLine(" ID: " + orderInfo.CashierInfo.CashierId);
                                Console.WriteLine(" Name: " + orderInfo.CashierInfo.CashierName    +"     Phone: "+ orderInfo.CashierInfo.Phone);
                                Console.WriteLine(" OrderId : "+ orderInfo.OrderId);
                                Console.WriteLine(" Table : "+ orderInfo.Table.TableNumber+ "    Date Time: "+ orderInfo.OrderDate);
                            
                            }
                        
                        List<OrderDetail> od = obl.GetOrderDetailByIds(orderInfo.OrderId);   
                        double total = 0,totalMoney = 0;
                        if(od != null){
                            Console.WriteLine("| Name Item          | Price  | Quantity | Total     |");
                             foreach(var i in od)
                            {
                                Console.WriteLine("| {0,-18} | {1,-6} | {2,-8} | {3,-8}  |", i._ItemName, i._ItemPrice, i._ItemQuantity, total = (i._ItemQuantity*i._ItemPrice));
                                totalMoney += total;
                            }
                            Console.WriteLine();
                            Console.WriteLine("| Total Money                            | {0,-9} |",totalMoney);;
                            Console.WriteLine("    Thank you for buying our products ... ");
                        }
                    
                        Console.WriteLine("\n    Press Enter key to back menu...");
                        Console.ReadLine();
                    break;
                }
                }while(mainChoose != 4);
                
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
        static string CheckUserName()
        {   
            string useName;
            do{
                Console.Write("   Input UserName: ");
                useName = Console.ReadLine();
                if(useName.Length < 6)
                {
                    Console.WriteLine("UserName must be more than 6 characters! Try again ...");
                }
            }while(useName.Length < 6);
            return useName;
        }
        static string CheckPass()
        {   
            string pass;
            do{
                Console.Write("   Input Password : ");
                pass = GetPassword();
                if(pass.Length < 6)
                {
                    Console.WriteLine("\nPassword must be more than 6 characters! Try again ...");
                }
            }while(pass.Length < 6);
            return pass;
        }
       
    }
}

