using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Login
{
    public partial class Info : Form
    {
        public string username;
        public string vehicletype;
        public string vehiclemodel;
        public string washtype;
        public string price;
        public string usertype;
        public string phoneNo;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Info(string username, string vehicletype, string vehiclemodel, string washtype ,string price,string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.vehicletype = vehicletype;
            this.vehiclemodel= vehiclemodel;
            this.price = price;
            this.washtype = washtype;
            this.usertype = usertype;
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            label1.Text = $"User : {username}";
            label5.Text = $"Vehicle Type : {vehicletype}";
            label6.Text = $"Vehicle Model : {vehiclemodel}";
            label8.Text = $"Price : {price}";
            label10.Text = $"User Type : {usertype}";
            label7.Text = $"Wash Type : {washtype}";

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
                phoneNo = reader["phoneno"].ToString();
                string address = reader["address"].ToString();
                
                label2.Text = "Gender: " + gender;
                label4.Text = "Phone No: " + phoneNo;
                label3.Text = "Address: " + address;

                
                decimal discount = 0m;
                decimal Price = Convert.ToDecimal(price);

                if (usertype == "Regular")
                {
                    discount = Price * 0.05m;
                }
                else if (usertype == "New")
                {
                    discount = Price * 0.10m; 
                }

                decimal totalPrice = Price - discount;
                price = Convert.ToString(totalPrice);
                
                
                label11.Text = "Discount : " + discount.ToString();
                label12.Text = "Total : " + price ;
            }
            else
            {
                MessageBox.Show("User not found");
            }

            
            
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
            if (vehicletype.Equals("Two Wheel"))
            {
                if(washtype.Equals("Premium"))
                {
                    PremiumBike premiumBike = new PremiumBike(username, usertype);
                    premiumBike.Show();
                    this.Close();
                }
                else
                {
                    NormalBike normalBike = new NormalBike(username, usertype);
                    normalBike.Show();
                    this.Close();
                }
            }
            else if (vehicletype.Equals("Four Wheel"))
            {
                if(washtype.Equals("Premium"))
                {
                    PremiumCar premiumCar = new PremiumCar(username, usertype);
                    premiumCar.Show();
                    this.Close();
                }
                else
                {
                    NormalCar normalCar = new NormalCar(username, usertype);
                    normalCar.Show();
                    this.Close();
                }
            }
            else if (vehicletype.Equals("Six Wheel"))
            {
                if(washtype.Equals("Premium"))
                {
                    Bus bus = new Bus(username, usertype);    
                    bus.Show();
                    this.Close();
                }
                else
                {
                    Truck truck = new Truck(username, usertype );
                    truck.Show();
                    this.Close();
                }
            }
            

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Please select an option before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show($"Confirm your {vehicletype} , {vehiclemodel} , {washtype} wash?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();


                    string query = @"
                INSERT INTO CAR_WASHED (username, customer_type, phoneno, vehicle_type, vehicle_model, washtype, price) 
                VALUES (@username, @customer_type, @phoneno, @vehicle_type, @vehicle_model, @washtype, @price)";


                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@customer_type", usertype);
                    cmd.Parameters.AddWithValue("@phoneno", phoneNo);
                    cmd.Parameters.AddWithValue("@vehicle_type", vehicletype);
                    cmd.Parameters.AddWithValue("@vehicle_model", vehiclemodel);
                    cmd.Parameters.AddWithValue("@washtype", washtype);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"{vehiclemodel} | {washtype}  Confirmed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    

                    if (radioButton1.Checked)
                    {
                        Cash cash = new Cash(username, vehicletype, vehiclemodel, washtype, price, usertype);
                        cash.Show();
                        this.Close();
                    }
                    
                    else if (radioButton2.Checked)
                    {
                        Bkash bkash = new Bkash(username, vehicletype, vehiclemodel, washtype, price, usertype);
                        bkash.Show();
                        this.Close();
                    }
                    else
                    {
                        CarPayment carPayment = new CarPayment(username, vehicletype, vehiclemodel, washtype, price, usertype);
                        carPayment.Show();
                        this.Close();
                        con.Close();
                    }
                }

            }

            

        }
    }
}
