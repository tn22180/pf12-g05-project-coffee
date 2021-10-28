using System;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL {
     public class CashierDal
     {
             MySqlConnection connection = DbHelper.GetConnection();
            //  private MySqlDataReader reader;
    
         public Cashier Login(string username, string pass)
         {
             Cashier cashier = new Cashier();
             try{
                 connection.Open();
                 MySqlCommand command = connection.CreateCommand();
                 command.Connection = connection;
                 command.CommandText = "select * from Cashiers where userName = @UserName and pass = @Password";
                 command.Parameters.AddWithValue("@UserName", username);
                 command.Parameters.AddWithValue("@Password", Md5Algorithms.CreateMD5(pass));
                 MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                        {
                            cashier = GetCashier(reader);
                        }
                    reader.Close();
                }
             catch(Exception e) { 
                    Console.WriteLine(e);
                }
            finally{
                connection.Close();
                }
            return cashier;
        }
        public Cashier GetCashierById(int CashierId)
        {
            Cashier cas = null;
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "select * from Cashiers where cashier_id = @CasID;";
                command.Parameters.AddWithValue("@CasID", CashierId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cas = GetCashier(reader);
                }
                    reader.Close();
                }
                catch(Exception e) { 
                    Console.WriteLine(e);
                }
                finally
                {
                    connection.Close();
                }
                return cas;
        
        }
        internal Cashier GetCashier(MySqlDataReader reader)
            {   
             Cashier cashier = new Cashier();
             cashier.Username = reader.GetString("userName");
             cashier.Password = reader.GetString("pass");
             cashier.CashierId = reader.GetInt32("cashier_id");
             cashier.CashierName = reader.GetString("cashier_name");
             cashier.Phone = reader.GetString("phone");
             cashier.Address = reader.GetString("address");
             cashier.Role = reader.GetInt32("role");
             return cashier;
            }
    }
 }