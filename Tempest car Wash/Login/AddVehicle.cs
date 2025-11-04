using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class AddVehicle : Form
    {
        public string adminid;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public AddVehicle(string adminid)
        {
            InitializeComponent();
            this.adminid = adminid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome(adminid);
            adminHome.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string vehicle_type = comboBox1.SelectedItem.ToString(); 
            string washtype = comboBox3.SelectedItem.ToString();
            string vehicle_model = textBox1.Text;
            string price = textBox2.Text;

            if (string.IsNullOrEmpty(vehicle_type) || string.IsNullOrEmpty(vehicle_model) || string.IsNullOrEmpty(washtype) || string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Please enter all Information.", "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else if (!Validateprice(price))
            {
                MessageBox.Show("Price contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string querry = "SELECT COUNT(*) FROM VEHICLES WHERE  washtype = @washtype and vehicle_model = @vehicle_model";

                SqlCommand checkUserCmd = new SqlCommand(querry, con);
                //checkUserCmd.Parameters.AddWithValue("@vehicle_type", vehicle_type);
                checkUserCmd.Parameters.AddWithValue("@vehicle_model", vehicle_model);
                checkUserCmd.Parameters.AddWithValue("@washtype", washtype);
                int userCount = (int)checkUserCmd.ExecuteScalar();
                if (userCount > 0)
                {
                    MessageBox.Show("Vehicle already exists. Please enter a different vehicle or washtype.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox1.Focus();
                   
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Add {vehicle_model},{washtype},ptice:{price}'?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                    if (result == DialogResult.Yes)
                    {
                        string querry2 = "INSERT INTO VEHICLES VALUES(@vehicle_type,@vehicle_model,@washtype,@price)";
                        SqlCommand checkUserCmd2 = new SqlCommand(querry2, con);

                        checkUserCmd2.Parameters.AddWithValue("@vehicle_type", vehicle_type);
                        checkUserCmd2.Parameters.AddWithValue("@vehicle_model", vehicle_model);
                        checkUserCmd2.Parameters.AddWithValue("@washtype", washtype);
                        checkUserCmd2.Parameters.AddWithValue("@price", price);


                        int rowsAffected = checkUserCmd2.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"New Vehicle Added successful! {vehicle_model} , price :{price} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        else
                        {
                            MessageBox.Show($"Error {adminid} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                     

                }
            }

                
        }
        private bool Validateprice(string price)
        {
            return Regex.IsMatch(price, @"^\d+$");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                MessageBox.Show("Please enter digits only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);


                e.Handled = true;
            }
        }
    }
}
