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
    public partial class HomePage : Form
    {
        public string username;
        public string usertype;
        public HomePage(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
            label1.Text = $"User : {username}";
            label2.Text = $"User Type : {usertype}";
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sixwheel sixwheel = new Sixwheel(username,usertype);
            sixwheel.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Twowheel twowheel = new Twowheel(username,usertype);
            twowheel.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Fourwheel fourwheel = new Fourwheel(username,usertype);
            fourwheel.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Log Out?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You {username}  For Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TempestHome tempestHome = new TempestHome();
                tempestHome.Show();
                this.Close();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            History history = new History(username, usertype);
            history.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(username, usertype);
            setting.Show();
            this.Close();
        }
    }
}
