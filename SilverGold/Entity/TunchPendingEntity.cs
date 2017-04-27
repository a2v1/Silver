using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class TunchPendingEntity
    {
        public string BillNo { get; set; }
        public DateTime TrDate { get; set; }
        public string PartyCate { get; set; }
        public string PartyName { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public decimal Weight { get; set; }
        public decimal TunchValue1 { get; set; }
        public decimal TunchValue2 { get; set; }
        public string Tunch1 { get; set; }
        public string Tunch2 { get; set; }
        public string InvoiceType { get; set; }
        public int TunchSno { get; set; }
        public string Company { get; set; }
        public string UserId { get; set; }

        #region Add Tunch Pending List

        public void AddTunchPending(String _BillNo, DateTime _TrDate, String _PartyCate, String _PartyName, String _Category, String _Product, Decimal _Weight, Decimal _TunchValue1, Decimal _TunchValue2, String _Tunch1, String _Tunch2, String _InvoiceType, int _TunchSno, String _Company, String _UserId)
        {
            BillNo = _BillNo;
            TrDate = _TrDate;
            PartyCate = _PartyCate;
            PartyName = _PartyName;
            Category = _Category;
            Product = _Product;
            Weight = _Weight;
            TunchValue1 = _TunchValue1;
            TunchValue2 = _TunchValue2;
            Tunch1 = _Tunch1;
            Tunch2 = _Tunch2;
            InvoiceType = _InvoiceType;
            TunchSno = _TunchSno;
            Company = _Company;
            UserId = _UserId;
        }

        #endregion

        #region INSERT

        public void InsertTunchPending(String _BillNo, DateTime _TrDate, String _PartyCate, String _PartyName, String _Category, String _Product, Decimal _Weight, Decimal _TunchValue1, Decimal _TunchValue2, String _Tunch1, String _Tunch2, String _InvoiceType, int _TunchSno, String _Company, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }
            strInsert = "INSERT INTO TunchPending(BillNo,TrDate,PartyCate,PartyName,Category,Product,Weight,TunchValue1,TunchValue2,Tunch1,Tunch2,InvoiceType,TunchSno,Company,UserId )VALUES(@BillNo,@TrDate,@PartyCate,@PartyName,@Category,@Product,@Weight,@TunchValue1,@TunchValue2,@Tunch1,@Tunch2,@InvoiceType,@TunchSno,@Company,@UserId)";
            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@BillNo", _BillNo);
            cmdInsert.Parameters.AddWithValue("@TrDate", _TrDate);
            cmdInsert.Parameters.AddWithValue("@PartyCate", _PartyCate);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@TunchValue1", _TunchValue1);
            cmdInsert.Parameters.AddWithValue("@TunchValue1", _TunchValue2);
            cmdInsert.Parameters.AddWithValue("@Tunch1", _Tunch1);
            cmdInsert.Parameters.AddWithValue("@Tunch2", _Tunch2);
            cmdInsert.Parameters.AddWithValue("@InvoiceType", _InvoiceType);
            cmdInsert.Parameters.AddWithValue("@TunchSno", _TunchSno);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }

        #endregion


        #region Bind Column In Datagridview

        public void BindGridColumn(DataGridView dgv)
        {
            DataGridViewColumn col_BillNo = new DataGridViewTextBoxColumn();
            col_BillNo.DataPropertyName = "BillNo";
            col_BillNo.HeaderText = "BillNo";
            col_BillNo.Name = "BillNo";
            col_BillNo.Visible = false;
            dgv.Columns.Add(col_BillNo);

            DataGridViewColumn col_TrDate = new DataGridViewTextBoxColumn();
            col_TrDate.DataPropertyName = "TrDate";
            col_TrDate.HeaderText = "Date";
            col_TrDate.Name = "TrDate";
            dgv.Columns.Add(col_TrDate);

            DataGridViewColumn col_PartyName = new DataGridViewTextBoxColumn();
            col_PartyName.DataPropertyName = "PartyName";
            col_PartyName.HeaderText = "PartyName";
            col_PartyName.Name = "PartyName";
            dgv.Columns.Add(col_PartyName);

            DataGridViewColumn col_Category = new DataGridViewTextBoxColumn();
            col_Category.DataPropertyName = "Category";
            col_Category.HeaderText = "Category";
            col_Category.Name = "Category";
            dgv.Columns.Add(col_Category);

            DataGridViewColumn col_Product = new DataGridViewTextBoxColumn();
            col_Product.DataPropertyName = "Product";
            col_Product.HeaderText = "Product";
            col_Product.Name = "Product";
            dgv.Columns.Add(col_Product);

            DataGridViewColumn col_Weight = new DataGridViewTextBoxColumn();
            col_Weight.DataPropertyName = "Weight";
            col_Weight.HeaderText = "Weight";
            col_Weight.Name = "Weight";            
            dgv.Columns.Add(col_Weight);

            DataGridViewColumn col_TunchValue1  = new DataGridViewTextBoxColumn();
            col_TunchValue1.DataPropertyName = "TunchValue1";
            col_TunchValue1.HeaderText = "Tunch1";
            col_TunchValue1.Name = "TunchValue1";
            dgv.Columns.Add(col_TunchValue1);

            DataGridViewColumn col_TunchValue2 = new DataGridViewTextBoxColumn();
            col_TunchValue2.DataPropertyName = "TunchValue2";
            col_TunchValue2.HeaderText = "Tunch2";
            col_TunchValue2.Name = "TunchValue2";
            dgv.Columns.Add(col_TunchValue2);

            DataGridViewColumn col_InvoiceType = new DataGridViewTextBoxColumn();
            col_InvoiceType.DataPropertyName = "InvoiceType";
            col_InvoiceType.HeaderText = "Type";
            col_InvoiceType.Name = "InvoiceType";
            dgv.Columns.Add(col_InvoiceType);

            DataGridViewColumn col_TunchSno = new DataGridViewTextBoxColumn();
            col_TunchSno.DataPropertyName = "TunchSno";
            col_TunchSno.HeaderText = "TunchSno";
            col_TunchSno.Name = "TunchSno";
            col_TunchSno.Visible = false;
            dgv.Columns.Add(col_TunchSno);
            
        }

        #endregion

    }
}
