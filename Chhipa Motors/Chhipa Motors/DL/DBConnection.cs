using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Chhipa_Motors.DL
{
    public class DBConnection
    {
        private SqlConnection con;

        public DBConnection()
        {
           // string conString = ConfigurationManager.ConnectionStrings["Chhipa_DB"].ConnectionString;

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Chhipa Motors\Chhipa Motors\Chhipa Motors\ChhipaMotors.mdf;Integrated Security=True";
            con = new SqlConnection(conString);
        }

        public SqlConnection Con { get => con; }
        public void OpenConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}