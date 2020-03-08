using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Word_Recognition_System
{
    public partial class AddItems : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;

        public AddItems()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source=wordSystem.sqlite;Version= 3;");
        }

        private void btnSlectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog BrowseImageDlg = new OpenFileDialog();
            BrowseImageDlg.Title = "Word\'s Image";
            BrowseImageDlg.Filter = "Jpeg Files(*.jpg)|*.jpg|Png Files(*.png)|*.png|All Files(*.*)|*.*";
            try
            {
                if (BrowseImageDlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(BrowseImageDlg.FileName);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && txtWord.Text != null)
            {
                byte[] imageBytes;
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                MemoryStream myStream = new MemoryStream();
                bmp.Save(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = myStream.ToArray();

                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                try
                {
                    DBcommand = DBconnect.CreateCommand();
                    DBcommand.CommandText = "INSERT INTO ThreeLetterWords(Word,Image) VALUES('" + txtWord.Text.ToUpper() +
                            "',@IMG)";

                    SQLiteParameter imgPara = new SQLiteParameter("@IMG", DbType.Binary);
                    DBcommand.Parameters.Add("IMG", DbType.Binary, imageBytes.Length).Value = imageBytes;

                    DBcommand.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully", "Saving...");

                    txtWord.Text = null;
                    pictureBox1.Image = null;
              
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error...");

                }
            }

            else
                MessageBox.Show("Type in a word or choose a valid image");
        }
    }
}
