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


namespace Login
{
    public partial class Changeinfo : Form
    {
        public string username;
        public string usertype;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Changeinfo(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
            label2.Text = $"User : {username}";
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string phoneNumber = textBox1.Text;
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please enter your new phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            else if (!ValidatePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Phone number must be 11 digits and contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }

            else
            {
                DialogResult result = MessageBox.Show("Update Your Phone Number ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string querry = "update customers  set phoneno=@phoneno where username=@username";


                    SqlCommand sq3 = new SqlCommand(querry, con);
                    sq3.Parameters.AddWithValue("@username", username);
                    sq3.Parameters.AddWithValue("@phoneno", phoneNumber);
                    int rowsAffected = (int)sq3.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User Phone number UPDATED", "Succsess! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear() ;
                        
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Please try again.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                }
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

        private void button6_Click(object sender, EventArgs e)
        {
            string gender = radioButton1.Checked ? "Male" : radioButton2.Checked ? "Female" : string.Empty;
            if (string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("Please select your gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Update Your Gender ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string querry = "update customers  set gender=@gender where username=@username";


                    SqlCommand sq3 = new SqlCommand(querry, con);
                    sq3.Parameters.AddWithValue("@username", username);
                    sq3.Parameters.AddWithValue("@gender", gender);
                    int rowsAffected = (int)sq3.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User Gender UPDATED","Success ! ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Update failed. Please try again.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string birth = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            if (string.IsNullOrWhiteSpace(birth))
            {
                MessageBox.Show("Please select your new date of birth.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Update Your Date of Birth", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string querry = "update customers  set dateofbirth=@dob where username=@username";

                    SqlCommand sq3 = new SqlCommand(querry, con);
                    sq3.Parameters.AddWithValue("@username", username);
                    sq3.Parameters.AddWithValue("@dob", birth);
                    int rowsAffected = (int)sq3.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User Date of Birth UPDATED","Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Update failed. Please try again.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string address = textBox2.Text;
            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter new address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Update Your Address", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string querry = "update customers  set address=@address where username=@username";


                    SqlCommand sq3 = new SqlCommand(querry, con);
                    sq3.Parameters.AddWithValue("@username", username);
                    sq3.Parameters.AddWithValue("@address", address);
                    int rowsAffected =(int) sq3.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User Address UPDATED","Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox2.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Update failed. Please try again.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(username, usertype);
            setting.Show();
            this.Close();
        }
        
    }
}
