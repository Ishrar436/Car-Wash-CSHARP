using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login
{
    public partial class Register : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public string username;
        public Register( )
        {
            InitializeComponent();
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            string address = textBox2.Text;
            string username = textBox3.Text;
            string nationality = textBox4.Text;
            string phoneNumber = textBox1.Text;
            string gender = maleRadioButton.Checked ? "Male" : femaleRadioButton.Checked ? "Female" : string.Empty;
            string birth= dateTimePicker1.Value.ToString("dd/MM/yyyy");

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please Enter Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(nationality))
            {
                MessageBox.Show("Please Enter Nationality.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            else if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please select your gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please Enter your phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (!ValidatePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Phone number must be 11 digits and contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter your address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(birth))
            {
                MessageBox.Show("Please enter your birthday.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO ONE_TIME_CUSTOMERS (USERNAME, NATIONALITY, DATEOFBIRTH, GENDER,PHONENO, ADDRESS) " +
                        "VALUES (@username, @nationality, @dateofbirth,@gender, @phoneNo, @address)", con);

                insertCmd.Parameters.AddWithValue("@username", username);
                insertCmd.Parameters.AddWithValue("@nationality", nationality);
                insertCmd.Parameters.AddWithValue("@gender", gender);
                insertCmd.Parameters.AddWithValue("@phoneNo", phoneNumber);
                insertCmd.Parameters.AddWithValue("@address", address);
                insertCmd.Parameters.AddWithValue("@dateofbirth", birth);

                MessageBox.Show($"Welcome To Tempest {username}");

                insertCmd.ExecuteNonQuery();

                con.Close();

                string usertype = "One Time";
                HomePage homePage = new HomePage(username,usertype);
                homePage.Show();
                this.Close();

            }

        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{11}$");
        }

        private void phoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void maleRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
}
