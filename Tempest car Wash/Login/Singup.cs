using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login
{
    public partial class Singup : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Singup()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string checkpass = textBox3.Text;
            string address = textBox5.Text;
            string phoneNumber = textBox4.Text;
            string gender = maleRadioButton.Checked ? "Male" : femaleRadioButton.Checked ? "Female" : string.Empty;
           
            string birth = dateTimePicker1.Value.ToString("dd/MM/yyyy");


            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your user name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(checkpass))
            {
                MessageBox.Show("Please enter rewrite password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password != checkpass)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
                textBox2.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Please select your gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please enter your phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ValidatePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Phone number must be 11 digits and contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Clear();
                return;
            }
            else if (string.IsNullOrWhiteSpace(birth))
            {
                MessageBox.Show("Please enter your birthday.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter your address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
           
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                
                SqlCommand sq2 = new SqlCommand("SELECT COUNT(*) FROM CUSTOMERS WHERE USERNAME = @username", con);
                sq2.Parameters.AddWithValue("@username", username);


                int userCount = (int)sq2.ExecuteScalar();

                if (userCount == 0)
                {

                    string query = @"INSERT INTO CUSTOMERS (USERNAME, PASSWORD, GENDER, PHONENO, ADDRESS, DATEOFBIRTH) " +
                        "VALUES (@username, @password, @gender, @phoneNo, @address, @dateOfBirth)";


                    SqlCommand insertCmd = new SqlCommand(query, con);

                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@password", password);
                    insertCmd.Parameters.AddWithValue("@gender", gender);
                    insertCmd.Parameters.AddWithValue("@phoneNo", phoneNumber);
                    insertCmd.Parameters.AddWithValue("@address", address);
                    insertCmd.Parameters.AddWithValue("@dateOfBirth",birth);

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Register successful! Welcome {username} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string usertype = "New";
                        HomePage homePage = new HomePage(username, usertype);
                        homePage.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Error {username} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                sq2.ExecuteNonQuery();

                con.Close();

                


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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }

        
}

