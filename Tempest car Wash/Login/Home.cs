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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Singup singup = new Singup();
            singup.Show();
            this.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TempestHome home = new TempestHome();
            home.Show();
            this.Close();
        }
    }
}
