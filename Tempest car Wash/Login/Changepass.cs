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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Login
{
    public partial class Changepass : Form
    {
        public string username;
        public string usertype;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abrar\OneDrive\Documents\Ishrar.mdf;Integrated Security=True;Connect Timeout=30";

        public Changepass(string username, string usertype)
        {
            InitializeComponent();
            this.username = username;
            this.usertype = usertype;
            label1.Text = $"User :{username}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomePage homepage = new HomePage(username, usertype);
            homepage.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newpass = textBox2.Text;
            string chkpass = textBox3.Text;
            string currentpass = textBox1.Text;

            if (string.IsNullOrWhiteSpace(newpass) || string.IsNullOrWhiteSpace(chkpass) || string.IsNullOrWhiteSpace(currentpass)) 
            {
                MessageBox.Show("Please enter  current password ,new password and rewite password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                string query = "SELECT COUNT(*) FROM CUSTOMERS WHERE username = @username AND password = @password";


                SqlCommand sq = new SqlCommand(query, con);
                sq.Parameters.AddWithValue("@username", username);
                sq.Parameters.AddWithValue("@password", currentpass);
                int rowsAffected = (int)sq.ExecuteScalar();
                con.Close();
                if (rowsAffected > 0)
                {
                    if (newpass.Equals(chkpass))
                    {
                        DialogResult result = MessageBox.Show("Update Your Password ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                        if (result == DialogResult.Yes)
                        {
                            con.Open();

                            string querry2 = "update customers  set password=@password where username=@username";


                            SqlCommand sq3 = new SqlCommand(querry2, con);
                            sq3.Parameters.AddWithValue("@username", username);
                            sq3.Parameters.AddWithValue("@password", newpass);
                            int rowsAffected2 = (int)sq3.ExecuteNonQuery();
                            con.Close ();

                            if (rowsAffected2 > 0)
                            {

                                MessageBox.Show("User Password UPDATED", "Succsess! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Setting setting = new Setting(username, usertype);
                                setting.Show();
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("Update failed. Please try again.","Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Rewrite your password correctly", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox2.Clear();
                        textBox2.Clear();
                        textBox2.Focus();
                    }

                }
                
                else
                {
                    MessageBox.Show("Please Enter Correct Current Password ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1 .Clear();
                    textBox1 .Focus();
                }
                


                
            }

            
        }
    }
}
