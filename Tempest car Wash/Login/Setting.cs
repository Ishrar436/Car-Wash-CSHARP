using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Setting : Form
    {
        public string username;
        public string usertype;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Setting(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            label1.Text = $"User :{username}";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = @"
                SELECT gender, phoneno, address,dateofbirth
                FROM CUSTOMERS 
                WHERE username = @username 
                UNION 
                SELECT gender, phoneno, address ,dateofbirth
                FROM ONE_TIME_CUSTOMERS 
                WHERE username = @username";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                string gender = reader["gender"].ToString();
                string phoneNo = reader["phoneno"].ToString();
                string address = reader["address"].ToString();
                string dob = reader["dateofbirth"].ToString();
                label3.Text = "Gender: " + gender;
                label2.Text = "Phone No: " + phoneNo;
                label4.Text = "Address: " + address;
                label5.Text = "Date Of Birth : " + dob;
            }
            else
            {
                MessageBox.Show("User not found");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Changepass changepass = new Changepass(username,usertype);
            changepass.Show();
            this.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Changeinfo changeinfo = new Changeinfo(username,usertype);
            changeinfo.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(username,usertype);
            homePage.Show();
            this.Close();
        }
    }
}
