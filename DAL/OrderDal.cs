using System;
using System.Collections.Generic;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL{
     public class OrderDal
    {
         private MySqlConnection connection = DbHelper.GetConnection();
         public bool CreateOrder(Order order)
            {
             if (order == null || order.ListItem == null || order.ListItem.Count == 0)
                {
                    return false;
                }
            bool result = false;
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "lock tables Cashiers write, Orders write, Items write, OrderDetails write, Coffee_Tables write;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader = null;
                if (order.CashierInfo == null || order.CashierInfo.CashierName == null || order.CashierInfo.CashierName == "")
                    {
                    order.CashierInfo = new Cashier(){CashierId = 1};
                    }
                try{  //get table_number
                    command.CommandText = "select table_number from Coffee_Tables where table_status = 1 and table_number = @number;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@number", order.Table);
                    reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        order.Table = GetTable(reader);
                    }
                    reader.Close();
                    //update table
                    command.CommandText = "update Coffee_Tables set table_status = 2 where table_number = @number_table;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@number_table", order.Table.TableNumber);
                    // command.Parameters.AddWithValue("@number", TableStatus.NO_EMPTY);
                    command.ExecuteNonQuery();
                    
                    // get cashier_info
                    
                    command.CommandText = "select * from Cashiers where cashier_id = @cashierId;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@cashierId", order.CashierInfo.CashierId);
                    reader = command.ExecuteReader();
                    if(reader.Read()){
                        order.CashierInfo = new CashierDal().GetCashier(reader);
                    }
            
                    reader.Close();
                    // insert order
                    command.CommandText = "insert into Orders(table_number,cashier_id,order_status) values (@number,@cashierID,@orderStatus);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@number", order.Table.TableNumber);
                    command.Parameters.AddWithValue("@cashierID", order.CashierInfo.CashierId);
                    command.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
                    command.ExecuteNonQuery();
                    // get new Order Id
                    command.CommandText = "select LAST_INSERT_ID() as order_id;";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("order_id");
                    }
                    reader.Close();
                    foreach(var item in order.ListItem)
                    {
                        if(item.ItemId == null || item.ItemQuantity <= 0)
                        {
                            throw new Exception("Not Exists Item");
                        }
                        command.CommandText = "select item_price from Items where item_id = @itemID";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@itemID", item.ItemId);
                        reader = command.ExecuteReader();
                        if(!reader.Read())
                        {
                            throw new Exception("Not Exists Item");
                        }
                        item.ItemPrice = reader.GetDouble("item_price");
                        reader.Close();
                    // insert to order detail
                        command.CommandText = @"insert into OrderDetails(order_id,item_id,item_price,quantity) values (" + order.OrderId + ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.ItemQuantity + ");";
                        command.ExecuteNonQuery();
                    //update quantity of Items
                        command.CommandText = "update Items set item_quantity = item_quantity - @quantity where item_id = " + item.ItemId + ";";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@quantity", item.ItemQuantity);
                        command.ExecuteNonQuery();
               
                    }
                        // get time
                        command.CommandText = "select order_date from Orders where order_id = @OrderId;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@OrderId", order.OrderId);
                        reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            order.OrderDate = reader.GetDateTime("order_date");
                        }
                        reader.Close();
                        //update order_status
                        command.CommandText = "update Orders set order_status = 2 where order_id = @OrId;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@OrId", order.OrderId);
                        command.ExecuteNonQuery();

                        //update number_table
                        // command.CommandText = "update Coffee_Tables set table_status = 1 where table_number = @number_table;";
                        // command.Parameters.Clear();
                        // command.Parameters.AddWithValue("@number_table", order.Table.TableNumber);
                        // // command.Parameters.AddWithValue("@number", TableStatus.NO_EMPTY);
                        // command.ExecuteNonQuery();
                    trans.Commit();
                    result = true;

                }
                catch(Exception e){
                    Console.WriteLine(e);
                    try
                    {   
                        
                        trans.Rollback();
                    }
                    catch { }
                }
                finally{
                    command.CommandText = "unlock tables;";
                    command.ExecuteNonQuery();   
               }
            }
            catch(Exception e) {
                Console.WriteLine(e);
             }
            finally
            {
                connection.Close();
            }
            return result; 
        }
            internal TableNumbers GetTable(MySqlDataReader reader)
            {   
             TableNumbers tab = new TableNumbers();
             tab.TableNumber = reader.GetInt32("table_number");
             tab.TableStatus = reader.GetInt32("table_status");
             return tab;
            }
            public List<TableNumbers> GetTables(TableNumbers tab)
            {
                List<TableNumbers> lst = null;
                try{
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = "select * from Coffee_Tables where table_status = @status;";
                    command.Parameters.AddWithValue("@status",tab.TableStatus);
                    MySqlDataReader reader = command.ExecuteReader();
                    lst = new List<TableNumbers>();
                    while(reader.Read())
                    {
                        lst.Add(GetTable(reader));
                    }
                    
                    reader.Close();
                    }
                catch{}
                finally{
                        connection.Close();
                }
                return lst;

                
            }
               public TableNumbers GetTableByNumber(int num)
        {
             TableNumbers tab = null;
             try{
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = @"select * from Coffee_Tables where table_number = @num;";
                    command.Parameters.AddWithValue("@num", num);
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                        {   
                            tab = GetTable(reader);
                        }    
                        reader.Close(); 
                }
            catch{
            }
            finally{
                connection.Close();
            }
            return tab;
            }
             internal Order GetOrder(MySqlDataReader reader)
            { 
              Order order = new Order() ;
                order.Table = new TableNumbers();
                order.ListItem = new List<Item>();
                order.CashierInfo = new Cashier();
                order.OrderId = reader.GetInt32("order_id");
                order.OrderDate = reader.GetDateTime("order_date");
                order.CashierInfo.CashierId = reader.GetInt32("cashier_id");
                order.CashierInfo.CashierName = reader.GetString("cashier_name");
                order.CashierInfo.Phone = reader.GetString("phone");
                order.Table.TableNumber = reader.GetInt32("table_number");
                order.OrderStatus = reader.GetInt32("order_status");
             return order;
             }
             internal OrderDetail GetOrderDetail(MySqlDataReader reader)
             {
                 OrderDetail ord = new OrderDetail();
                 ord._ItemName = reader.GetString("item_name");
                 ord._ItemPrice = reader.GetDouble("item_price");
                 ord._ItemQuantity = reader.GetInt32("quantity");
                 return ord;
             }
             

             public List<Order> GetAllOrder()
             {
                 List<Order> lso =null;
                 try{
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText= "select * from Orders;" ;
                    command.Parameters.Clear();
                    MySqlDataReader reader = command.ExecuteReader();
                    lso = new List<Order>();
                    while(reader.Read())
                    {
                       lso.Add(GetOrder(reader));
                    }
                    reader.Close();
                    }
                catch(Exception e){
                    Console.WriteLine(e);
                    }
            
                finally{
                    connection.Close();
                    }
                return lso;
            }
             public Order GetOrderByTable( int tab, int sta)
             {
                 Order order = new Order();
                 try{
                     connection.Open();
                     MySqlCommand command = connection.CreateCommand();
                     command.CommandText = "select * from Cashiers,Orders where Cashiers.cashier_id = Orders.cashier_id and  Orders.order_status = @sta and Orders.table_number = @tab;";
                    command.Parameters.Clear();
                    // command.Parameters.AddWithValue("@id",id);
                    command.Parameters.AddWithValue("@sta",sta);
                    command.Parameters.AddWithValue("@tab",tab);
                    MySqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        order = GetOrder(reader);
                    }
                        reader.Close();
                

                    // update Tables;
                    command.CommandText = "update Orders set order_status = 1 where order_id = @id";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", order.OrderId);
                    command.ExecuteNonQuery();


                    //update number_table
                    command.CommandText = "update Coffee_Tables set table_status = 1 where table_number = @number_table;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@number_table", order.Table.TableNumber);
                        // command.Parameters.AddWithValue("@number", TableStatus.NO_EMPTY);
                     command.ExecuteNonQuery();
                 }  
                catch(Exception e){
                    Console.WriteLine(e);
                }
                finally{
                    connection.Close();
                }
                 return order;
             }
            public List<OrderDetail> GetOrderDetailById(int Id)
             {
                 List<OrderDetail> ld = null;
                //  OrderDetail orders = new OrderDetail();
                 try{
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText= "select OrderDetails.order_id,OrderDetails.item_id,OrderDetails.item_price,OrderDetails.quantity,Items.item_name,Orders.cashier_id,Orders.table_number,Orders.order_date,Orders.order_status from OrderDetails,Orders,Items where OrderDetails.item_id = Items.item_id and OrderDetails.order_id = Orders.order_id and Orders.order_id = @id;" ;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", Id);
                    MySqlDataReader reader = command.ExecuteReader();
                    ld = new List<OrderDetail>();
                    while(reader.Read())
                    {
                        ld.Add(GetOrderDetail(reader));
                    }
                    reader.Close();
                    }
                catch(Exception e){
                    Console.WriteLine(e);
                    }
            
                finally{
                    connection.Close();
                    }
                return ld;
            }
            
            
        }
    }
 