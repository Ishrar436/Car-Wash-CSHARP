using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class TempestHome : Form
    {
        public TempestHome()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adminlogin adminlogin = new Adminlogin();
            adminlogin.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Thank You   For Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
        }

        private void TempestHome_Load(object sender, EventArgs e)
        {

        }
    }
}
