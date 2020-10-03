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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog fd1 = new OpenFileDialog();
        private void button1_Click(object sender, EventArgs e)
        {
            fd1.Filter = "image files|*.jpg;*.png;.*gif;*.icon;.*;";
            DialogResult dres2 = fd1.ShowDialog();
            if (dres2 == DialogResult.Abort)
                return;
            if (dres2 == DialogResult.Cancel)
                return;
            textBox4.Text = fd1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.label6.Text = textBox1.Text.ToString();
            f2.label7.Text = textBox2.Text.ToString();
            f2.pictureBox1.Image = Image.FromFile(fd1.FileName);
            MemoryStream ms1 = new MemoryStream();
            f2.pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            //MessageBox.Show(ms1.Length.ToString()); calculate image length
            if (ms1.Length > 25600 )
                MessageBox.Show("Please upload less than 25 kb image and less than 12 kb Signature");
            else
                f2.Show();
        }
    }
}
