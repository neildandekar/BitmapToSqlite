using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapToSqlite
{
    class SQLiteDatabaseOperations
    {
        public SQLiteDatabaseOperations()
        {
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "SELECT * FROM thsNames";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);                                
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<String> records = new List<String>();
                 while (reader.Read())
                {
                    records.Add(reader.GetInt32(0).ToString() + "-" + reader.GetString(1));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public int SaveImageRecord(byte[] ba, string name)
        {
            try { 
            string cs = @"URI=file:d:\hottots.db";
            SQLiteConnection conn = new SQLiteConnection(cs);
            conn.Open();
            string sqlQuery = "insert into Images (name,image)values(@a,@d)";
            SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@a", name);
            cmd.Parameters.AddWithValue("@d", ba);
            return cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }
        public int SaveSoundRecord(byte[] ba,string name)
        {
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "insert into Sounds (sound,name)values(@s,@n)";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@s", ba);
                cmd.Parameters.AddWithValue("@n", name);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }
        public byte[] getImage(string key)
        {
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "SELECT image FROM Images where name = @n";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@n", key);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<byte[]> records = new List<byte[]>();
                while (reader.Read())
                {
                    byte[] imageArray = new byte[25000]; 
                    reader.GetBytes(0,0,imageArray,0,25000);
                    records.Add(imageArray);
                    
                }
                return records[0];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public byte[] getSound(string key)
        {
         try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "SELECT sound FROM sounds where name = @n ";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@n", key);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<byte[]> records = new List<byte[]>();
                while (reader.Read())
                {
                    byte[] soundArray = new byte[60000];
                    reader.GetBytes(0, 0, soundArray, 0, 60000);
                    records.Add(soundArray);
                }
                return records[0];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
}

    }
}
