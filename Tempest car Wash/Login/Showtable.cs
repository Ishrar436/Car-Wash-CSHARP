using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class Showtable : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";
        public string adminid;
        public Showtable(string adminid)
        {
            InitializeComponent();
            this.adminid = adminid;
        }

        private void Showtable_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string querry = "select * from CUSTOMERS";
            SqlDataAdapter sq1 = new SqlDataAdapter(querry, con);
            DataTable dt = new DataTable();
            sq1.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome(adminid);
            adminHome.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {

                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];


                if (dataGridView1.Columns[selectedCell.ColumnIndex].Name == "username")
                {

                    string username = selectedCell.Value.ToString();


                    DialogResult result = MessageBox.Show($"Are you sure you want to delete the user '{username}'?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        SqlConnection con = new SqlConnection(connectionString);
                        con.Open();

                        string query = @"DELETE FROM CUSTOMERS WHERE username = @username";

                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@username", username);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{username} deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the user.");
                        }

                        con.Close();
                    }
                }
                else
                {

                    MessageBox.Show("Please select a username to delete the record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No cell selected. Please select a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string querry = "select * from CUSTOMERS";

            SqlDataAdapter sq1 = new SqlDataAdapter(querry, con);
            DataTable dt = new DataTable();
            sq1.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
