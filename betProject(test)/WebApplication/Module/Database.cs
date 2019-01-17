using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

namespace betProject.module
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
            string db = "gdc3_4";
            string port = "5002";

            string connStr = string.Format("server={0};uid={1};password={2};database={3};port={4};", host, user, password, db, port);
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

        public bool NonQuery(string sql, Hashtable ht)
        {
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    foreach (DictionaryEntry data in ht)
                    {
                        comm.Parameters.AddWithValue(data.Key.ToString(), data.Value);
                    }
                    comm.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public MySqlDataReader Reader2(string sql, Hashtable ht)
        {
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    foreach (DictionaryEntry data in ht)
                    {
                        comm.Parameters.AddWithValue(data.Key.ToString(), data.Value);
                    }
                    return comm.ExecuteReader();
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }
        public MySqlDataReader Reader(string sql)
        {
            if (status)
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    return comm.ExecuteReader();
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }

        public bool ReaderClose(MySqlDataReader reader)
        {
            try
            {
                reader.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}