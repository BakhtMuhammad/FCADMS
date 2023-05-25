using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fruitCommissionAgent
{
    public partial class Home : Form
    {
        private object netAmount;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }



        private void farmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Farmer farmer = new Farmer();
            farmer.Show();
        }

        private void fruitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fruit fruit = new Fruit();
            fruit.Show();
        }

        private void netAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetAmount netAmount = new NetAmount();
            netAmount.Show();
            
        }

        private void trucksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Truck truck= new Truck();   
            truck.Show();
        }

        private void expenditureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Expanditure exp = new Expanditure();
            exp.Show();
        }
    }
}
