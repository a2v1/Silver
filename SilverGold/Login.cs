using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class Login : Form
    {

        #region Declare Variable

        Boolean CheckCompanyDir = false;
       
        #endregion
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var directoryInfo = new System.IO.DirectoryInfo(Application.StartupPath);
            var dirName = directoryInfo.GetDirectories();

            for (int i = 0; i < dirName.Count(); i++)
            {
                var mainDir = dirName[i].GetDirectories();
                FileInfo[] Files;
                foreach (var item in mainDir)
                {
                    DirectoryInfo d = new DirectoryInfo(item.FullName);
                    Files = d.GetFiles("*.mdb");
                    foreach (FileInfo file in Files)
                    {

                        CheckCompanyDir = true;

                        ValidateLogin(item.FullName, file.Name, txtUserId.Text.Trim(), txtPassword.Text.Trim());
                    }
                }
            }

            if (CommanHelper.CompanyLogin.Count() == 0)
            {
                MessageBox.Show("Invalid Userid And Password !!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUserId.Focus();
                return;
            }
            else
            {
                if (CheckCompanyDir == true)
                {
                    CompanyDetails oCompanyDetails = new CompanyDetails();
                    oCompanyDetails.Show();
                    this.Hide();
                }
                else
                {
                    Master oMaster = new Master();
                    oMaster.Show();
                    this.Hide();
                }
            }

          
        }

        #region Helper

        private void ValidateLogin(String _DataPath, String _DataBase, String Uid, String Pwd)
        {   using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(_DataPath, _DataBase)))
            {
                string _list_FinYear = "";
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select UserId,Pwd,UserType,Company,DateFrom,DateTo,CompanyName,FinancialYear,DatabasePath,DataBaseName from Users Left Outer Join Company On Users.Company = Company.CompanyName Where UserId = '" + Uid + "' and Pwd = '" + Pwd + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    CompanyLoginEntity oCompanyLoginEntity = new CompanyLoginEntity();
                    oCompanyLoginEntity.UserId = dr["UserId"].ToString();
                    oCompanyLoginEntity.Password = dr["Pwd"].ToString();
                    oCompanyLoginEntity.CompanyName = dr["CompanyName"].ToString();
                    oCompanyLoginEntity.DataBaseName = dr["DataBaseName"].ToString();
                    oCompanyLoginEntity.DataBasePath = dr["DatabasePath"].ToString();
                    oCompanyLoginEntity.DateFrom = dr["DateFrom"].ToString();
                    oCompanyLoginEntity.DateTo = dr["DateTo"].ToString();
                    oCompanyLoginEntity.FinancialYear = dr["FinancialYear"].ToString();
                    CommanHelper.CompanyLogin.Add(oCompanyLoginEntity);

                    //_list_FinYear = dr["FinancialYear"].ToString();
                    // _list_FinYear = _list_FinYear.Substring(0, 4) + "-" + _list_FinYear.Substring(4, 4);
                    // strCompany = dr["CompanyName"].ToString() + "  (" + _list_FinYear + ")".ToString();
                }
                con.Close();
            }
           
        }

        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
