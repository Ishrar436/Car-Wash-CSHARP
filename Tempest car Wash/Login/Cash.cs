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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Login
{
    public partial class Cash : Form
    {

        public string username;
        public string vehicletype;
        public string vehiclemodel;
        public string washtype;
        public string price;
        public string usertype;
        public double  amount ;
        public Cash(string username, string vehicletype, string vehiclemodel, string washtype, string price, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.vehicletype = vehicletype;
            this.vehiclemodel = vehiclemodel;
            this.washtype = washtype;
            this.price = price;
            this.usertype = usertype;
            amount = 0;

            label1.Text = $"User : {username}!";
            label5.Text = $"Vehicle Type : {vehicletype}";
            label6.Text = $"Vehicle Model :{vehiclemodel}";
            label7.Text = $"Wash Type : {washtype}";
            label8.Text = $"Bill : {price}";
            label3.Text = $"Bill : {price}";

        }

    private void button1_Click(object sender, EventArgs e)
        {
            /*double pr = Convert.ToDouble(price); ;
            amount += Convert.ToDouble(textBox1.Text);
            
             if (!double.TryParse(price, out double pr))
            {
                MessageBox.Show("Invalid price format. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the textbox input is a valid number
            if (!int.TryParse(textBox1.Text, out int enteredAmount))
            {
                MessageBox.Show("Please enter a valid numeric amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }

            
            else if (amount >= pr)
            {
                double returnp = amount - pr;
                label12.Text = $"Paid : {amount}";
                label11.Text = $"Return : {returnp}";

                MessageBox.Show($"Thank You {username} For Visting", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (usertype.Equals("New"))
                {
                    usertype = "Regular";
                }
                HomePage homePage = new HomePage(username, usertype);
                homePage.Show();
                this.Close();


            }
            else if (pr == amount)
            {
                label11.Text = $"Return : {0}";
                label12.Text =$"Paid :{amount}";
                MessageBox.Show($"Thank You {username} For Visting", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (usertype.Equals("New"))
                {
                    usertype = "Regular";
                }
                HomePage homePage = new HomePage(username, usertype);
                homePage.Show();
                this.Close();
            }
            else
             {

                int returnp = amount - pr;
                 MessageBox.Show($"Not Enogh Amount enter {returnp} taka more.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  label12.Text = $"Paid : {amount}";
                    
            }*/

            if (!double.TryParse(price, out double pr))
            {
                MessageBox.Show("Invalid price format. Please enter a valid numeric price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            if (!double.TryParse(textBox1.Text, out double enteredAmount))
            {
                MessageBox.Show("Please enter a valid numeric amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }

           
            amount += enteredAmount;

            if (amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (amount >= pr)
            {
                double returnp = amount - pr;
                label12.Text = $"Paid : {amount:F2}";  
                label11.Text = $"Return : {returnp:F2}";

                MessageBox.Show($"Thank you, {username}, for visiting!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                if (usertype.Equals("New"))
                {
                    usertype = "Regular";
                }

                HomePage homePage = new HomePage(username, usertype);
                homePage.Show();
                this.Close();
            }
            else
            {
                
                double moreRequired = pr - amount;
                MessageBox.Show($"Not enough amount entered. Please enter {moreRequired:F2} more.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label12.Text = $"Paid : {amount:F2}";
                textBox1 .Clear();
                textBox1.Focus();
            }

        }
    }
}
