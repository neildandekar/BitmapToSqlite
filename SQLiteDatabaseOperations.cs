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
        public int SaveImageRecord(byte[] ba)
        {
            try { 
            string cs = @"URI=file:d:\hottots.db";
            SQLiteConnection conn = new SQLiteConnection(cs);
            conn.Open();
            string sqlQuery = "insert into Images (name,qualification,age,image)values(@a,@b,@c,@d)";
            SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@a", "PlaceHolder");
            cmd.Parameters.AddWithValue("@b", "PlaceHolder");
            cmd.Parameters.AddWithValue("@c", "PlaceHolder");
            cmd.Parameters.AddWithValue("@d", ba);
            return cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }
        public byte[] getImage()
        {
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "SELECT image FROM Images";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
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

    }
}
