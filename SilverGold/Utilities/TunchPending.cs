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

namespace SilverGold.Utilities
{
    public partial class TunchPending : Form
    {

        #region Declare Variable
        OleDbConnection con;
        OleDbTransaction Tran = null;

        public int _Showtunch = 0;
        string _StrTunchPending = "";

        #endregion




        public TunchPending()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
        }

        #region Mapper




        public void GetSqlQuery(string sql)
        {
            _StrTunchPending = sql;
        }

        #endregion

        private void TunchPending_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnExit;
            con = new OleDbConnection();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

            OleDbDataAdapter da = new OleDbDataAdapter(_StrTunchPending, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void txtPartyName_TextChanged(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
            
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            { this.Close(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
