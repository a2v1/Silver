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
        int _LoginCount = 0;
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
                if (CommanHelper._CompName_ChangeComapny != "")
                {
                    if (ValidateLogin(CommanHelper._Com_DB_PATH_ChangeComapny, CommanHelper._Com_DB_NAME_ChangeComapny, txtUserId.Text.Trim(), txtPassword.Text.Trim()) == true)
                    {
                        this.Hide();
                        Master.objMaster.Hide();
                        Master oMaster = new Master();
                        oMaster.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid id And Password !!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtUserId.Focus();
                        return;
                    }
                }
                else
                {
                    CommanHelper.UserId = txtUserId.Text.Trim();
                    CommanHelper.Password = txtPassword.Text.Trim();

                    if (ValidateLogin(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME, txtUserId.Text.Trim(), txtPassword.Text.Trim()) == true)
                    {
                        Master oMaster = new Master();
                        oMaster.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid id And Password !!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtUserId.Focus();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        #region Helper

        private Boolean ValidateLogin(String _DataPath, String _DataBase, String Uid, String Pwd)
        {
            Boolean _CheckValidateLogin = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(Application.StartupPath + "\\" + _DataPath, _DataBase + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select UserId,Pwd,UserType,Company,DateFrom,DateTo,CompanyName,FinancialYear,DatabasePath,DataBaseName from Users Left Outer Join Company On Users.Company = Company.CompanyName Where UserId = '" + Uid + "' and Pwd = '" + Pwd + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (CommanHelper._CompName_ChangeComapny != "")
                        {
                            CommanHelper.CompName = CommanHelper._CompName_ChangeComapny.ToString();
                            CommanHelper._FinancialYear = CommanHelper._FinancialYear_ChangeComapny.ToString();
                            CommanHelper.Com_DB_PATH = CommanHelper._Com_DB_PATH_ChangeComapny.ToString();
                            CommanHelper.Com_DB_NAME = CommanHelper._Com_DB_NAME_ChangeComapny.ToString();
                        }
                        if ((CommanHelper.CompName == dr["CompanyName"].ToString().Trim()) || (CommanHelper._FinancialYear == dr["FinancialYear"].ToString()) || (CommanHelper.Com_DB_PATH == dr["DatabasePath"].ToString()) || (CommanHelper.Com_DB_NAME == dr["DataBaseName"].ToString()))
                        {
                            _CheckValidateLogin = true;
                        }
                    }

                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return _CheckValidateLogin;
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

        private void btnLogin_Enter(object sender, EventArgs e)
        {
            _LoginCount++;
            if (_LoginCount == 3)
            {
                Application.Exit();
            }

        }
    }
}
