using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Recognition_System
{
    public partial class MainMenu : Form
    {

        Letters letterForm = new Letters();

        public MainMenu()
        {
            Thread t = new Thread(new ThreadStart(SplashStart));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();

            letterForm.FormClosed += LetterForm_FormClosed;
        }

        public MainMenu(String msg)
        {
            Thread t = new Thread(new ThreadStart(DoNothing));
            InitializeComponent();
        }

        private void SplashStart()
        {
            Application.Run(new SplashScreen());
        }

        //Function to use with Thread not to show wen u are not just starting
        private void DoNothing()
        {

        }

        private void LetterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MainMenu mm = new MainMenu();
            //mm.Show(this);
        }
        

        private void btnLetters_Click(object sender, EventArgs e)
        {
            letterForm.Show();
            //this.Hide();
        }

        private void addNewLettersNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItems addItems = new AddItems();
            addItems.Show(this);
            //this.Hide();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void lettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            letterForm = new Letters();
            letterForm.Show();
            this.Hide();
            Cursor.Current = Cursors.Default;

        }

        private void ManageWord_Click(object sender, EventArgs e)
        {
            WordMgt wordManager = new WordMgt();
            wordManager.Show();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void adminAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login logIn = new Login();
            logIn.Show(this);
        }

        private void numberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Numbers numb = new Numbers();
            numb.Show();
            this.Hide();
            Cursor.Current = Cursors.Default;
        }
    }
}
