using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Login
{
    public partial class Login : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Login()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string querry = @"SELECT COUNT(*) FROM CUSTOMERS WHERE USERNAME = @username";
            SqlCommand checkUserCmd = new SqlCommand(querry, con);
            checkUserCmd.Parameters.AddWithValue("@username", username);
            int userCount = (int)checkUserCmd.ExecuteScalar();
            if (userCount > 0)
            {
                string querry2 = @"SELECT PASSWORD FROM CUSTOMERS WHERE USERNAME = @username";
                SqlCommand checkPasswordCmd = new SqlCommand(querry2, con);
                checkPasswordCmd.Parameters.AddWithValue("@username", username);

                string storedPassword = checkPasswordCmd.ExecuteScalar().ToString();

                if (storedPassword == password)
                {
                    MessageBox.Show($"Login successful! Welcome {username} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string usertype = "Regular";
                    HomePage homePage = new HomePage(username, usertype);
                    homePage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong password.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                    textBox2.Focus();
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("Username not found.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox1.Clear();
                return;
            }
           


            con.Close();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
}
