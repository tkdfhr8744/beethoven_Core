using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Database
    {
        private MySqlConnection conn;
        public bool status;

        public Database()
        {
            status = Connection();
        }

        private bool Connection()
        {
            string host = "gudi.kr";
            string user = "gdc3";
            string password = "gdc3";
            string port = "5002";
            string db = "gdc3_4";

            string connStr = string.Format("server={0};uid={1};password={2};port={3};database={4}", host, user, password, port, db);
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                this.conn = conn;
                return true;
            }
            catch
            {
                conn.Close();
                this.conn = null;
                return false;
            }
        }

        public bool ConnectionClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool NonQuery(string sql)
        {
            try
            {
                if (status)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    comm.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public MySqlDataReader Reader(string sql)
        {
            try
            {
                if (status)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    return comm.ExecuteReader();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public void ReaderClose(MySqlDataReader reader)
        {
            reader.Close();
        }
    }
}
