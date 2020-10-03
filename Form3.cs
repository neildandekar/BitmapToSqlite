using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapToSqlite
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteDatabaseOperations sdb = new SQLiteDatabaseOperations();
            byte[] img_arr1 = sdb.getImage();
            MemoryStream ms1 = new MemoryStream(img_arr1);
            ms1.Seek(0, SeekOrigin.Begin);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms1);
            pictureBox1.Image = image;
        }
    }
}
