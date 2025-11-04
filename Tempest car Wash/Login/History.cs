using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Login
{
    public partial class History : Form
    {
        public string username;
        public string usertype;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public History(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
            LoadUserInfo();
            LoadHistotyData();

        }
        private void LoadUserInfo()
        {
            label1.Text = $"User : {username}";
            label2.Text = $"User Type : {usertype}";

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = @"
                SELECT gender, phoneno, address
                FROM CUSTOMERS 
                WHERE username = @username 
                UNION 
                SELECT gender, phoneno, address 
                FROM ONE_TIME_CUSTOMERS 
                WHERE username = @username";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                string gender = reader["gender"].ToString();
                string phoneNo = reader["phoneno"].ToString();
                

                label3.Text = "Gender: " + gender;
                label4.Text = "Phone No: " + phoneNo;
                
            }

            reader.Close();

        }
        private void LoadHistotyData()
        {
            if (usertype.Equals("One Time"))
            {
                richTextBox1.Text = "No Record For One Time User";
            }
            
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();


                string query = @"
                SELECT vehicle_type, vehicle_model, washtype, price 
                FROM CAR_WASHED 
                WHERE username = @username AND customer_type in ('Regular' ,'New')";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    richTextBox1.Clear(); 

                    while (reader.Read())
                    {
                        string vehicleType = reader["vehicle_type"].ToString();
                        string vehicleModel = reader["vehicle_model"].ToString();
                        string washType = reader["washtype"].ToString();
                        string price = reader["price"].ToString();

                        richTextBox1.AppendText($"Vehicle Type: {vehicleType}\n");
                        richTextBox1.AppendText($"Vehicle Model: {vehicleModel}\n");
                        richTextBox1.AppendText($"Wash Type: {washType}\n");
                        richTextBox1.AppendText($"Price: ${price}\n");
                        richTextBox1.AppendText("-------------------------\n");
                    }
                }
                else
                {
                    richTextBox1.Text = "No history found for this user.";
                }

                reader.Close();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(username, usertype);
            homePage.Show();
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
