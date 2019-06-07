using BRO.Models;
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
        static string sMySQLConn;

        public ConDB(string sDatabase)
        {
            if (sDatabase == "bronet")
            {
                sMySQLConn = "MySQLConn1";
            }
            else
            {
                sMySQLConn = "MySQLConn2";
            }
        }

        //public void save(string sTable, PasswordModel viewModel)
        //{
        //    foreach (string Fieldname in sFieldName)
        //    {

        //    }
        //}

        public void execute(string sSQL)
        {
            string constr = ConfigurationManager.ConnectionStrings[sMySQLConn].ConnectionString;
            MySqlConnection MyConn = new MySqlConnection(constr);
            MySqlCommand MyCommand = new MySqlCommand(sSQL, MyConn);
            MySqlDataReader MyReader;
            MyConn.Open();
            MyReader = MyCommand.ExecuteReader();
            MyConn.Close();
        }

        public MySqlDataReader ExecuteReader(string sSQL) //=== Correct
        {
            string constr = ConfigurationManager.ConnectionStrings[sMySQLConn].ConnectionString;
            MySqlConnection MyConn = new MySqlConnection(constr);
            MySqlCommand command = new MySqlCommand(sSQL, MyConn);
            command.Connection.Open();
            MySqlDataReader dr = command.ExecuteReader();
            return dr;
        }

        public DataTable GetData(string sSQL)
        {
            string constr = ConfigurationManager.ConnectionStrings[sMySQLConn].ConnectionString;
            MySqlConnection MyConn = new MySqlConnection(constr);
            MySqlCommand command = new MySqlCommand(sSQL, MyConn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void ExecuteQuery(string myExecuteQuery)  //=== Correct
        {
            //connection.Close();
            string constr = ConfigurationManager.ConnectionStrings["MySQLConn1"].ConnectionString;
            MySqlConnection MyConn = new MySqlConnection(constr);
            MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, MyConn);
            myCommand.Connection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();
        }

        public void ExeQuery(string myExecuteQuery)
        {
            //MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, connection);
            //myCommand.Connection.Open();
            //myCommand.ExecuteNonQuery();
            //myCommand.Connection.Close();
        }

       

        //public MySqlDataReader ExeReader(string sSQL)
        //{
        //    //MySqlCommand command = new MySqlCommand(sSQL, connection);
        //    //command.Connection.Open();
        //    //MySqlDataReader drpath = command.ExecuteReader();
        //    //return drpath;
        //}

        private bool Open_Local_Connection()
        {
            try
            {
                //connection.Open();
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
                //connection.Close();
                
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error Connection Close: " + e);
                
            }
        }
    }
}