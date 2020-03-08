using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Word_Recognition_System
{
    public partial class Letters : Form
    {
        public static String letterSelected = "A";

        Words wordForm = new Words();

        public Letters()
        {
            InitializeComponent();
            wordForm.FormClosed += WordForm_FormClosed;
        }

        private void WordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Letters letter = new Letters();
            letter.Show();
        }

        private void button_pressed(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            letterSelected = selectedButton.Text;
            Cursor.Current = Cursors.WaitCursor;
            wordForm.Show();
            Cursor.Current = Cursors.Default;
            this.Hide();

        }

        private void Letters_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainMenu mn = new MainMenu("No Splash Screen");
            mn.Show();
        }
    }
}
