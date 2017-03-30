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
            CommanHelper.CompanyLogin.Clear();
            this.CancelButton = btnClose;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var directoryInfo = new System.IO.DirectoryInfo(Application.StartupPath);
                var dirName = directoryInfo.GetDirectories();

                CommanHelper.UserId = txtUserId.Text.Trim();
                CommanHelper.Password = txtPassword.Text.Trim();
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



                if (CheckCompanyDir == true)
                {
                    if (CommanHelper.CompanyLogin.Count() == 0)
                    {
                        MessageBox.Show("Invalid Userid And Password !!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtUserId.Focus();
                        return;
                    }
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
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        #region Helper

        private void ValidateLogin(String _DataPath, String _DataBase, String Uid, String Pwd)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(_DataPath, _DataBase)))
                {
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
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPassword.Focus();                
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.Focus();
            }
        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            txtUserId.SelectAll();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }
    }
}
