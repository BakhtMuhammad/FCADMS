using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace fruitCommissionAgent
{
    public partial class Fruit : Form
    {

        public Fruit()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;
        private void Fruit_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Fruit", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvFruit.DataSource = ds.Tables[0];
        }
        public void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Fruit values(@FCNIC,@Date,@FruitName,@StockNO,@SalePerStock, @TradeNo)", con);
            
            con.Open();
            cmd.Parameters.AddWithValue("@FCNIC", txtFCNIC.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.Parameters.AddWithValue("@FruitName", txtFruitName.Text);
            cmd.Parameters.AddWithValue("@StockNO", txtStockNumber.Text);
            cmd.Parameters.AddWithValue("@SalePerStock", txtSalePerStock.Text);
            cmd.Parameters.AddWithValue("@TradeNo", txtTradeNumber.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Fruit record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Fruit_Load(null, null);
            clear();
        }

        private void clear()
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Fruit set FCNIC=@FCNIC,Date=@Date,FruitName=@FruitName,StockNO=@StockNO,SalePerStock=@SalePerStock,TradeNo=@TradeNo where Id= '" + ID + "'", con);

            con.Open();
            // cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@FCNIC", txtFCNIC.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);
            cmd.Parameters.AddWithValue("@FruitName", txtFruitName.Text);
            cmd.Parameters.AddWithValue("@StockNO", txtStockNumber.Text);
            cmd.Parameters.AddWithValue("@SalePerStock", txtSalePerStock.Text);
            cmd.Parameters.AddWithValue("@TradeNo", txtTradeNumber.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Student updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Fruit_Load(this, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Fruit where Id='" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("Fruit Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Fruit_Load(this, null);
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
            txtFCNIC.Clear();
            txtDate.Clear();
            txtFruitName.Clear();
            txtStockNumber.Clear();
            txtSalePerStock.Clear();
            txtTradeNumber.Clear();
        }

        private void dgvFruit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvFruit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvFruit.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                SqlCommand cmd = new SqlCommand("Select * from tb_Fruit where Id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                txtFCNIC.Text = ds.Tables[0].Rows[0][1].ToString();
                txtDate.Text = ds.Tables[0].Rows[0][2].ToString();
                txtFruitName.Text = ds.Tables[0].Rows[0][3].ToString();
                txtStockNumber.Text = ds.Tables[0].Rows[0][4].ToString();
                txtSalePerStock.Text = ds.Tables[0].Rows[0][5].ToString();
                txtTradeNumber.Text = ds.Tables[0].Rows[0][6].ToString();
   

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
                    SqlCommand cmd = new SqlCommand("Select * from tb_Fruit where FCNIC LIKE '" + txtSearch.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvFruit.DataSource = ds.Tables[0];
                    //Products_Load(this, null);

                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Fruit", con);
                    cmd.Connection = con;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvFruit.DataSource = ds.Tables[0];
                    //Products_Load(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFCNIC_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
