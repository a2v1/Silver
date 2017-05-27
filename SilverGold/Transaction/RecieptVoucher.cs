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

namespace SilverGold.Transaction
{
    public partial class RecieptVoucher : Form
    {

        #region Declare Variable
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        RecieptPaymentVoucherEntity oRecieptPaymentVoucherEntity = new RecieptPaymentVoucherEntity();

        #endregion

        public RecieptVoucher()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            CommanHelper.ChangeGridFormate(dataGridView2);
        }

        private void RecieptVoucher_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            con = new OleDbConnection();
            objCon = new ConnectionClass();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
            oRecieptPaymentVoucherEntity.BindGridColumn(dataGridView1);
            oRecieptPaymentVoucherEntity.SetGridView_ColumnWith(dataGridView1);
            KFFactory.BindKFColumn(dataGridView2);
            KFFactory.SetKF_ColumnWidth(dataGridView2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            { this.Close(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dtp_Enter(object sender, EventArgs e)
        {
            try
            { panel12.BackColor = Color.RosyBrown; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dtp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)cmbCategory.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dtp_Leave(object sender, EventArgs e)
        {
            try
            { panel12.BackColor = Color.Transparent; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            try
            {
                panel2.BackColor = Color.RosyBrown;
                cmbCategory.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) cmbParty.Focus();
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                panel2.BackColor = Color.Transparent;
                cmbCategory.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbParty_Enter(object sender, EventArgs e)
        {
            try
            {
                panel11.BackColor = Color.RosyBrown;
                cmbParty.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbParty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)cmbProduct.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbParty_Leave(object sender, EventArgs e)
        {
            try
            {
                panel11.BackColor = Color.Transparent;
                cmbParty.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbProduct_Enter(object sender, EventArgs e)
        {
            try
            {
                panel9.BackColor = Color.RosyBrown;
                cmbProduct.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)txtFine.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                panel9.BackColor = Color.Transparent;
                cmbProduct.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtFine_Enter(object sender, EventArgs e)
        {
            try
            { txtFine.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtFine_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtFine_Leave(object sender, EventArgs e)
        {
            try
            { txtFine.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void gross_Enter(object sender, EventArgs e)
        {
            try
            { gross.BackColor = Color.RosyBrown; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void gross_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void gross_Leave(object sender, EventArgs e)
        {
            try
            { gross.BackColor = Color.Transparent; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void net_Enter(object sender, EventArgs e)
        {
            try
            { net.BackColor = Color.RosyBrown; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void net_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void net_Leave(object sender, EventArgs e)
        {
            try
            { net.BackColor = Color.Transparent; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void wt_Enter(object sender, EventArgs e)
        {
            try
            { wt.BackColor = Color.RosyBrown; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void wt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void wt_Leave(object sender, EventArgs e)
        {
            try
            { wt.BackColor = Color.Transparent; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void gram_Enter(object sender, EventArgs e)
        {
            try
            { gram.BackColor = Color.RosyBrown; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void gram_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void gram_Leave(object sender, EventArgs e)
        {
            try
            { gram.BackColor = Color.Transparent; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtPremium_Enter(object sender, EventArgs e)
        {
            try
            { txtPremium.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtPremium_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13) { } }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtPremium_Leave(object sender, EventArgs e)
        {
            try
            { txtPremium.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            try
            { txtAmount.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)txtDescription.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            { txtAmount.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            try
            { txtDescription.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)btnOk.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            try
            { txtDescription.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbPopUp_Enter(object sender, EventArgs e)
        {
            try
            {
                plnpopup.BackColor = Color.RosyBrown;
                cmbPopUp.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbPopUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)dtp.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbPopUp_Leave(object sender, EventArgs e)
        {
            try
            { plnpopup.BackColor = Color.Transparent; cmbPopUp.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            try
            { panel13.BackColor = Color.RosyBrown; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    listBox1.Visible = true; listBox1.Focus();
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            try
            { panel13.BackColor = Color.Transparent; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }
    }
}
