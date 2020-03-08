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
    public partial class Numbers : Form
    {
        SQLiteConnection DBconnect;
        SQLiteCommand DBcommand;
        SQLiteDataReader DBreader;
        SQLiteDataAdapter da = new SQLiteDataAdapter();
        DataSet ds = new DataSet();
        SpeechSynthesizer textReader = new SpeechSynthesizer();
        static int currentRow = 0;

        public Numbers()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source=wordSystem.sqlite;Version= 3;");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Left))
            {
                btnPrevious.PerformClick();
            }


            if (keyData == (Keys.Right))
            {
                btnNext.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void loadNumbersFromDB()
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();
            String query = "SELECT * FROM Numbers";

            try
            {
                DBcommand = new SQLiteCommand(query, DBconnect);
                da = new SQLiteDataAdapter(DBcommand);
                da.Fill(ds);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DBconnect.Close();
        }




        private void btnPrevious_Click(object sender, EventArgs e)
        {
            readPreviousNumber();
            readWord();
        }

        private void Numbers_Load(object sender, EventArgs e)
        {
            loadNumbersFromDB();
            //Load First element in category
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblWord.Text = ds.Tables[0].Rows[0]["inWords"].ToString(); //Load word

                lblNumber.Text = ds.Tables[0].Rows[0]["Number"].ToString(); //Load Number
            }

            readWord();
        }

        private void readWord()
        {
            if (lblWord.Text != null)
            {
                if (textReader != null)
                    textReader.Dispose();

                textReader = new SpeechSynthesizer();
                textReader.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Senior);
                textReader.Volume = 100;
                textReader.Rate = -6;

                StringBuilder word = new StringBuilder();

                char[] wordAsArray = lblWord.Text.ToCharArray();
                for (int i = 0; i < wordAsArray.Length - 1; i++)
                {
                    word.Append(wordAsArray[i]);
                    word.Append(" ");
                }
                word.Append(wordAsArray[wordAsArray.Length - 1]);

                textReader.SpeakAsync(word.ToString() + "\t\t\t" + lblWord.Text);

            }
        }

        private void readNextNumber()
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
                    lblWord.Text = ds.Tables[0].Rows[currentRow]["inWords"].ToString(); //Load word

                    lblNumber.Text = ds.Tables[0].Rows[currentRow]["Number"].ToString(); //Load Number
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void readPreviousNumber()
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
                    lblWord.Text = ds.Tables[0].Rows[currentRow]["inWords"].ToString(); //Load word

                    lblNumber.Text = ds.Tables[0].Rows[currentRow]["Number"].ToString(); //Load Number
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            readNextNumber();
            readWord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readWord();
        }

        private void Numbers_FormClosed(object sender, FormClosedEventArgs e)
        {
            textReader.Dispose();
            MainMenu menu = new MainMenu("No Splash Screen");
            menu.Show();
        }
    }
}
