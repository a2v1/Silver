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
            cmdInsert.Parameters.AddWithValue("@PartyCat", _PartyCate);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@TunchValue1", _TunchValue1);
            cmdInsert.Parameters.AddWithValue("@TunchValue2", _TunchValue2);
            cmdInsert.Parameters.AddWithValue("@Tunch1", _Tunch1);
            cmdInsert.Parameters.AddWithValue("@Tunch2", _Tunch2);
            cmdInsert.Parameters.AddWithValue("@InvoiceType", _InvoiceType);
            cmdInsert.Parameters.AddWithValue("@TunchSno", _TunchSno);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }


        private void BindGridColumn(DataGridView dgv)
        {
            DataGridViewColumn col_Group = new DataGridViewTextBoxColumn();
            col_Group.DataPropertyName = "PGroup";
            col_Group.HeaderText = "PGroup";
            col_Group.Name = "PGroup";
            dgv.Columns.Add(col_Group);

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

            DataGridViewColumn col_Pcs = new DataGridViewTextBoxColumn();
            col_Pcs.DataPropertyName = "Pcs";
            col_Pcs.HeaderText = "Pcs";
            col_Pcs.Name = "Pcs";
            dgv.Columns.Add(col_Pcs);

            DataGridViewColumn col_Tunch1 = new DataGridViewTextBoxColumn();
            col_Tunch1.DataPropertyName = "Tunch1";
            col_Tunch1.HeaderText = "Tunch";
            col_Tunch1.Name = "Tunch1";
            dgv.Columns.Add(col_Tunch1);

            DataGridViewColumn col_Tunch2 = new DataGridViewTextBoxColumn();
            col_Tunch2.DataPropertyName = "Tunch2";
            col_Tunch2.HeaderText = "Tunch2";
            col_Tunch2.Name = "Tunch2";
            dgv.Columns.Add(col_Tunch2);

            DataGridViewColumn col_Westage = new DataGridViewTextBoxColumn();
            col_Westage.DataPropertyName = "Westage";
            col_Westage.HeaderText = "Westage";
            col_Westage.Name = "Westage";
            dgv.Columns.Add(col_Westage);

            DataGridViewColumn col_LabourFine = new DataGridViewTextBoxColumn();
            col_LabourFine.DataPropertyName = "LaboursRate";
            col_LabourFine.HeaderText = "LaboursFine";
            col_LabourFine.Name = "LaboursRate";
            dgv.Columns.Add(col_LabourFine);

            DataGridViewColumn col_Fine = new DataGridViewTextBoxColumn();
            col_Fine.DataPropertyName = "Fine";
            col_Fine.HeaderText = "Fine";
            col_Fine.Name = "Fine";
            dgv.Columns.Add(col_Fine);

            DataGridViewColumn col_Amount = new DataGridViewTextBoxColumn();
            col_Amount.DataPropertyName = "Amount";
            col_Amount.HeaderText = "Amount";
            col_Amount.Name = "Amount";
            dgv.Columns.Add(col_Amount);

            DataGridViewColumn col_Narration = new DataGridViewTextBoxColumn();
            col_Narration.DataPropertyName = "Narration";
            col_Narration.HeaderText = "Narration";
            col_Narration.Name = "Narration";
            dgv.Columns.Add(col_Narration);

            DataGridViewColumn col_TunchSno = new DataGridViewTextBoxColumn();
            col_TunchSno.DataPropertyName = "TunchSno";
            col_TunchSno.HeaderText = "TunchSno";
            col_TunchSno.Name = "TunchSno";
            col_TunchSno.Visible = false;
            dgv.Columns.Add(col_TunchSno);

            DataGridViewColumn col_Sno = new DataGridViewTextBoxColumn();
            col_Sno.DataPropertyName = "Sno";
            col_Sno.HeaderText = "Sno";
            col_Sno.Name = "Sno";
            col_Sno.Visible = false;
            dgv.Columns.Add(col_Sno);


        }

    }
}
