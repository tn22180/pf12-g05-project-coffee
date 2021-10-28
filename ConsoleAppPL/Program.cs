using System;
using Persistence;
using System.Collections.Generic;
using BL;
using System.Globalization;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {   
             string logo1 = @"
 =============================================================================
      _ _ _         _   _            _ _ _                   _   _
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
            Cashier cashier;
            var money  = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            Console.WriteLine(logo1);
            do{
            Console.WriteLine(line);
            Console.WriteLine("                                  LOGIN");
             Console.WriteLine(line);
            string userName = CheckUserName();
            string pass = CheckPass();
            Console.WriteLine();
            // cashier = new Cashier(){Username = userName, Password = pass};
                 cashier = bl.Login(userName, pass);
                if(cashier.Role <= 0){
                    Console.WriteLine("   Can't login to System, Try again...");
                     }
            }while(cashier.Role <= 0);  
                
                if(cashier.Role > 0){
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
                                    Console.WriteLine("+---------------------------------------------------------------+");
                                    Console.WriteLine("|                           Info Item                           |");
                                    Console.WriteLine("+---------------------------------------------------------------+");
                                    Console.WriteLine("| Item Name: {0,-18}                                 |" ,item.ItemName);
                                    Console.WriteLine("| Item Price: {0,-18}                                |" ,item.ItemPrice);
                                    Console.WriteLine("| Item Quantity: {0,-18}                             |" ,item.ItemQuantity);
                                    Console.WriteLine("| Item Description: {0,-18}                          |" ,item.ItemDescription);
                                    Console.WriteLine("+---------------------------------------------------------------+");

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
                                bool checkName = true;
                                string name = "";
                                do{
                                    try{
                                            Console.Write("Enter name item you want to search: ");
                                             name = Console.ReadLine();
                                            lst = ibl.SearchByName(name);
                                            if(lst.Count == 0 || lst == null)
                                            {
                                                Console.WriteLine("Don't found name item : "+name);
                                                Console.WriteLine("Try again .");
                                                checkName = false;
                                            }
                                            else{
                                                Console.WriteLine(" +------------------------------------------------------------------------------+");
                                                Console.WriteLine(" | ID | Name Item                 | Price    | Quantity | Description           |");
                                                Console.WriteLine(" +------------------------------------------------------------------------------+");
                                                
                                                foreach(Item i in lst){
                                                        Console.WriteLine(" | {0,-2} | {1,-25} | {2,-6} | {3,-8} | {4,-21} |", i.ItemId, i.ItemName, String.Format(money ,"{0:c}", i.ItemPrice), i.ItemQuantity, i.ItemDescription);

                                                    }
                                                        Console.WriteLine(" +------------------------------------------------------------------------------+");
                                                Console.WriteLine("\n    Press Enter key to back menu...");
                                                Console.ReadLine();
                                                checkName = true;
                                            }
                                    }catch{
                                        Console.WriteLine("Don't found name item : "+name);
                                        Console.WriteLine("Try again .");
                                    }
                                    
                                }while(checkName != true);
                                    break;
                            case 3:
                                lst = ibl.GetAll();
                                 Console.WriteLine(" +------------------------------------------------------------------------------+");
                                 Console.WriteLine(" | ID | Name Item                 | Price    | Quantity | Description           |");
                                 Console.WriteLine(" +------------------------------------------------------------------------------+");
                                
                                foreach(Item i in lst)
                                {
                                Console.WriteLine(" | {0,-2} | {1,-25} | {2,-6} | {3,-8} | {4,-21} |", i.ItemId, i.ItemName, String.Format(money, "{0:c}", i.ItemPrice), i.ItemQuantity, i.ItemDescription);
                                }
                                 Console.WriteLine(" +------------------------------------------------------------------------------+");

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
                            bool checkTabEmpty = true;
                          do{
                            // ltb = obl.GetAllTableByStatus(OrderStatus.CREATE_NEW_ORDER);
                           ltb =  obl.GetAllTable();
                             Console.WriteLine("Empty Tables:     ");
                            foreach(TableNumbers t in ltb){
                                         Console.Write("["+t.TableNumber+"]"+"\t");
                            }
                            try{
                                Console.Write("\nChoose Table : ");
                                 tab = Int32.Parse(Console.ReadLine());
                                
                                TableNumbers table1 = obl.GetTableByNumberr(tab);
                                if(table1.TableStatus == TableStatus.NO_EMPTY)
                                {  Order order1 = new Order();
                                    Console.WriteLine("Update Order");
                                    order1 = obl.GetOrderByTableAndStatus(tab,OrderStatus.ORDER_INPROGRESS);
                                    
                                     do{
                                         
                                        int itemID = 0;
                                        bool check = true;
                                        Item i = null;
                                        do{
                                            try{
                                                lst = ibl.GetAll();
                                                Console.WriteLine(" +------------------------------------------------------+");
                                                Console.WriteLine(" | ID | Name Item                 | Price    | Quantity |");
                                                Console.WriteLine(" +------------------------------------------------------+");
                                                
                                                foreach(Item it in lst)
                                                {
                                                Console.WriteLine(" | {0,-2} | {1,-25} | {2,-6} | {3,-8} |", it.ItemId, it.ItemName, String.Format(money, "{0:c}", it.ItemPrice), it.ItemQuantity);
                                                }
                                                Console.WriteLine(" +------------------------------------------------------+");
                                                Console.Write("Input id item want to order : ");
                                                itemID = Int32.Parse(Console.ReadLine());
                                                i = ibl.SearchById(itemID);
                                                Console.WriteLine(i.ItemName + ": " +String.Format(money, "{0:c}", i.ItemPrice));
                                                if(i == null)
                                                {
                                                    Console.WriteLine("Don't have id Item : "+itemID);
                                                    Console.WriteLine("Input Again ....");
                                                    check = false;
                                                }
                                    
                                                else{
                                                   
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
                                            bool checkitem = false;
                                            
                                            try{
                                                Console.Write("Input quantity: ");
                                                quantity = Int32.Parse(Console.ReadLine());
                                                if(i.ItemQuantity < quantity)
                                                {
                                                    Console.WriteLine("The quantity you need in stock is out");
                                                    checkQuantity = false;
                                                }
                                                else if(quantity <= 0)
                                                {
                                                     Console.WriteLine("The quantity > 0");
                                                    checkQuantity = false;
                                                }
                                                else{
                                        
                                                   if(obl.CheckItemInList(order1,itemID)){
                                                       obl.UpdateQuantity(quantity,order1.OrderId,itemID);
                                                       checkitem = true;
                                                   }
                                                    if(!checkitem)
                                                    {   
                                                        order1.ListItem.Add(i);
                                                        i.ItemQuantity = quantity;
                                                    }
                                                    checkQuantity = true;
                                                }
                                            }catch{
                                                Console.WriteLine("Your choice is wrong!");
                                                Console.WriteLine("Input Again ....");
                                                continue;
                                            }
                                        }while(checkQuantity != true);
                                            Console.Write("Do you want to be continue ? (Y/N) : ");
                                            c = Convert.ToChar(Console.ReadLine());
                                    }while (c == 'y' || c == 'Y');
                                    Console.WriteLine("Update Order: " + (obl.UpdateOrder(order1) ? "completed!" : "completed")); 
                                }
                                else{
                                    Console.WriteLine("Create New Order");
                                   order.Table = table1; 
                                   checkTabEmpty = true;
                                    // insert cashierinfo
                                    order.CashierInfo = cashier;
                                //insert item
                                    do{
                                        int itemID = 0;
                                        bool check = true;
                                        Item i = null;
                                        do{
                                            try{
                                                lst = ibl.GetAll();
                                                Console.WriteLine(" +------------------------------------------------------+");
                                                Console.WriteLine(" | ID | Name Item                 | Price    | Quantity |");
                                                Console.WriteLine(" +------------------------------------------------------+");
                                                
                                                foreach(Item it in lst)
                                                {
                                                Console.WriteLine(" | {0,-2} | {1,-25} | {2,-6} | {3,-8} |", it.ItemId, it.ItemName, String.Format(money, "{0:c}", it.ItemPrice), it.ItemQuantity);
                                                }
                                                Console.WriteLine(" +------------------------------------------------------+");
                                                Console.Write("Input id item want to order : ");
                                                itemID = Int32.Parse(Console.ReadLine());
                                                i = ibl.SearchById(itemID);
                                                Console.WriteLine(i.ItemName + ": " +String.Format(money, "{0:c}", i.ItemPrice));
                                               
                                                if(i == null)
                                                {
                                                    Console.WriteLine("Don't have id Item : "+itemID);
                                                    Console.WriteLine("Input Again ....");
                                                    check = false;
                                                }
                                                else{
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
                                            
                                            int j = 0;
                                            bool checkitem = false;
                                            try{
                                                Console.Write("Input quantity: ");
                                                quantity = Int32.Parse(Console.ReadLine());
                                                if(i.ItemQuantity < quantity)
                                                {
                                                    Console.WriteLine("The quantity you need in stock is out");
                                                    checkQuantity = false;
                                                }
                                                else if(quantity <= 0)
                                                {
                                                     Console.WriteLine("Quantity must to > 0..!");
                                                     checkQuantity = false;
                                                }
                                                else{
                                                    for(j = 0;j < order.ListItem.Count;j++)
                                                    {
                                                        if(itemID == order.ListItem[j].ItemId)
                                                        {
                                                            checkitem = true;
                                                            order.ListItem[j].ItemQuantity += quantity;

                                                        }
                                                    }
                                                    if(!checkitem)
                                                    {   
                                                        order.ListItem.Add(i);
                                                        i.ItemQuantity = quantity;
                                                    }
                                                    checkQuantity = true;
                                                }
                                                
                                            }catch{
                                                Console.WriteLine("Your choice is wrong!");
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
                                        }
                                    }
                                    catch{
                                        Console.WriteLine("your choose is wrong! Please Choose again.");
                                       break;
                                    }
                          }while( checkTabEmpty != true || tab == 0);
                        // insert cashierinfo
                    break;
                    case 3:
                        Console.WriteLine(logo3);
                        Console.WriteLine(line);
                        List<TableNumbers> ltb1 ; 
                        Order orderInfo;
                        
                        // do{ 
                        int tabl = 0;
                            ltb1 = obl.GetAllTableByStatus(OrderStatus.ORDER_INPROGRESS);
                            Order orders = new Order();
                            Console.WriteLine("The Tables is booked :     ");
                                foreach(TableNumbers t in ltb1){
                                            Console.Write("["+t.TableNumber+"]"+"\t");
                            }
                            bool checkTab = true;
                            do{
                                try{
                                        Console.Write("\nChoose Table Or enter '0' to exit : ");
                                        tabl = Int32.Parse(Console.ReadLine());
                                        TableNumbers table2 = obl.GetTableByNumberr(tabl);
                                        if(table2.TableStatus == 1)
                                        {
                                            Console.WriteLine("The table is not booked!");
                                            checkTab = false;
                                        }
                                        else
                                        {
                                            orders.Table = table2;
                                            checkTab = true;
                                            orderInfo = obl.GetOrderByTableAndStatus(tabl,OrderStatus.ORDER_INPROGRESS);
                                    if(orderInfo != null)
                                    {
                                        Console.WriteLine("===========================================================");
                                        Console.WriteLine("|                        INVOICE                          |");                      
                                        Console.WriteLine("===========================================================");
                                        Console.WriteLine("| OrderId : "+ orderInfo.OrderId+"                                            |");
                                        Console.WriteLine("| Table : "+ orderInfo.Table.TableNumber+ "    Date Time: "+ orderInfo.OrderDate+"             |");
                                    }
                                
                                List<OrderDetail> od = obl.GetOrderDetailByOrderId(orderInfo.OrderId);   
                                double total = 0,totalMoney = 0;
                                if(od != null){
                                    Console.WriteLine("+---------------------------------------------------------+");
                                    Console.WriteLine("| Name Item          | Price    | Quantity | Total        |");
                                    Console.WriteLine("+---------------------------------------------------------+");
                                    foreach(var i in od)
                                    {
                                        Console.WriteLine("| {0,-18} | {1,-6} | {2,-8} | {3,-11}  |", i._ItemName, String.Format(money, "{0:c}", i._ItemPrice), i._ItemQuantity, String.Format(money ,"{0:c}", total = (i._ItemQuantity*i._ItemPrice)));
                                        Console.WriteLine("+---------------------------------------------------------+");
                                        totalMoney += total;
                                    }
                                
                                    Console.WriteLine("| Total Money                              | {0,-12} |",String.Format(money, "{0:c}", totalMoney));;
                                    Console.WriteLine("+---------------------------------------------------------+");
                                }
                           
                                Console.WriteLine("1. Payment");
                                Console.WriteLine("2. Back to Main Menu");
                                int choice = 0;
                                do
                                {
                                    Console.Write("Your Choice :");
                                    try{
                                        choice = Int16.Parse(Console.ReadLine());
                                    
                                
                                switch(choice)
                                {
                                case 1:
                                
                                        
                                        Console.WriteLine("===========================================================");
                                        Console.WriteLine("|                     Coffee Tuan Ha                      |");
                                        Console.WriteLine("===========================================================");
                                        Console.WriteLine("|                        INVOICE                          |");                      
                                        Console.WriteLine("===========================================================");
                                        Console.WriteLine("| ID: " + orderInfo.CashierInfo.CashierId+"                                                   |");
                                        Console.WriteLine("| Name Cashier: " + orderInfo.CashierInfo.CashierName    +"     Phone: "+ orderInfo.CashierInfo.Phone+"     |");
                                        Console.WriteLine("| OrderId : "+ orderInfo.OrderId+"                                            |");
                                        Console.WriteLine("| Table : "+ orderInfo.Table.TableNumber+ "    Date Time: "+ orderInfo.OrderDate+"             |");
                                        Console.WriteLine("+---------------------------------------------------------+");
                                        Console.WriteLine("| Name Item          | Price    | Quantity | Total        |");
                                        Console.WriteLine("+---------------------------------------------------------+");
                                        foreach(var i in od)
                                        {
                                            Console.WriteLine("| {0,-18} | {1,-6} | {2,-8} | {3,-11}  |", i._ItemName, String.Format(money, "{0:c}", i._ItemPrice), i._ItemQuantity, String.Format(money ,"{0:c}", total = (i._ItemQuantity*i._ItemPrice)));
                                            Console.WriteLine("+---------------------------------------------------------+");
                                          
                                        }
                                    
                                        Console.WriteLine("| Total Money                              | {0,-12} |",String.Format(money, "{0:c}", totalMoney));;
                                        Console.WriteLine("+---------------------------------------------------------+");
                                        Console.WriteLine("          Thank you for buying our products ... ");
                                        Console.WriteLine("\n    Press Enter key to back menu...");
                                        Console.ReadLine();
                                        obl.Update(orderInfo.OrderId,orderInfo.Table.TableNumber);
                                break;
                                }
                                }
                                catch{
                                        Console.WriteLine("Your choice is wrong!");
                                        continue;
                                    }
                            }while(choice <=1 && choice > 2);
                                }
                                }
                                catch{
                                    Console.WriteLine("your choose is wrong! Please Choose again.");
                                    break;
                                }
                            }while(checkTab != true || tabl == 0);
                            
                          
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
                    Console.WriteLine("   UserName must be more than 6 characters! Try again ...");
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
                    Console.WriteLine("\n   Password must be more than 6 characters! Try again ...");
                }
            }while(pass.Length < 6);
            return pass;
        }
       
    }
}

