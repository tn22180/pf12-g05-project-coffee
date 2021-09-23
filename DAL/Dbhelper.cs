using System;
using MySql.Data.MySqlClient;
namespace DAL{
    public class DbHelper{
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection(){
            if(connection == null){
                connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;user id=vtca_pf12;password=tuan2001;port=3306;database=Coffee;"
                };
                
            }
            return connection;
        } 
    }
}