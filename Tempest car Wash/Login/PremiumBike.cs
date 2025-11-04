using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class PremiumBike : Form
    {
        public string username;
        public string price;
        public string usertype;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public PremiumBike(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
            label7.Text = $"User : {username}   User Type : {usertype}";
            LoadVehicleModels();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;


        }
        private void LoadVehicleModels()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT vehicle_model FROM VEHICLES WHERE vehicle_type='Two Wheel' AND washtype='Premium'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["vehicle_model"].ToString());
            }
            
            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Twowheel twowheel = new Twowheel(username, usertype);
            twowheel.Show();
            this.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedModel = comboBox1.SelectedItem.ToString();
            ShowPrice(selectedModel);
        }
        private void ShowPrice(string selectedModel)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = @"SELECT price FROM VEHICLES WHERE vehicle_model = @selectedModel AND washtype = 'Premium'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@selectedModel", selectedModel);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                price = result.ToString();
                label4.Text = "Wash Cost : " + price;
            }
            else
            {
                label4.Text = "Price not found.";
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string vehicletype = "Two Wheel";
            string vehiclemodel = comboBox1.SelectedItem.ToString(); ;
            string washtype = "Premium";
            

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a kit type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                Info info = new Info(username, vehicletype, vehiclemodel, washtype, price,usertype);
                info.Show();
                this.Close();
            }

            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = 0;
            if (count < 8)
            {
                pictureBox1.Image = imageList1.Images[count];
                count++;
            }
            else
            {
                count = 0;
            }
            
        }
        int count = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
           
            if (count < 9)
            { 
                pictureBox1.Image = imageList1.Images[count];
                count++;
            }
            else
            {
                count = 0;
            }
        }
    }
}
