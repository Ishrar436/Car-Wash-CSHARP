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
    public partial class Twowheel : Form
    {
        public string username;
        public string usertype;
        public Twowheel(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NormalBike normalBike = new NormalBike(username,usertype);
            normalBike.Show();
            this.Close();
        }

        private void Twowheel_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(username, usertype);
            homePage.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PremiumBike premiumBike = new PremiumBike(username,usertype);
            premiumBike.Show();
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
    }
}
