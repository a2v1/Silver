using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class JamaNaamEntity
    {
        public string PGroup { get; set; }
        public string Product { get; set; }
        public decimal Weight { get; set; }
        public decimal Pcs { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Westage { get; set; }
        public decimal LaboursRate { get; set; }
        public decimal Fine { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public int TunchSno { get; set; }
        public int Sno { get; set; }


        public void AddJamaNaam(String _PGroup, String _Product, Decimal _Weight, Decimal _Pcs, Decimal _Tunch1, Decimal _Tunch2, Decimal _Westage, Decimal _LaboursRate, Decimal _Fine, Decimal _Amount, String _Narration, int _TunchSno, int _Sno)
        {
            PGroup = _PGroup;
            Product = _Product;
            Weight = _Weight;
            Pcs = _Pcs;
            Tunch1 = _Tunch1;
            Tunch2 = _Tunch2;
            Westage = _Westage;
            LaboursRate = _LaboursRate;
            Fine = _Fine;
            Amount = _Amount;
            Narration = _Narration;
            TunchSno = _TunchSno;
            Sno = _Sno;
        }

        public void InsertJamaNaam(String _BillNo, DateTime _TrDate, String _MetalCategory, String _Category, String _PartyName, String _PGroup, String _Product, Decimal _Weight, Decimal _Pcs, Decimal _Tunch1, Decimal _Tunch2, Decimal _Westage, Decimal _LaboursRate, Decimal _Debit, Decimal _Credit, Decimal _Amount, string _Narration, String _TranType, String _EntryFrom, int _TunchSno, String _Company, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();

            strInsert = "INSERT into PartyTran( BillNo,TrDate,MetalCategory,Category,PartyName,PGroup, Product, Weight, Pcs, Tunch1, Tunch2, Westage, LaboursRate,Debit, Credit, Amount, Narration,TranType,EntryFrom,TunchSno,Company,UserId ) Values ( @BillNo,@TrDate,@MetalCategory,@Category,@PartyName,@PGroup, @Product, @Weight, @Pcs, @Tunch1, @Tunch2, @Westage, @LaboursRate,@Debit, @Credit, @Amount, @Narration,@TranType,@EntryFrom,@TunchSno,@Company,@UserId )";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }
            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@BillNo", _BillNo);
            cmdInsert.Parameters.AddWithValue("@TrDate", _TrDate);
            cmdInsert.Parameters.AddWithValue("@MetalCategory", _MetalCategory);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@PGroup", _PGroup);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@Pcs", _Pcs);
            cmdInsert.Parameters.AddWithValue("@Tunch1", _Tunch1);
            cmdInsert.Parameters.AddWithValue("@Tunch2", _Tunch2);
            cmdInsert.Parameters.AddWithValue("@Westage", _Westage);
            cmdInsert.Parameters.AddWithValue("@LaboursRate", _LaboursRate);
            cmdInsert.Parameters.AddWithValue("@Debit", _Debit);
            cmdInsert.Parameters.AddWithValue("@Credit", _Credit);
            cmdInsert.Parameters.AddWithValue("@Amount", _Amount);
            cmdInsert.Parameters.AddWithValue("@Narration", _Narration);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@EntryFrom", _EntryFrom);
            cmdInsert.Parameters.AddWithValue("@TunchSno", _TunchSno);            
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }

        public void GetBillNo_ListBox(ListBox _ListBox, DateTime _Date, String _TranType, OleDbConnection _Con)
        {
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Billno,PartyName from PartyTran where TrDate=#" + _Date + "# And TranType='" + _TranType + "' Group by billno,PartyName", _Con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _ListBox.Items.Add(dr[0].ToString() + "..." + dr[1].ToString());
            }
            dr.Close();
            _Con.Close();
        }


        public void BindGridColumn(DataGridView grd)
        {

            DataGridViewColumn col_Group = new DataGridViewTextBoxColumn();
            col_Group.DataPropertyName = "PGroup";
            col_Group.HeaderText = "PGroup";
            col_Group.Name = "PGroup";
            grd.Columns.Add(col_Group);

            DataGridViewColumn col_Product = new DataGridViewTextBoxColumn();
            col_Product.DataPropertyName = "Product";
            col_Product.HeaderText = "Product";
            col_Product.Name = "Product";
            grd.Columns.Add(col_Product);

            DataGridViewColumn col_Weight = new DataGridViewTextBoxColumn();
            col_Weight.DataPropertyName = "Weight";
            col_Weight.HeaderText = "Weight";
            col_Weight.Name = "Weight";
            grd.Columns.Add(col_Weight);

            DataGridViewColumn col_Pcs = new DataGridViewTextBoxColumn();
            col_Pcs.DataPropertyName = "Pcs";
            col_Pcs.HeaderText = "Pcs";
            col_Pcs.Name = "Pcs";
            grd.Columns.Add(col_Pcs);

            DataGridViewColumn col_Tunch1 = new DataGridViewTextBoxColumn();
            col_Tunch1.DataPropertyName = "Tunch1";
            col_Tunch1.HeaderText = "Tunch";
            col_Tunch1.Name = "Tunch1";
            grd.Columns.Add(col_Tunch1);

            DataGridViewColumn col_Tunch2 = new DataGridViewTextBoxColumn();
            col_Tunch2.DataPropertyName = "Tunch2";
            col_Tunch2.HeaderText = "Tunch2";
            col_Tunch2.Name = "Tunch2";
            grd.Columns.Add(col_Tunch2);

            DataGridViewColumn col_Westage = new DataGridViewTextBoxColumn();
            col_Westage.DataPropertyName = "Westage";
            col_Westage.HeaderText = "Westage";
            col_Westage.Name = "Westage";
            grd.Columns.Add(col_Westage);

            DataGridViewColumn col_LabourFine = new DataGridViewTextBoxColumn();
            col_LabourFine.DataPropertyName = "LaboursRate";
            col_LabourFine.HeaderText = "LaboursFine";
            col_LabourFine.Name = "LaboursRate";
            grd.Columns.Add(col_LabourFine);

            DataGridViewColumn col_Fine = new DataGridViewTextBoxColumn();
            col_Fine.DataPropertyName = "Fine";
            col_Fine.HeaderText = "Fine";
            col_Fine.Name = "Fine";
            grd.Columns.Add(col_Fine);

            DataGridViewColumn col_Amount = new DataGridViewTextBoxColumn();
            col_Amount.DataPropertyName = "Amount";
            col_Amount.HeaderText = "Amount";
            col_Amount.Name = "Amount";
            grd.Columns.Add(col_Amount);

            DataGridViewColumn col_Narration = new DataGridViewTextBoxColumn();
            col_Narration.DataPropertyName = "Narration";
            col_Narration.HeaderText = "Narration";
            col_Narration.Name = "Narration";
            grd.Columns.Add(col_Narration);

            DataGridViewColumn col_TunchSno = new DataGridViewTextBoxColumn();
            col_TunchSno.DataPropertyName = "TunchSno";
            col_TunchSno.HeaderText = "TunchSno";
            col_TunchSno.Name = "TunchSno";
            col_TunchSno.Visible = false;
            grd.Columns.Add(col_TunchSno);

            DataGridViewColumn col_Sno = new DataGridViewTextBoxColumn();
            col_Sno.DataPropertyName = "Sno";
            col_Sno.HeaderText = "Sno";
            col_Sno.Name = "Sno";
            col_Sno.Visible = false;
            grd.Columns.Add(col_Sno);


        }

        public void SetCreditLimitGridView_ColumnWith(DataGridView grd)
        {
            grd.Columns["PGroup"].Width = 40;
            grd.Columns["Product"].Width = 105;
            grd.Columns["Weight"].Width = 55;
            grd.Columns["Pcs"].Width = 48;
            grd.Columns["Tunch1"].Width = 48;
            grd.Columns["Tunch2"].Width = 48;
            grd.Columns["Westage"].Width = 55;
            grd.Columns["LaboursRate"].Width = 60;
            grd.Columns["Fine"].Width = 60;
            grd.Columns["Amount"].Width = 65;

            grd.Columns["Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Pcs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Tunch1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Tunch2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Westage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["LaboursRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Fine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


    }
}
