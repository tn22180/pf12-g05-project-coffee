using System;
using Persistence;
using MySql.Data.MySqlClient;
 namespace DAL {
     public class CashierDal{
         public int Login(Cashier cashier){
             int login = 0;
             string sql = "select * from Cashiers where userName = @UserName and pass = @Password";
             MySqlConnection connection = DbHelper.GetConnection();
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
        }
    }