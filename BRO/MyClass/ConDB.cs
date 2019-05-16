using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;


namespace BRO.MyClass
{
    public class ConDB
    {
        private bool connection_open;
        private MySqlConnection connection;

        public ConDB(string sMySQLConn)
        {
            connection_open = false;

            connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[sMySQLConn].ConnectionString;
            
            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {
                Console.Error.WriteLine("Error - Database Connection Error");
            }
        }

        public DataTable GetData(string sSQL)
        {
            MySqlCommand command = new MySqlCommand(sSQL, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void ExecuteQuery(string sSQL)
        {
            MySqlCommand command = new MySqlCommand("", connection);
            command.Connection = connection;
            command.CommandText = sSQL;
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader(string sSQL)
        {
            MySqlCommand command = new MySqlCommand(sSQL, connection);
            MySqlDataReader dr = command.ExecuteReader();
            return dr;
        }

        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error Connection Open: " + e);
                return false;
            }
        }

        public void Close()
        {
            try
            {
                connection.Close();
                
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error Connection Close: " + e);
                
            }
        }
    }
}