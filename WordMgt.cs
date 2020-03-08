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
    public partial class WordMgt : Form
    {
        SQLiteConnection DBconnect;
        SQLiteCommand DBcommand;
        SQLiteDataReader DBreader;

        public WordMgt()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source=wordSystem.sqlite;Version= 3;");
            loadRecordIntoListBox();
        }

        private void loadRecordIntoListBox()
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();
            try
            {
                DBcommand = new SQLiteCommand("SELECT * FROM ThreeLetterWords ORDER BY WORD ASC",DBconnect);
                DBreader = DBcommand.ExecuteReader();

                listBox1.Items.Clear();

                while (DBreader.Read())
                {
                    listBox1.Items.Add(DBreader["Word"].ToString());
                }

                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                    listBox1.Select();
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DBreader.Close();
            DBconnect.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
            //    listBox1.SelectedIndex = 0;
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();
            try
            {
                DBcommand = new SQLiteCommand("SELECT * FROM ThreeLetterWords WHERE Word ='"+
                    listBox1.Text+"' ORDER BY Word ASC", DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    groupBox1.Text = DBreader["Word"].ToString();
                    byte[] imageAsBytes = (byte[])DBreader["Image"];
                    MemoryStream imageStream = new MemoryStream(imageAsBytes);
                    pictureBox1.Image = Image.FromStream(imageStream);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DBreader.Close();
            DBconnect.Close();
        }

        private void WordMgt_Load(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                listBox1.Select();
            }
            chkEdit.Checked = false;
        }

        private void chkEdit_CheckedChanged(object sender, EventArgs e)
        {
            tlpEdit.Visible = chkEdit.Checked;
        }

        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog BrowseImageDlg = new OpenFileDialog();
            BrowseImageDlg.Title = "Choose Word\'s Image";
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

        private void btnUpdate_Click(object sender, EventArgs e)
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
                    DBcommand.CommandText = "UPDATE ThreeLetterWords SET Word ='" + txtWord.Text.ToUpper()+
                            "',Image =@IMG WHERE Word= '"+listBox1.Text+"'";

                    SQLiteParameter imgPara = new SQLiteParameter("@IMG", DbType.Binary);
                    DBcommand.Parameters.Add("IMG", DbType.Binary, imageBytes.Length).Value = imageBytes;

                    DBcommand.ExecuteNonQuery();
                    MessageBox.Show("Data Updated successfully", "Saving...");

                    vacuumDatabase();
                    loadRecordIntoListBox();

                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.SelectedIndex = 0;
                        listBox1.Select();
                    }

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();
            try
            {
                DBcommand = DBconnect.CreateCommand();
                DBcommand.CommandText = "DELETE FROM ThreeLetterwords WHERE Word ='" + listBox1.Text+"'";

                if (MessageBox.Show("Are you sure to delete this word?",
                    "Notification", MessageBoxButtons.YesNo )== DialogResult.Yes)
                {
                    DBcommand.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted successfully", "Deletion...");

                    txtWord.Text = null;
                    pictureBox1.Image = null;
                    vacuumDatabase();
                    loadRecordIntoListBox();

                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.SelectedIndex = 0;
                        listBox1.Select();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...");

            }
        }

        //Methods that Resize (Vacuum) Database i.e remove excessive allocated space
        private void vacuumDatabase()
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();

            DBcommand = new SQLiteCommand("VACUUM;", DBconnect);
            DBcommand.ExecuteNonQuery();

            DBconnect.Close();
        }
    }
}
