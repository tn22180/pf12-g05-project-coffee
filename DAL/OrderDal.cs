using System;
using System.Collections.Generic;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL{
     public class OrderDal{
         private MySqlConnection connection = DbHelper.GetConnection();
         public bool CreateOrder(Order order)
            {
             if (order == null || order.listItem == null || order.listItem.Count == 0)
                {
                    return false;
                }
            bool result = false;
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "lock tables Cashier write, Orders write, Items write, OrderDetails write, Coffee_Tables write;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader = null;
                try{  //get table_number
                    command.CommandText = "select table_number from Coffee_Tables where table_status = 1 and table_number = @number;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@number", order.table);
                    reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        order.table = GetTable(reader);
                    }
                         // update number_table
                        command.CommandText = "update Coffee_Table set table_status = @number where table_number = "+order.table+";";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@number", TableStatus.NO_EMPTY);
                        command.ExecuteNonQuery();

                    // get cashier_info
                    command.CommandText = "select * from Cashiers where cashier_id=@cashierId;";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@cashierId", order.cashierInfo.cashier_id);
                    reader = command.ExecuteReader();
                    if(reader.Read()){
                        order.cashierInfo =new CashierDal().GetCashier(reader);
                    }
                    reader.Close();
                    // insert order
                    command.CommandText = "insert into Orders(table_number,cashier_id,order_status) values (@number,@cashierID,@orderStatus);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@number", order.table);
                    command.Parameters.AddWithValue("@cashierID", order.cashierInfo.cashier_id);
                    command.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
                    // command.Parameters.AddWithValue("@customerName", order.cashierInfo.cashier_name);
                    // command.Parameters.AddWithValue("@customerPhone", order.cashierInfo.phone);
                    command.ExecuteNonQuery();
                    command.CommandText = "select LAST_INSERT_ID() as order_id;";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        order.order_id = reader.GetInt32("order_id");
                    }
                    reader.Close();
                    foreach(var item in order.listItem)
                    {
                        if(item.item_id == null || item.item_quantity <= 0)
                        {
                            throw new Exception("Not Exists Item");
                        }
                        command.CommandText = "select item_price from Items where item_id = itemID";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@itemID", item.item_id);
                        reader = command.ExecuteReader();
                        if(!reader.Read())
                        {
                            throw new Exception("Not Exists Item");
                        }
                        item.item_price = reader.GetDouble("item_price");
                        reader.Close();
                    // insert to order detail
                        command.CommandText = @"insert into OrderDetails(order_id,item_id,item_price,quantity) values (" + order.order_id + ", " + item.item_id + ", " + item.item_price + ", " + item.item_quantity + ");";
                        command.ExecuteNonQuery();
                    //update quantity of Items
                        command.CommandText = "update Items set item_quantity = item_quantity - @quantity where item_id = " + item.item_id + ";";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@quantity", item.item_quantity);
                        command.ExecuteNonQuery();
               
                    }
                    trans.Commit();
                    result = true;

                }
                catch{
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
            catch { }
            finally
            {
                connection.Close();
            }
            return result; 
        }
            internal TableNumber GetTable(MySqlDataReader reader)
            {   
             TableNumber tab = new TableNumber();
             tab.table_number = reader.GetInt32("table_number");
             return tab;
            }
        }
     }
 