using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using SilverGold.Transaction;
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
      //  OleDbTransaction Tran = null;
        DataGridView.HitTestInfo ht1;
        public int _Showtunch = 0;
        string _StrTunchPending = "";
        TunchPendingEntity oTunchPendingEntity;
        List<TunchPendingEntity> TunchPendingList = new List<TunchPendingEntity>();
        #endregion




        public TunchPending()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            oTunchPendingEntity = new TunchPendingEntity();
            oTunchPendingEntity.BindGridColumn(dataGridView1);


        }

        #region Mapper

        public void GetSqlQuery(string sql)
        {
            _StrTunchPending = sql;
        }


        private void BindTunchPending()
        {
            try
            {
                if (_StrTunchPending != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand(_StrTunchPending, con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    TunchPendingList.Clear();
                    while (dr.Read())
                    {
                        oTunchPendingEntity = new TunchPendingEntity();
                        oTunchPendingEntity.AddTunchPending(dr["BillNo"].ToString(), Conversion.ConToDT(dr["TrDate"].ToString()), dr["PartyCate"].ToString(), dr["PartyName"].ToString(), dr["Category"].ToString(), dr["Product"].ToString(), Conversion.ConToDec(dr["Weight"].ToString()), Conversion.ConToDec(dr["TunchValue1"].ToString()), Conversion.ConToDec(dr["TunchValue1"].ToString()), dr["Tunch1"].ToString(), dr["Tunch2"].ToString(), dr["InvoiceType"].ToString(), Conversion.ConToInt(dr["TunchSno"].ToString()), dr["Company"].ToString(), dr["UserId"].ToString());
                        TunchPendingList.Add(oTunchPendingEntity);
                    }
                    dataGridView1.DataSource = TunchPendingList.Select(x => new
                    {
                        x.BillNo,
                        x.TrDate,
                        x.PartyName,
                        x.Category,
                        x.Product,
                        x.Weight,
                        x.TunchValue1,
                        x.TunchValue2,
                        x.InvoiceType,
                        x.TunchSno
                    }).ToList();

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #endregion

        private void TunchPending_Load(object sender, EventArgs e)
        {

            dataGridView1.Columns["TrDate"].Width = 100;
            dataGridView1.Columns["PartyName"].Width = 185;
            dataGridView1.Columns["Category"].Width = 60;
            dataGridView1.Columns["Product"].Width = 105;
            dataGridView1.Columns["Weight"].Width = 90;

            this.dataGridView1.Columns["Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["TunchValue1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["TunchValue2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oTunchPendingEntity = new TunchPendingEntity();

            this.CancelButton = btnExit;
            con = new OleDbConnection();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");



            BindTunchPending();
        }

        private void txtPartyName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var result = TunchPendingList.Where(x => x.PartyName.ToLower().Contains(txtPartyName.Text.Trim())).ToList();
                dataGridView1.DataSource = result.Select(x => new
                {
                    x.BillNo,
                    x.TrDate,
                    x.PartyName,
                    x.Category,
                    x.Product,
                    x.Weight,
                    x.TunchValue1,
                    x.TunchValue2,
                    x.InvoiceType,
                    x.TunchSno
                }).ToList();

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var result = TunchPendingList.Where(x => x.Product.ToLower().Contains(txtProduct.Text.Trim())).ToList();
                dataGridView1.DataSource = result.Select(x => new
                {
                    x.BillNo,
                    x.TrDate,
                    x.PartyName,
                    x.Category,
                    x.Product,
                    x.Weight,
                    x.TunchValue1,
                    x.TunchValue2,
                    x.InvoiceType,
                    x.TunchSno
                }).ToList();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal _Weight = Conversion.ConToDec(txtWeight.Text.Trim());
                if (txtWeight.Text.Trim() != "")
                {
                    dataGridView1.DataSource = TunchPendingList.Where(x => x.Weight == _Weight).Select(x => new
                    {
                        x.BillNo,
                        x.TrDate,
                        x.PartyName,
                        x.Category,
                        x.Product,
                        x.Weight,
                        x.TunchValue1,
                        x.TunchValue2,
                        x.InvoiceType,
                        x.TunchSno
                    }).ToList();
                }
                else
                {
                    dataGridView1.DataSource = TunchPendingList.Select(x => new
                    {
                        x.BillNo,
                        x.TrDate,
                        x.PartyName,
                        x.Category,
                        x.Product,
                        x.Weight,
                        x.TunchValue1,
                        x.TunchValue2,
                        x.InvoiceType,
                        x.TunchSno
                    }).ToList();
                }
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

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                String _InvoiceType = "";
                String _BillNo = "";
                int _TunchSno = 0;
                ht1 = dataGridView1.HitTest(e.X, e.Y);
                if (ht1.RowIndex >= 0)
                {
                    CommanHelper.F_TunchPending = 1;
                    _InvoiceType = dataGridView1.Rows[ht1.RowIndex].Cells[8].Value.ToString();
                    _BillNo = dataGridView1.Rows[ht1.RowIndex].Cells[0].Value.ToString();
                    _TunchSno = Conversion.ConToInt(dataGridView1.Rows[ht1.RowIndex].Cells[9].Value.ToString());
                    if (_InvoiceType == "GR")
                    {
                        Jama oJama = new Jama();
                        Jama._Flage_TunchPending = 1;
                        Jama._TunchSno_TunchPending = _TunchSno;
                        oJama.Show();
                        oJama.GetDetails(_BillNo);
                    }
                    if (_InvoiceType == "GG")
                    {
                        Naam oNaam = new Naam();
                        Naam._Flage_TunchPending = 1;
                        Naam._TunchSno_TunchPending = _TunchSno;
                        oNaam.Show();
                        oNaam.GetDetails(_BillNo);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    String _InvoiceType = "";
                    String _BillNo = "";
                    int _TunchSno = 0;

                    CommanHelper.F_TunchPending = 1;
                    _InvoiceType = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    _BillNo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    _TunchSno = Conversion.ConToInt(dataGridView1.CurrentRow.Cells[9].Value.ToString());
                    if (_InvoiceType == "GR")
                    {
                        Jama oJama = new Jama();
                        Jama._Flage_TunchPending = 1;
                        Jama._TunchSno_TunchPending = _TunchSno;
                        oJama.Show();
                        oJama.GetDetails(_BillNo);
                    }
                    if (_InvoiceType == "GG")
                    {
                        Naam oNaam = new Naam();
                        Naam._Flage_TunchPending = 1;
                        Naam._TunchSno_TunchPending = _TunchSno;
                        oNaam.Show();
                        oNaam.GetDetails(_BillNo);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
