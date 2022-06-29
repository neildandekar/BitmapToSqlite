using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BitmapToSqlite
{
    class SQLiteDatabaseOperations
    {
        private static SQLiteConnection conn;
        public SQLiteDatabaseOperations()
        {

        }
        public static SQLiteConnection getConnection()
        {
            string cs = @"URI=file:d:\hottots.db";
            if (conn == null) conn = new SQLiteConnection(cs);
            conn.Open();
            return conn;

        }
        public int SaveImgSndRecord(byte[] bi, byte[]bs, byte[] bg, string name)
        {
            try {
                if (bi.Length < 1 || bs.Length < 1)
                {

                    MessageBox.Show("Image and/or Sound can not be empty. Record not saved.");
                    return 0;
                }

                string sqlQuery = "insert into ImgSnd (name,image,sound,gif)values(@a,@i,@s,@g)";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
                cmd.Parameters.AddWithValue("@a", name);
                cmd.Parameters.AddWithValue("@i", bi);
                cmd.Parameters.AddWithValue("@s", bs);
                cmd.Parameters.AddWithValue("@g", bg);                
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show("Record successfully saved.");
                return i;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            finally
            {
               if (conn != null) conn.Close();
            }

        }
        public int UpdateImgSndRecord(byte[] bi, byte[] bs, byte[] bg, string name)
        {
            try
            {
                if (bi.Length < 1 || bs.Length < 1)
                {

                    MessageBox.Show("Image and/or Sound can not be empty. Record not saved.");
                    return 0;
                }

                string sqlQuery = "update ImgSnd set [image] = @i , [sound] = @s, [gif] = @g where [name] = @n";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@i", bi);
                cmd.Parameters.AddWithValue("@s", bs);
                cmd.Parameters.AddWithValue("@g", bg);
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            finally
            {
                if (conn != null) conn.Close();
            }

        }
        public int DeleteImgSndRecord (string name)
        {
            try
            {
                string sqlQuery = "delete from ImgSnd where name = '" + name + "'";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            finally
            {
                if (conn != null) conn.Close();
            }

        }
        public byte[] getImage(string key)
        {
            try
            {
                string sqlQuery = "SELECT image FROM ImgSnd where name = @n";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
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
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        public List<string> getKeys()
        {
            List<string> retList = new List<string>();
            try
            {
                string sqlQuery = "SELECT distinct name FROM ImgSnd";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
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
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        public byte[] getGif(string key)
        {
            try
            {
                string sqlQuery = "SELECT gif FROM ImgSnd where name = @n";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
                cmd.Parameters.AddWithValue("@n", key);
                SQLiteDataReader reader = cmd.ExecuteReader();
                List<byte[]> records = new List<byte[]>();
                while (reader.Read())
                {
                    byte[] gifArray = new byte[25000];
                    reader.GetBytes(0, 0, gifArray, 0, 25000);
                    records.Add(gifArray);
                }
                return records[0];

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        public byte[] getSound(string key)
        {
         try
            {
                string sqlQuery = "SELECT sound FROM ImgSnd where name = @n ";
                SQLiteCommand cmd = new SQLiteCommand(sqlQuery, getConnection());
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
            finally
            {
                if (conn != null) conn.Close();
            }
        }
    }
}
