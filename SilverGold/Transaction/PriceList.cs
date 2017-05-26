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
    public partial class PriceList : Form
    {
        #region Declare Variable
        OleDbConnection con;
        ConnectionClass objCon;
        public String _TranType = "";
        #endregion
        public PriceList()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate2(dataGridView1);
        }

        private void PriceList_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            con = new OleDbConnection();
            objCon = new ConnectionClass();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPartyName_PriseList_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbPartyName_PriseList.BackColor = Color.Cyan;
                CommanHelper.GetParty(cmbPartyName_PriseList, "PARTY");
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbPartyName_PriseList_Leave(object sender, EventArgs e)
        {
            try
            { cmbPartyName_PriseList.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbPartyName_PriseList_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) cmbProduct_PriceList.Focus();
                CommanHelper.ComboBoxItem(cmbProduct_PriceList, "Product", "ProductName");
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbProduct_PriceList_Enter(object sender, EventArgs e)
        {
            try
            { cmbProduct_PriceList.BackColor = Color.Cyan; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbProduct_PriceList_Leave(object sender, EventArgs e)
        {
            try
            { cmbProduct_PriceList.BackColor = Color.White; }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbProduct_PriceList_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)dtpFrom.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dtpFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            { if (e.KeyChar == 13)dtpTo.Focus(); }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dtpTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("Select format([TrDate],\"dd/MM/yyyy\") as [Date],Westage,LabourRs From PriceList Where PartyName='" + cmbPartyName_PriseList.Text + "' And Product='" + cmbProduct_PriceList.Text + "' And [TrDate]>= #" + Conversion.GetDateStr(dtpFrom.Text.Trim()) + "# And [TrDate]<= #" + Conversion.GetDateStr(dtpTo.Text.Trim()) + "#  and TranType='" + _TranType + "' Order by [TrDate] desc", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    this.dataGridView1.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns["Westage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView1.Columns["LabourRs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void PriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _TranType = "";
        }
    }
}
