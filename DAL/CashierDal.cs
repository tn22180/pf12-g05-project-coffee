using System;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL {
     public class CashierDal{
             MySqlConnection connection = DbHelper.GetConnection();
             private MySqlDataReader reader;
    
         public int Login(Cashier cashier){
             int login = 0;
             string sql = "select * from Cashiers where userName = @UserName and pass = @Password";
             try{
                 connection.Open();
                 MySqlCommand command = new MySqlCommand(sql, connection);
                 command.Parameters.AddWithValue("@UserName", cashier.username);
                 command.Parameters.AddWithValue("@Password", Md5Algorithms.CreateMD5(cashier.password));
                 MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                        {
                            login = 1;
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
                string sql = "select * from Cashiers where cashier_id = @CashierId;";
                reader = (new MySqlCommand(sql, connection)).ExecuteReader();
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
             cashier.username = reader.GetString("userName");
             cashier.password = reader.GetString("pass");
             cashier.cashier_id = reader.GetInt32("cashier_id");
             cashier.cashier_name = reader.GetString("cashier_name");
             cashier.phone = reader.GetString("phone");
             cashier.address = reader.GetString("address");
             return cashier;
         }
            
        }
    }