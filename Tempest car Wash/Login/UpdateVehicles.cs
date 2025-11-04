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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class UpdateVehicles : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";
        public string adminid;
        public string vehicle_model;
        public string vehicle_type;
        public string washtype;
        public string price;
        public UpdateVehicles(string adminid)
        {
            InitializeComponent();
            this.adminid = adminid;
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
        }

        private void UpdateVehicles_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string querry = "select * from VEHICLES";
            SqlDataAdapter sq1 = new SqlDataAdapter(querry, con);
            DataTable dt = new DataTable();
            sq1.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newprice = textBox1.Text;
            if (!ValidatePrice(newprice))
            {
                MessageBox.Show("Price Contain Only Numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }
            if(string.IsNullOrEmpty(newprice))
            {
                MessageBox.Show("Pleace Enter New Price .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show($"Do you want update price for {vehicle_model} , {washtype}?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    

                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string query = "UPDATE VEHICLES SET price = @newPrice WHERE vehicle_model = @vehicleModel AND vehicle_type = @vehicleType AND washtype = @washType;";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@newPrice", newprice);
                    cmd.Parameters.AddWithValue("@vehicleModel", vehicle_model);
                    cmd.Parameters.AddWithValue("@vehicleType", vehicle_type);
                    cmd.Parameters.AddWithValue("@washType", washtype);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show($"{vehicle_model},{washtype} price updated to {newprice}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear() ;
                    textBox1.Focus();


                }
            }
        }
        private bool ValidatePrice(string newprice)
        {
            return Regex.IsMatch(newprice, @"^\d+$");
        }
        private void txtBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                
                MessageBox.Show("Please enter digits only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);

                
                e.Handled = true;
            }
        }

        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                vehicle_type= selectedRow.Cells["vehicle_type"].Value.ToString();
                vehicle_model = selectedRow.Cells["vehicle_model"].Value.ToString();
                washtype = selectedRow.Cells["washtype"].Value.ToString();
                price = selectedRow.Cells["price"].Value.ToString();
                label1.Text = $"Price : {price}";
                label2.Text = $"Vehicle Type : {vehicle_type}";
                label3.Text =$"Vehicle Model : {vehicle_model}";
                label4.Text = $"Wash Type : {washtype}";
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                vehicle_type = selectedRow.Cells["vehicle_type"].Value.ToString();
                vehicle_model = selectedRow.Cells["vehicle_model"].Value.ToString();
                washtype = selectedRow.Cells["washtype"].Value.ToString();
                price = selectedRow.Cells["price"].Value.ToString();
                label1.Text = $"Price : {price}";
                label2.Text = $"Vehicle Type : {vehicle_type}";
                label3.Text = $"Vehicle Model : {vehicle_model}";
                label4.Text = $"Wash Type : {washtype}";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt != null)
            {
                dt.Clear();
            }
            AdminHome adminHome = new AdminHome (adminid);
            adminHome.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

          
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt != null)
            {
                dt.Clear();
            }

            string querry = "select * from VEHICLES";

            SqlDataAdapter sq1 = new SqlDataAdapter(querry, con);
            dt = new DataTable();
            sq1.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
