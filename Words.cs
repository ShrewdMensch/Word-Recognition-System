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
using System.Speech;
using System.Speech.Synthesis;

namespace Word_Recognition_System
{
    public partial class Words : Form
    {
        SQLiteConnection DBconnect;
        SQLiteCommand DBcommand;
        SQLiteDataReader DBreader;
        SQLiteDataAdapter da = new SQLiteDataAdapter();
        DataSet ds = new DataSet();
        SpeechSynthesizer textReader = new SpeechSynthesizer();
        static int currentRow = 0;

        public Words()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source=wordSystem.sqlite;Version= 3;");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Left))
            {
                button2.PerformClick();
            }


            if (keyData == (Keys.Right))
            {
                button3.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readNextWord();
            readWord();
        }

        private void loadWordCategoryFromDB()
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();
            String query = "SELECT * FROM ThreeLetterWords WHERE Word LIKE '"+
                Letters.letterSelected+"%' ORDER BY Word ASC";
            try
            {
                DBcommand = new SQLiteCommand(query, DBconnect);
                da = new SQLiteDataAdapter(DBcommand);
                da.Fill(ds);

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DBconnect.Close();
        }

        private void Words_Load(object sender, EventArgs e)
        {
            loadWordCategoryFromDB();
            //Load First element in category
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblWord.Text = ds.Tables[0].Rows[0]["Word"].ToString(); //Load word

                byte[] imgFromDB = (byte[])ds.Tables[0].Rows[0]["Image"];
                if (imgFromDB == null) pictureBox1.Image = null;
                else
                {
                    MemoryStream mystream = new MemoryStream(imgFromDB);
                    pictureBox1.Image = Image.FromStream(mystream);
                }
            }

            readWord();
        }

        private void readWord()
        {
            if (lblWord.Text != null && (lblWord.Text.ToUpper() != "WORD"))
            {
                if (textReader != null)
                    textReader.Dispose();

                textReader = new SpeechSynthesizer();
                textReader.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                textReader.Volume = 100;
                textReader.Rate = -6;
            
                StringBuilder word = new StringBuilder();

                char[] wordAsArray = lblWord.Text.ToCharArray();
                for (int i =0; i<wordAsArray.Length-1;i++)
                {
                    word.Append(wordAsArray[i]);
                    word.Append(" ");
                }
                word.Append(wordAsArray[wordAsArray.Length-1]);

                textReader.SpeakAsync(word.ToString()+"\t\t\t"+lblWord.Text);
                
            }
        }

       

        private void readNextWord()
        {
           
            try
            {
                if (currentRow < ds.Tables[0].Rows.Count)
                    currentRow++;

                if (currentRow >= ds.Tables[0].Rows.Count)
                    currentRow = 0;

                // Check if word category is not empty
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblWord.Text = ds.Tables[0].Rows[currentRow]["Word"].ToString(); //Load word

                    byte[] imgFromDB = (byte[])ds.Tables[0].Rows[currentRow]["Image"];
                    if (imgFromDB == null) pictureBox1.Image = null;
                    else
                    {
                        MemoryStream mystream = new MemoryStream(imgFromDB);
                        pictureBox1.Image = Image.FromStream(mystream);
                    }
                }
            }

            catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void readPreviousWord()
        {
            try
            {
                if (currentRow > 0)
                    currentRow--;
                else
                    currentRow = ds.Tables[0].Rows.Count - 1;

                //Check if word category is not empty
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblWord.Text = ds.Tables[0].Rows[currentRow]["Word"].ToString(); //Load word

                    byte[] imgFromDB = (byte[])ds.Tables[0].Rows[currentRow]["Image"];

                    if (imgFromDB == null) pictureBox1.Image = null;

                    else
                    {
                        MemoryStream mystream = new MemoryStream(imgFromDB);
                        pictureBox1.Image = Image.FromStream(mystream);
                    }
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readWord();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            readPreviousWord();
            readWord();
        }

        private void Words_FormClosed(object sender, FormClosedEventArgs e)
        {
            textReader.Dispose();
        }
    }
}
