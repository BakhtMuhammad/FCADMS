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
    public partial class NetAmount : Form
    {
        public NetAmount()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;
        private void NetAmount_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_NetAmount", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvNetAmount.DataSource = ds.Tables[0];
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_NetAmount values(@FarmerCNIC,@TotalExpanditures,@NetAmount)", con);

            //SQLParameter collection
            con.Open();
            cmd.Parameters.AddWithValue("@FarmerCNIC", txtFarmerCNIC.Text);
            cmd.Parameters.AddWithValue("@TotalExpanditures", txtTotalExpanditures.Text);
            cmd.Parameters.AddWithValue("@NetAmount", txtNetAmount.Text);
          

            cmd.ExecuteNonQuery();
            con.Close();
            //MessageBox.Show("You have successfully added data.", "Heading", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            MessageBox.Show("Net Amount recorded successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NetAmount_Load(null, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_NetAmount set FarmerCNIC=@FarmerCNIC, TotalExpanditure=@TotalExpanditure,NetAmount=@NetAmount where Id= '" + ID + "'", con);
            con.Open();

            cmd.Parameters.AddWithValue("@FarmerCNIC", txtFarmerCNIC.Text);
            cmd.Parameters.AddWithValue("@TotalExpanditure", txtTotalExpanditures.Text);
            cmd.Parameters.AddWithValue("@NetAmount", txtNetAmount.Text);

            //cmd.Parameters.AddWithValue("@stulivingstatus", cbLivingStatus.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Net Amount record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NetAmount_Load(this, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_NetAmount where Id='" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("NetAmount Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    NetAmount_Load(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtFarmerCNIC.Clear();
            txtTotalExpanditures.Clear();
            txtNetAmount.Clear();
        }

        private void dgvNetAmount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvNetAmount.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvNetAmount.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                SqlCommand cmd = new SqlCommand("Select * from tb_NetAmount where Id= '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                //Filling the table data in the respective text boxes
                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtFarmerCNIC.Text = ds.Tables[0].Rows[0][1].ToString();
                txtTotalExpanditures.Text = ds.Tables[0].Rows[0][2].ToString();
                txtNetAmount.Text = ds.Tables[0].Rows[0][3].ToString();
                //txtMobileNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                //txtAddress.Text = ds.Tables[0].Rows[0][4].ToString();
                //txtCity.Text = ds.Tables[0].Rows[0][5].ToString();
                //txtProvince.Text = ds.Tables[0].Rows[0][6].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_NetAmount where FarmerCNIC LIKE '" + txtSearch.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvNetAmount.DataSource = ds.Tables[0];
                    //Products_Load(this, null);

                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_NetAmount", con);
                    cmd.Connection = con;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvNetAmount.DataSource = ds.Tables[0];
                    //Products_Load(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
