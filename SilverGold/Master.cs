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
    }
}
