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
        public int SaveImgSndRecord(byte[] bi, byte[]bs, string name)
        {
            try { 
            string cs = @"URI=file:d:\hottots.db";
            SQLiteConnection conn = new SQLiteConnection(cs);
            conn.Open();
            string sqlQuery = "insert into ImgSnd (name,image,sound)values(@a,@i,@s)";
            SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@a", name);
            cmd.Parameters.AddWithValue("@i", bi);
                cmd.Parameters.AddWithValue("@s", bs);
                MessageBox.Show("Record successfully saved.");
            return cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

        }
        public int UpdateImgSndRecord(byte[] bi, byte[] bs, string name)
        {
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "update ImgSnd set [image] = @i , [sound] = @s  where [name] = @n";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@i", bi);
                cmd.Parameters.AddWithValue("@s", bs);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

        }
        public int DeleteImgSndRecord (string name)
        {
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "delete from ImgSnd where name = @n";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@n", name);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                string sqlQuery = "SELECT image FROM ImgSnd where name = @n";
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
        public List<string> getKeys()
        {
            List<string> retList = new List<string>();
            try
            {
                string cs = @"URI=file:d:\hottots.db";
                SQLiteConnection conn = new SQLiteConnection(cs);
                conn.Open();
                string sqlQuery = "SELECT distinct name FROM ImgSnd";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    retList.Add(reader.GetString(0));
                }
                return retList;

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
                string sqlQuery = "SELECT sound FROM ImgSnd where name = @n ";
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
