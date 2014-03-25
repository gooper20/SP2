using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SP2
{
    class DBHandler
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBHandler()
        {
            Initialize();
        }

        //Initializes values
        private void Initialize()
        {
            server = "db4free.net";
            database = "stickypay2";
            uid = "gooper20";
            password = "Gooper19!";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        /// <summary>
        /// Insert statement
        /// "INSERT INTO 'table' ('places') VALUES('values')"
        /// "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')"
        /// </summary>
        /// <param name="tableinfo">name, age</param>
        /// <param name="values">'John Smith', '33'</param>
        public void Insert(string table, string places, string values)
        {
            string query = "INSERT INTO " + table + " (" + places + ") VALUES(" + values + ")";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        /// <summary>
        /// "UPDATE 'table' SET 'values' WHERE 'where'"
        /// "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'"
        /// </summary>
        /// <param name="table">tableinfo</param>
        /// <param name="values">name = 'Joe', age='22'</param>
        /// <param name="where">name='John Smith'</param>
        //Update statement
        public void Update(string table, string values, string where)
        {
            string query = "UPDATE " + table + " SET " + values + " WHERE " + where;
            
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                
                //Assign the query using CommandText
                cmd.CommandText = query;
                
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }

        }
        /// <summary>
        /// "DELETE FROM tableinfo WHERE name='John Smith'"
        /// "DELETE FROM 'table' WHERE 'where'";
        /// </summary>
        //Delete statement
        public void Delete(string table, string where)
        {
            string query = "DELETE FROM " + table + " WHERE " + where;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List <string> [] Select()
        {

        }

        //Count statement
        public int Count()
        {
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
}

