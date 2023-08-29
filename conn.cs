using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;


namespace bims
{
    public class conn
    {
        string username;
        string pwd;
        string server;
        string db;
        public MySqlConnection connection;


        public conn()
        {
            server = "localhost";
            db = "book_store";
            username = "root";
            pwd = "";

            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + db + ";"+ "UID=" + username + ";" + "PASSWORD=" + pwd + ";";

            connection = new MySqlConnection(connectionString);
        } 
        public bool openConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        MessageBox.Show("Server not found. Contact Administator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid Credentials");
                        break;
                }
            }
            return false;


        }

        public bool closeConnection()
        {

             try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
               
               MessageBox.Show("Connection close failed!");
             
            }
             return false;
           
        }

    }
}
