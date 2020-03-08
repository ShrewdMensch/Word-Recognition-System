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
    public partial class AdminArea : Form
    {
        public AdminArea()
        {
            InitializeComponent();
        }

        private void AdminArea_Load(object sender, EventArgs e)
        {
            AddItems addItemsForm = new AddItems();
            addItemsForm.TopLevel = false;
            tabPage1.Controls.Add(addItemsForm);
            addItemsForm.FormBorderStyle = FormBorderStyle.None;
            addItemsForm.Dock = DockStyle.Fill;
            addItemsForm.Show();

            WordMgt wMgt = new WordMgt();
            wMgt.TopLevel = false;
            tabPage2.Controls.Add(wMgt);
            wMgt.FormBorderStyle = FormBorderStyle.None;
            wMgt.Dock = DockStyle.Fill;
            wMgt.Show();
        }
    }
}
