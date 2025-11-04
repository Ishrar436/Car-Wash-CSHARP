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
    public partial class Sixwheel : Form
    {
        public string username;
        public string usertype;
        public Sixwheel (string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bus bus = new Bus(username,usertype);
            bus.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(username, usertype);
            homePage.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You {username}  For Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Truck truck = new Truck(username,usertype);  
            truck.Show();
            this.Close();
        }
    }
}
