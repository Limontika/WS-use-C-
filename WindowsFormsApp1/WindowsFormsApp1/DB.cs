using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    class DB
    {

        public static MySqlConnection GetDBConnection()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "mydb";
            string username = "root";
            string password = "";

            return DBMySQLUnits.GetDBConnection(host, port, database, username, password);
        }
    }
}
