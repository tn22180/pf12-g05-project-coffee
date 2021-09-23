using System;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL 
 {
     public class ItemDal{
         public void SearchById(Item item){
             string sql = "select * from Items where item_id = @ItemID";
             MySqlConnection connection = DbHelper.GetConnection();
             
                 connection.Open();
                 MySqlCommand command = new MySqlCommand(sql, connection);
                 command.Parameters.AddWithValue("@ItemID", item.item_id);
                 MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                        {   Console.WriteLine("Item Info");
                            Console.Write($" ID : {reader["item_id"],1}\n Name: {reader["item_name"],3}\n Price: {reader["item_price"],1}\n Quantity: {reader["item_quantity"],1}\n Desription: {reader["item_description"],3}\n");
                        }
                    reader.Close();
                    connection.Close();
                    else{
                          Console.WriteLine("Don't found item");
                          connection.Close();
                    }
            }


            public void SearchByName(Item item){
             MySqlConnection connection = DbHelper.GetConnection();
                 connection.Open();
                 using var command = new MySqlCommand();
                 command.Connection = connection;
                 command.CommandText = "select item_name as Name from Items where item_name like '@ItemName%';";
                 command.Parameters.AddWithValue("@ItemName", item.item_name);
                 using var reader = command.ExecuteReader();
                if(reader.HasRows)
                        { 
                            while(reader.Read())
                            {
                                Console.WriteLine($"{reader["item_name"],3}");
                            }  
                        }  
                        connection.Close(); 
                else
                {
                    Console.WriteLine("Don't found item");
                    connection.Close(); 
                }
            }
        }
        
    }