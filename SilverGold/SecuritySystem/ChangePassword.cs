using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.SecuritySystem
{
    public partial class ChangePassword : Form
    {
        #region Declare Variable
        OleDbConnection con;
        OleDbTransaction Tran;
        #endregion

        public ChangePassword()
        {
            InitializeComponent();
        }

        #region Helper

        private void ClearControl()
        {
            txtUserId.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            cmbUserType.SelectedIndex = -1;
            cmbUserType.Text = "";
            txtUserId.Focus();
        }

        #endregion

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            con = new OleDbConnection();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
            this.toolStripMenuItem1.Click += new EventHandler(btnSave_Click);
            this.toolStripMenuItem2.Click += new EventHandler(btnRefresh_Click);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text.Trim() == "")
                {
                    txtUserId.Focus();
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    txtPassword.Focus();
                    return;
                }
                if (txtConfirmPassword.Text.Trim() == "")
                {
                    txtConfirmPassword.Focus();
                    return;
                }
                if (cmbUserType.Text.Trim() == "")
                {
                    cmbUserType.Focus();
                    return;
                }

                if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    MessageBox.Show("Confirm Password not match.Enter Correct Confirm Password", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtConfirmPassword.Focus();
                    return;
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    Tran = con.BeginTransaction();
                }
                OleDbCommand cmd = new OleDbCommand("DELETE FROM USERS", con, Tran);
                cmd.ExecuteNonQuery();
                UserFactory.Insert(txtUserId.Text.Trim(), txtConfirmPassword.Text.Trim(), cmbUserType.Text.Trim(), CommanHelper.CompName, con, Tran);

                Tran.Commit();
                MessageBox.Show("Data Successfully Saved.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                Tran.Rollback();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            try
            {
                txtUserId.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { txtPassword.Focus(); } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtUserId_Leave(object sender, EventArgs e)
        {
            try
            { txtUserId.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            try
            { txtPassword.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { txtConfirmPassword.Focus(); } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            try
            { txtPassword.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            try
            { txtConfirmPassword.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbUserType.Focus();
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                txtConfirmPassword.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbUserType_Enter(object sender, EventArgs e)
        {
            try
            { cmbUserType.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbUserType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { btnSave.Focus(); } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbUserType_Leave(object sender, EventArgs e)
        {
            try
            { cmbUserType.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }
    }
}
