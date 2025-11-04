using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class Adminlogin : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Adminlogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TempestHome tempestHome = new TempestHome();
            tempestHome.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adminid =  textBox1.Text;
            string password = textBox2.Text;


            if (string.IsNullOrEmpty(adminid) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both admin Id  and password.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ValidateAdminId(adminid))
            {
                MessageBox.Show("Admin Id contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string querry = "SELECT COUNT(*) FROM ADMIN WHERE ADMINID = @adminid";

            SqlCommand checkUserCmd = new SqlCommand(querry, con);
            checkUserCmd.Parameters.AddWithValue("@adminid", Convert.ToInt32(adminid));
            int userCount = (int)checkUserCmd.ExecuteScalar();
            if (userCount > 0)
            {
                string querry2 = "SELECT ADMINPASSWORD FROM ADMIN WHERE ADMINID = @adminid";

                SqlCommand checkPasswordCmd = new SqlCommand(querry2, con);
                checkPasswordCmd.Parameters.AddWithValue("@adminid", Convert.ToInt32(adminid));

                string storedPassword = checkPasswordCmd.ExecuteScalar().ToString();

                if (storedPassword == password)
                {
                    MessageBox.Show($"Login successful! Welcome {adminid} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AdminHome adminHome = new AdminHome(adminid);
                    adminHome.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong password.","Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                    textBox2.Focus();
                    return;
                }

            }
            else
            {
                MessageBox.Show("Username not found.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox1.Clear();
                return;
            }
            con.Close();



        }
        private bool ValidateAdminId(string adminid)
        {
            return Regex.IsMatch(adminid, @"^\d+$");
        }
        private void txtAdminId_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
                MessageBox.Show("Admin ID can only contain numbers.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
