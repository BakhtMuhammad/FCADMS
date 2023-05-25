using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
    public partial class Expanditure : Form
    {
        public Expanditure()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;

        private void Expanditure_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Expanditure", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvExpanditure.DataSource = ds.Tables[0];

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Expanditure values(@CompanyID,@DriverCNIC,@FarmerCNIC,@Quantity,@TotaleSale, @TruckRent, @TotalCommission, @TotalExpanditure)", con);

            con.Open();
            cmd.Parameters.AddWithValue("@CompanyID", txtCompanyId.Text);
            cmd.Parameters.AddWithValue("@DriverCNIC", txtDriverCnic.Text);
            cmd.Parameters.AddWithValue("@FarmerCNIC", txtFarmerCnic.Text);
            cmd.Parameters.AddWithValue("@Quantity", txtQuant.Text);
            cmd.Parameters.AddWithValue("@TotaleSale", txtTotalSale.Text);
            cmd.Parameters.AddWithValue("@TruckRent", txtTruckRent.Text);
            cmd.Parameters.AddWithValue("@TotalCommission", txtTotalCommission.Text);
            cmd.Parameters.AddWithValue("@TotalExpanditure", txtTotalExpanditure.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Expanditure record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Expanditure_Load(null, null);
       

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Expanditure where Id='" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("Expanditure Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Expanditure_Load(this, null);
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
            txtCompanyId.Clear();
            txtDriverCnic.Clear();
            txtFCnic.Clear();
            txtQuant.Clear();
            txtTotSale.Clear();
            txtRent.Clear();
            txtTotalCommission.Clear();
            txtTotalExpanditure.Clear();

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Expanditure set CompanyID=@CompanyID,DriverCNIC=@DriverCNIC,FarmerCNIC=@FarmerCNIC,Quantity=@Quantity,TotaleSale=@TotaleSale, TruckRent=@TruckRent,TotalCommission=@TotalCommission,TotalExpanditure=@TotalExpanditure where Id= '" + ID + "'", con);

            con.Open();
            cmd.Parameters.AddWithValue("@CompanyID", txtCompanyId.Text);
            cmd.Parameters.AddWithValue("@DriverCNIC", txtDriverCnic.Text);
            cmd.Parameters.AddWithValue("@FarmerCNIC", txtFCnic.Text);
            cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@TotaleSale", txtTotSale.Text);
            cmd.Parameters.AddWithValue("@TruckRent", txtTruckRent.Text);
            cmd.Parameters.AddWithValue("@TotalCommission", txtTotalCommission.Text);
            cmd.Parameters.AddWithValue("@TotalExpanditure", txtTotalExpanditure.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Expanditure record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Expanditure_Load(null, null);
           

        }

        private void dgvExpanditure_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvExpanditure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvExpanditure.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvExpanditure.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                SqlCommand cmd = new SqlCommand("Select * from tb_Expanditure where Id= '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtCompanyId.Text = ds.Tables[0].Rows[0][1].ToString();
                txtDriverCnic.Text = ds.Tables[0].Rows[0][2].ToString();
                txtFCnic.Text = ds.Tables[0].Rows[0][3].ToString();
                txtQuant.Text = ds.Tables[0].Rows[0][4].ToString();
                txtTotSale.Text = ds.Tables[0].Rows[0][5].ToString();
                txtRent.Text = ds.Tables[0].Rows[0][6].ToString();
                txtTotalCommission.Text = ds.Tables[0].Rows[0][7].ToString();
                txtTotalExpanditure.Text = ds.Tables[0].Rows[0][8].ToString();
                
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
                    SqlCommand cmd = new SqlCommand("Select * from tb_Expanditure where CompanyID LIKE '" + txtSearch.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvExpanditure.DataSource = ds.Tables[0];
                    //Products_Load(this, null);

                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Expanditure", con);
                    cmd.Connection = con;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvExpanditure.DataSource = ds.Tables[0];
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
