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
using System.Xml.Linq;

namespace fruitCommissionAgent
{
    public partial class Farmer : Form
    {
        public Farmer()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;
        private void Farmer_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Farmer", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvFarmer.DataSource = ds.Tables[0];
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Farmer values (@TraderName,@FCNIC,@FMobileNO,@FAddress,@FCity,@FProvince)", con);

            con.Open();
            cmd.Parameters.AddWithValue("@TraderName", txtTraderName.Text);
            cmd.Parameters.AddWithValue("@FCNIC", txtMobileNumber.Text);
            cmd.Parameters.AddWithValue("@FMobileNO", txtAddress.Text);
            cmd.Parameters.AddWithValue("@FAddress", txtCity.Text);
            cmd.Parameters.AddWithValue("@FCity", txtProvince.Text);
            cmd.Parameters.AddWithValue("@FProvince", txtProvince.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Farmer record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Farmer_Load(null, null);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Farmer where Farmer_Id='" + ID + "'");
                    cmd.Connection = con;

                    SqlDataAdapter sda= new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    MessageBox.Show("Farmer Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Farmer_Load(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Farmer set TraderName=@TraderName,FCNIC=@FCNIC,FMobileNO=@FMobileNO,FAddress=@FAddress,FCity=@FCity, FProvince=@FProvince where Farmer_Id= '" + ID + "'", con);
            con.Open();

            cmd.Parameters.AddWithValue("@TraderName", txtTraderName.Text);
            cmd.Parameters.AddWithValue("@FCNIC", txtFCNIC.Text);
            cmd.Parameters.AddWithValue("@FMobileNO", txtMobileNumber.Text);
            cmd.Parameters.AddWithValue("@FAddress", txtAddress.Text);
            cmd.Parameters.AddWithValue("@FCity", txtCity.Text);
            cmd.Parameters.AddWithValue("@FProvince", txtProvince.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Farmer record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Farmer_Load(this, null);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtTraderName.Clear();
            txtFCNIC.Clear();
            txtMobileNumber.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtProvince.Clear();
        }

        private void dgvFarmer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvFarmer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvFarmer.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                SqlCommand cmd = new SqlCommand("Select * from tb_Farmer where Farmer_Id= '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                //Filling the table data in the respective text boxes
                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtTraderName.Text = ds.Tables[0].Rows[0][1].ToString();

                txtFCNIC.Text = ds.Tables[0].Rows[0][2].ToString();
                txtMobileNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0][4].ToString();
                txtCity.Text = ds.Tables[0].Rows[0][5].ToString();
                txtProvince.Text = ds.Tables[0].Rows[0][6].ToString();


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
                    SqlCommand cmd = new SqlCommand("Select * from tb_Farmer where FCNIC LIKE '" + txtSearch.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvFarmer.DataSource = ds.Tables[0];
                    //Products_Load(this, null);

                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Farmer", con);
                    cmd.Connection = con;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvFarmer.DataSource = ds.Tables[0];
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
