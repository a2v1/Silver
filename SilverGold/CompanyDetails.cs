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

        #region Helper

        private void ValidateCompany()
        {

            String _Company_Name = CommanHelper.FilterCompany(listBox1.SelectedItem.ToString(), "(");
            var result = CommanHelper.CompanyLogin.ToList().Where(x => (x.CompanyName == _Company_Name.Trim()) && (x.UserId == CommanHelper.UserId.Trim()) && (x.Password == CommanHelper.Password.Trim())).FirstOrDefault();

            if (result != null)
            {
                CommanHelper.CompName = _Company_Name;
                CommanHelper.FDate = result.DateFrom.ToString();
                CommanHelper.TDate = result.DateTo.ToString();
                CommanHelper._FinancialYear = result.FinancialYear.ToString();
                CommanHelper.Com_DB_PATH = result.DataBasePath.ToString();
                CommanHelper.Com_DB_NAME = result.DataBaseName.ToString();

                this.Visible = false;
                Master oMaster = new Master();
                oMaster.Show();
            }
        }

        #endregion

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if ((listBox1.SelectedItem??(object)"").ToString() != "")
                    {

                        ValidateCompany();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((listBox1.SelectedItem ?? (object)"").ToString() != "")
                {
                    ValidateCompany();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
