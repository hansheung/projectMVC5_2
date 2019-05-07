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

        public ConDB()
        {
            //myConnection = new MySqlConnection();
            //myConnection.Open();

            connection_open = false;

            connection = new MySqlConnection();
            //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

            //            if (db_manage_connnection.DB_Connect.OpenTheConnection(connection))
            //if (Open_Local_Connection())
            //{
            //    connection_open = true;
            //}
            //else
            //{
            //    				//MessageBox::Show("No database connection connection made...\n Exiting now", "Database Connection Error");
            //    				//	 Application::Exit();
            //}
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

        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}