using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BitmapToSqlite
{
    public partial class Form1 : Form
    {
        char mode;
        OpenFileDialog fd2 = new OpenFileDialog();
        OpenFileDialog fd1 = new OpenFileDialog();
        private byte[] img_arr1;
        private byte[] snd_arr1;
        private byte[] gif_arr1;
        public Form1()
        {
            InitializeComponent();
            listBox1.DataSource = getKeys();
            listBox1.ClearSelected();
            if (listBox1.Items.Count < 1)  // No records in the Database
            {
                //disable the edit and delete buttons. Nothing to Edit/Delete
                button5.Enabled = false;
                button6.Enabled = false;
            }
            resetScreen();
        }

        #region  Click and selection changed events
        private void button1_Click(object sender, EventArgs e)
        {

            fd1.Filter = "image files|*.jpg;*.png;.*gif;*.icon;.*;";
            DialogResult dres2 = fd1.ShowDialog();
            if (dres2 == DialogResult.Abort)
                return;
            if (dres2 == DialogResult.Cancel)
                return;
            try {
            textBox4.Text = fd1.FileName;
            pictureBox1.Image = Image.FromFile(textBox4.Text);
            ImageToArray();
            if (img_arr1.Length > 35000)
            {
                MessageBox.Show("Please upload images less than 25 kb.");
                return;
            }
            textBox4.Text = fd1.FileName;
            ImageToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try 
            { 
            SQLiteDatabaseOperations sqliteDb = new SQLiteDatabaseOperations();
                if (mode == 'a')
                { 
                    if (textBox1.Text.ToString().Length < 1)
                    {
                        MessageBox.Show("Name can not be blank");
                        return;
                    }
                    sqliteDb.SaveImgSndRecord(img_arr1, snd_arr1, gif_arr1, textBox1.Text); 
                }
            if (mode == 'e') sqliteDb.UpdateImgSndRecord(img_arr1, snd_arr1,gif_arr1, listBox1.SelectedItem.ToString());
            if (mode == 'd') sqliteDb.DeleteImgSndRecord(listBox1.SelectedItem.ToString());
                listBox1.DataSource = getKeys();
                resetScreen();
                resetVariables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resetScreen();
                resetVariables();
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            
            fd2.Filter = "sound files|*.mp3;";
            DialogResult dres2 = fd2.ShowDialog();
            if (dres2 == DialogResult.Abort)
                return;
            if (dres2 == DialogResult.Cancel)
                return;
            textBox5.Text = fd2.FileName;
            SoundToArray();
            if (snd_arr1.Length > 25000)
            {
                MessageBox.Show("Please upload sound files less than 25 kb.");
                return;
            }

            button7.Enabled = true;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            mode = 'a';
            textBox1.ReadOnly = false;
            textBox1.Enabled = true;
            textBox1.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox5.Enabled = true;
            textBoxgif.Enabled = true;
            listBox1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button8.Enabled = true;
            buttongif.Enabled = true;
            textBox4.Text = "Select a file.";
            textBox5.Text = "Select a file.";
            textBoxgif.Text = "Select a file";

            //SoundToArray();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            mode = 'e';
            textBox1.ReadOnly = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox5.Enabled = true;
            textBoxgif.Enabled= true;
            listBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = true;
            button8.Enabled = true;
            buttongif.Enabled= true;
            textBox4.Text = "Select a file.";
            textBox5.Text = "Select a file.";
            textBoxgif.Text = "Select a file";
            ArrayToImage();
            ArrayToSound();
            ArrayToGIF();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            mode = 'd';
            textBox1.ReadOnly = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox5.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = false;
            button8.Enabled = true;
            listBox1.Enabled = true;
        }
        private void buttonGif_Click (object sender, EventArgs e)
        {
            fd1.Filter = "image files|*.gif";
            DialogResult dres2 = fd1.ShowDialog();
            if (dres2 == DialogResult.Abort)
                return;
            if (dres2 == DialogResult.Cancel)
                return;
            try
            {
                textBox5.Text = fd1.FileName;
                pictureBox2.Image = Image.FromFile(textBox5.Text);
                ImageToArray();
                if (img_arr1.Length > 35000)
                {
                    MessageBox.Show("Please upload gifs less than 25 kb.");
                    return;
                }
                textBox5.Text = fd1.FileName;
                GifToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;

        }
        private void button7_Click(object sender, EventArgs e)
        {
            playSoundFile();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            resetScreen();
            resetVariables();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString().Length > 0)
            {
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                ArrayToImage();
                ArrayToSound();
                ArrayToGIF();
                SoundArrayToFile();

            }

        }
        #endregion
        #region Conversion from Images and Sounds to Byte Array
        private void ImageToArray()
        {
                Image img = pictureBox1.Image;
                Bitmap bitmap = (Bitmap)img;
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    img_arr1 = ms.ToArray();
                }

        }
        private void GifToArray()
        {
            Image img = pictureBox2.Image;
            Bitmap bitmap = (Bitmap)img;
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                gif_arr1 = ms.ToArray();
            }
        }
        private void SoundToArray()
        {
                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(textBox5.Text, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                    snd_arr1 = ms.ToArray();
                }

        }
        #endregion
        #region Conversion from Array to Images and Sound
        private void ArrayToImage()
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString().Length > 0)
            {
                SQLiteDatabaseOperations sdb = new SQLiteDatabaseOperations();
                byte[] img_arr1 = sdb.getImage(listBox1.SelectedItem.ToString());
                if (img_arr1 != null)
                {

                MemoryStream ms1 = new MemoryStream(img_arr1);
                ms1.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms1);
                pictureBox1.Image = image;
                textBox4.Text = "Image file found. Browse to change.";
                }
                else
                {
                    textBox4.Text = "No image file stored.";
                }
            }
        }
        private void ArrayToSound()
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString().Length > 0 )
            {
                SQLiteDatabaseOperations sdb = new SQLiteDatabaseOperations();
                snd_arr1 = sdb.getSound(listBox1.SelectedItem.ToString());
                textBox5.Text = "Sound File Found. Browse to change.";
            }
        }
        private void ArrayToGIF()
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem.ToString().Length > 0)
            {
                SQLiteDatabaseOperations sdb = new SQLiteDatabaseOperations();
                gif_arr1 = sdb.getGif(listBox1.SelectedItem.ToString());
                if (gif_arr1 != null)
                {

                    MemoryStream ms1 = new MemoryStream(gif_arr1);
                    ms1.Seek(0, SeekOrigin.Begin);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms1);
                    pictureBox2.Image = image;
                    textBoxgif.Text = "Gif file found. Browse to change.";
                }
                else
                {
                    textBoxgif.Text = "No Gif file stored.";
                }
            }
        }
        #endregion
        #region Miscellaneous functions
        private void SoundArrayToFile()
        {
            if (snd_arr1 != null)
            {
                using (FileStream file = new FileStream("d:\\temp.mp3", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    file.Write(snd_arr1, 0, snd_arr1.Length);
                    file.Close();
                    file.Dispose();
                }
                return;
            }
        }
        private List<string> getKeys()
        {
            SQLiteDatabaseOperations sqlop = new SQLiteDatabaseOperations();
            return sqlop.getKeys();

        }       
        private void resetScreen()
        {
            listBox1.Enabled = false;
            textBox1.Enabled = false; textBox1.Text = "";
            textBox4.Enabled = false; textBox4.Text = "";
            textBox5.Enabled = false; textBox5.Text = "";
            textBox5.Text = "";
            textBoxgif.Enabled = false; textBoxgif.Text = "";
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            buttongif.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = false;

        }
        private void resetVariables ()
        {
            snd_arr1 = null;
            img_arr1 = null;
            gif_arr1 = null;
            mode = ' ';
        }
        #endregion
        private void playSoundFile()
        {
            WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();

            if ( File.Exists(textBox5.Text.ToString()))
            {
                Player.URL = textBox5.Text.ToString();
                Player.controls.play();
            }
            else if (snd_arr1 == null)
            {
                ArrayToSound();
                if (snd_arr1 == null)
                {
                    MessageBox.Show("No sound associated with this name.");
                    return;
                }
                SoundArrayToFile();
                Player.URL = "d:\\temp.mp3";
                Player.controls.play();
                File.Delete("d:\\temp.mp3");
            }
            else
            {
                SoundArrayToFile();
                Player.URL = "d:\\temp.mp3";
                Player.controls.play();
                File.Delete("d:\\temp.mp3");
            }
            GC.Collect();
        }
    }
}
