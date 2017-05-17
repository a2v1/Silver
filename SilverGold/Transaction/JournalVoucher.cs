using SilverGold.Comman;
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
    public partial class JournalVoucher : Form
    {
        #region Declare Variables
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        DataGridView.HitTestInfo hti;


        #endregion
        public JournalVoucher()
        {
            InitializeComponent();
        }

        private void JournalVoucher_Load(object sender, EventArgs e)
        {
            //this.CancelButton = btnClose;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            con = new OleDbConnection();
            objCon = new ConnectionClass();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
