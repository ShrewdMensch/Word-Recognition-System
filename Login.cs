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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "admin" && txtPassword.Text == "1234")
            {
                AdminArea adminForm = new AdminArea();
                adminForm.Show();
                this.Hide();
            }

            else
                MessageBox.Show("Invalid username/password", "Error...");
        }
    }
}
