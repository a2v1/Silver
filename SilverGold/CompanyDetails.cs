using SilverGold.Entity;
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
    public partial class CompanyDetails : Form
    {
        public CompanyDetails()
        {
            InitializeComponent();
        }

        private void CompanyDetails_Load(object sender, EventArgs e)
        {
            var result = CommanHelper.CompanyLogin.Select(x => new CompanyLoginEntity
            {
                CompanyName = x.CompanyName,
                FinancialYear = x.FinancialYear.Substring(0, 4) + "-" + x.FinancialYear.Substring(4, 4)
            }).ToList();
            foreach (var itemValue in result)
            {
                listBox1.Items.Add(itemValue.CompanyName + " (" + itemValue.FinancialYear + ")");
            }
           listBox1.Focus(); 
        }

        private void CompanyDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
