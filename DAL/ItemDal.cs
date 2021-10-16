using System;
using Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
 namespace DAL 
 {  
     public static class ItemFilter
     {
         public const int GET_ALL = 0;
         public const int FILTER_BY_ITEM_NAME = 1;
     }
     public class ItemDal
     {
        string sql;
        MySqlConnection connection = DbHelper.GetConnection();
        
        public Item SearchById(int id)
        {
             Item item = null;
             try{
                    connection.Open();
                    sql = @"select * from Items where item_id = @ID;";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                        {   
                            item = GetItem(reader);
                        }    
                        reader.Close(); 
                }
            catch{
            }
            finally{
                connection.Close();
            }
            return item;
         }
         internal Item GetItem(MySqlDataReader reader)
         {   
             Item item = new Item();
             item.ItemId = reader.GetInt32("item_id");
             item.ItemName = reader.GetString("item_name");
             item.ItemPrice = reader.GetDouble("item_price");
             item.ItemQuantity = reader.GetInt32("item_quantity");
             item.ItemDescription = reader.GetString("item_description");
             return item;
         }
            public List<Item> GetItems(int itemFilter,Item item)
            {
                List<Item> lst = null;
             try{
                 connection.Open();
                 MySqlCommand command = new MySqlCommand("", connection);
                 switch(itemFilter)
                    {
                        case ItemFilter.GET_ALL:
                        sql = @"select * from Items;";
                        break;
                        case ItemFilter.FILTER_BY_ITEM_NAME:
                        sql = @"select * from Items where item_name like concat('%',@itemName,'%');";
                        command.Parameters.AddWithValue("@itemName", item.ItemName);
                        break;
                    }
                    command.CommandText = sql;
                    MySqlDataReader reader = command.ExecuteReader();
                    lst = new List<Item>();
                    while(reader.Read())
                    {
                        lst.Add(GetItem(reader));
                    }
                    reader.Close();
                    }
                catch{}
                finally{
                        connection.Close();
                }
                return lst;
            }
        
        }
        
    }