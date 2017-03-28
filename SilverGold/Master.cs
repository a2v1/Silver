using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class Master : Form
    {
        private int childFormNumber = 0;
        public static Master objMaster;
        public Master()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }


        private void Master_Load(object sender, EventArgs e)
        {
            objMaster = this;
            CommanHelper.FormX = this.Width;
            CommanHelper.FormY = this.Height;
        }

        private void Master_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void createCompToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Company oCompany = new Company();
            oCompany.MdiParent = this;
            oCompany.Show();
        }

        private void createItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Items oItems = new Items();
            oItems.MdiParent = this;
            oItems.Show();
        }

        private void partyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartyInformation oPartyInformation = new PartyInformation();
            oPartyInformation.MdiParent = this;
            oPartyInformation.Show();
        }

        private void productDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductDetails oProductDetails = new ProductDetails();
            oProductDetails.MdiParent = this;
            oProductDetails.Show();
        }

        private void labourRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ghattakListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void introducerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupHeadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupHead oGroupHead = new GroupHead();
            oGroupHead.MdiParent = this;
            oGroupHead.Show();
        }
    }
}
