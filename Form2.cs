using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapToSqlite
{
    public partial class Form2 : Form
    {
        private byte[] img_arr1;
        public Form2()
        {
            InitializeComponent();
        }

        private void  button1_Click(object sender, EventArgs e)
        {
            getMsAsync();
            SQLiteDatabaseOperations sqliteDb = new SQLiteDatabaseOperations();
            sqliteDb.SaveImageRecord(img_arr1);       
            
        }
        private void getMsAsync()
        {
            Image img = pictureBox1.Image ; // or use the PictureEdit.Image property
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                img_arr1 = ms.ToArray();
                //MemoryStream ms1 = new MemoryStream();
                //pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                //img_arr1 = new byte[ms1.Length];
                //int i = await  ms1.ReadAsync(img_arr1, 0, img_arr1.Length);
                //return;
            }
            }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            Form2 f2 = new Form2();
            f2.Hide();
            f3.Show();
        }
    }
}
