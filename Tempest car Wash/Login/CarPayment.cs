using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class CarPayment : Form
    {
        public string username;
        public string vehicletype;
        public string vehiclemodel;
        public string washtype;
        public string price;
        public string usertype;
        public CarPayment(string username, string vehicletype, string vehiclemodel, string washtype , string price,string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.vehicletype = vehicletype;
            this.vehiclemodel = vehiclemodel;
            this.washtype = washtype;
            this.price = price;
            this.usertype = usertype;

            label1.Text = $"User : {username}!";
            label5.Text = $"Vehicle Type : {vehicletype}";
            label6.Text = $"Vehicle Model :{vehiclemodel}";
            label7.Text = $"Wash Type : {washtype}";
            label8.Text = $"Bill : {price}";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cardNumber = textBox1.Text;
            string pin = textBox2.Text;
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                MessageBox.Show("Please enter your card number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Regex.IsMatch(cardNumber, @"^\d+$"))
            {
                MessageBox.Show("Card Number must contain only numbers.", "Invalid Card Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }
            else if (string.IsNullOrWhiteSpace(pin))
            {
                MessageBox.Show("Please enter your pin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Regex.IsMatch(pin, @"^\d{1,5}$"))
            {
                MessageBox.Show("PIN must be a number and can only have up to 5 digits.", "Invalid PIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                return;
            }


            
            else
            {
                MessageBox.Show($"Thank You {username} For Visting", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(usertype.Equals("New"))
                {
                    usertype = "Regular";
                }
                HomePage homePage = new HomePage(username, usertype);
                homePage.Show();
                this.Close();
            }
        }

        private void CarPayment_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

        
}
