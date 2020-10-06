using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapToSqlite
{
    public partial class Form2 : Form
    {
        private byte[] img_arr1;
        private byte[] snd_arr1;
        public string key { get; set; }
        public string soundFileName {get;set;}
        public Form2()
        {
            InitializeComponent();
        }

        private void  button1_Click(object sender, EventArgs e)
        {
            getPictureArray();
            getSoundArray();
            SQLiteDatabaseOperations sqliteDb = new SQLiteDatabaseOperations();
            sqliteDb.SaveImageRecord(img_arr1, key);
            sqliteDb.SaveSoundRecord(snd_arr1, key);
            
        }
        private void getPictureArray()
        {
            Image img = pictureBox1.Image ; // or use the PictureEdit.Image property
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                img_arr1 = ms.ToArray();
            }
         }
        private void getSoundArray()
        {
            using (MemoryStream ms = new MemoryStream())
            using (FileStream file = new FileStream(soundFileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
                snd_arr1 = ms.ToArray();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.key = this.key;
            Form2 f2 = new Form2();
            f2.Hide();
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
            Player.URL = soundFileName;
            Player.controls.play();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
