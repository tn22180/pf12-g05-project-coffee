using System;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL {
     public class CashierDal
     {
             MySqlConnection connection = DbHelper.GetConnection();
             private MySqlDataReader reader;
    
         public int Login(Cashier cashier)
         {
             int login = 0;
             string sql = "select * from Cashiers where userName = @UserName and pass = @Password";
             try{
                 connection.Open();
                 MySqlCommand command = new MySqlCommand(sql, connection);
                 command.Parameters.AddWithValue("@UserName", cashier.Username);
                 command.Parameters.AddWithValue("@Password", Md5Algorithms.CreateMD5(cashier.Password));
                 reader = command.ExecuteReader();
                    if(reader.Read())
                        {
                            login = reader.GetInt32("role");
                        }
                    reader.Close();
                    connection.Close();
                }
            catch{
                login = -1;
                }
            finally{
                connection.Close();
                }
            return login;
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
                catch { }
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
             return cashier;
            }
    }
 }