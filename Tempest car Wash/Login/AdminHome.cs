using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class AdminHome : Form
    {
        public string adminid;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";
        public string adminame;

        public AdminHome(string adminid)
        {
             
            InitializeComponent();
            this.adminid = adminid;
            load();
        }
        private void load()
        {
            label2.Text = $"Admin ID : {adminid}";
            

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string querry = "SELECT COUNT(*) FROM ADMIN WHERE ADMINID = @adminid";

            SqlCommand checkUserCmd = new SqlCommand(querry, con);
            checkUserCmd.Parameters.AddWithValue("@adminid", Convert.ToInt32(adminid));
            int userCount = (int)checkUserCmd.ExecuteScalar();
            if (userCount > 0)
            {
                string querry2 = "SELECT ADMINNAME FROM ADMIN WHERE ADMINID = @adminid";

                SqlCommand checkPasswordCmd = new SqlCommand(querry2, con);
                checkPasswordCmd.Parameters.AddWithValue("@adminid", Convert.ToInt32(adminid));

                adminame = checkPasswordCmd.ExecuteScalar().ToString();

                label3.Text = $"Admin Name : {adminame}";



            }
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Log Out?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You {adminame}", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TempestHome tempestHome = new TempestHome();
                tempestHome.Show();
               
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You {adminame}  For Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Showtable showtable = new Showtable (adminid);
            showtable.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateVehicles updateVehicles = new UpdateVehicles (adminid);
            updateVehicles.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddVehicle addVehicle = new AddVehicle (adminid);
            addVehicle.Show();
            this.Close();
        }
    }
}
