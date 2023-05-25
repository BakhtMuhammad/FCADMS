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
    public partial class Truck : Form
    {
        public Truck()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;
        private void Truck_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Truck", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgv_Truck.DataSource = ds.Tables[0];
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Truck values(@TradeName,@ArrivalTime,@TruckNumber,@Rent)", con);

            con.Open();
            cmd.Parameters.AddWithValue("@TradeName", txtTradeNumber.Text);
            cmd.Parameters.AddWithValue("@ArrivalTime", txtArrivalTime.Text);
            cmd.Parameters.AddWithValue("@TruckNumber", txtTruckNumber.Text);
            cmd.Parameters.AddWithValue("@Rent", txtRent.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Truck record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Truck_Load(null, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Truck set TradeNo=@TradeNo,ArrivalTime=@ArrivalTime,TruckNumber=@TruckNumber,Rent=@Rent where Id= '" + ID + "'", con);
            con.Open();

            cmd.Parameters.AddWithValue("@TradeNo", txtTradeNumber.Text);
            cmd.Parameters.AddWithValue("@ArrivalTime", txtArrivalTime.Text);
            cmd.Parameters.AddWithValue("@TruckNumber", txtTruckNumber.Text);
            cmd.Parameters.AddWithValue("@Rent", txtRent.Text);
           

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Truck record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Truck_Load(this, null);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Truck where Id = '" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("Truck Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Truck_Load(this, null);
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
            txtTradeNumber.Clear();
            txtArrivalTime.Clear();
            txtTruckNumber.Clear();
            txtRent.Clear();

        }

        private void dgv_Truck_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_Truck.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgv_Truck.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                SqlCommand cmd = new SqlCommand("Select * from tb_Truck where Id= '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                //Filling the table data in the respective text boxes
                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtTradeNumber.Text = ds.Tables[0].Rows[0][1].ToString();

                txtArrivalTime.Text = ds.Tables[0].Rows[0][2].ToString();
                txtTruckNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                txtRent.Text = ds.Tables[0].Rows[0][4].ToString();              

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
                    SqlCommand cmd = new SqlCommand("Select * from tb_Truck where TradeNo LIKE '" + txtSearch.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgv_Truck.DataSource = ds.Tables[0];
                    //Products_Load(this, null);

                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Truck", con);
                    cmd.Connection = con;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgv_Truck.DataSource = ds.Tables[0];
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
