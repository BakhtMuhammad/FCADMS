using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fruitCommissionAgent
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFullName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "" && txtRetypePass.Text != "")
                {
                    if (txtPassword.Text != txtRetypePass.Text)
                    {
                        MessageBox.Show("Password doesn't match. Please type again.");
                    }
                    else
                    {
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                        SqlCommand cmd = new SqlCommand("Insert into tb_User (fullName, userName, userPassword) Values ('" + txtFullName.Text + "', '" + txtUserName.Text + "','" + txtPassword.Text + "')", con);
                        cmd.Connection = con;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("User Registered Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFullName.Clear();
                        txtUserName.Clear();
                        txtPassword.Clear();
                        txtRetypePass.Clear();
                    }
                }

                else
                {
                    MessageBox.Show("Please make sure all fields are completed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
