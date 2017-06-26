using SilverGold.Comman;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class CreditPeriodEntity
    {
        public string PartyName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string RateRevised { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public Decimal Westage { get; set; }
        public Decimal Amount { get; set; }
        public string Tran_Type { get; set; }
        public int Days { get; set; }
        public string Company { get; set; }
        public string UserId { get; set; }

        public void CreditPeriodMapper(String _PartyName, DateTime _DateFrom, DateTime _DateTo, string _RateRevised, string _Category, string _Product, Decimal _Westage, Decimal _Amount, string _Tran_Type, int _Days, string _Company, string _UserId)
        {
            PartyName = _PartyName;
            DateFrom = _DateFrom;
            DateTo = _DateTo;
            RateRevised = _RateRevised;
            Category = _Category;
            Product = _Product;
            Westage = _Westage;
            Amount = _Amount;
            Tran_Type = _Tran_Type;
            Days = _Days;
            Company = _Company;
            UserId = _UserId;
        }

        public DataGridViewComboBoxColumn col_RateRevise_CreditPeriod = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Matltype_CreditPeriod = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Product_CreditPeriod = new DataGridViewComboBoxColumn();
        public DataGridViewColumn col_Westage_CreditPeriod = new DataGridViewTextBoxColumn();
        public DataGridViewColumn col_Amount_CreditPeriod = new DataGridViewTextBoxColumn();
        public DataGridViewColumn col_Days_CreditPeriod = new DataGridViewTextBoxColumn();
        public DataGridViewComboBoxColumn col_TranType_CreditPeriod = new DataGridViewComboBoxColumn();

        CalendarColumn dtpDateFrom = new CalendarColumn();
        CalendarColumn dtpDateTo = new CalendarColumn();

        public void BindCreditPeriod(DataGridView dgv, String _Category)
        {
            dgv.DataSource = null;
            dgv.Columns.Clear();
            dtpDateFrom.DataPropertyName = "DateFrom";
            dtpDateFrom.HeaderText = "DateFrom";
            dtpDateFrom.Name = "DateFrom";            
            dgv.Columns.Add(dtpDateFrom);

            dtpDateTo.DataPropertyName = "DateTo";
            dtpDateTo.HeaderText = "DateTo";
            dtpDateTo.Name = "DateTo";
            dgv.Columns.Add(dtpDateTo);

            col_RateRevise_CreditPeriod.DataPropertyName = "RateRevised";
            col_RateRevise_CreditPeriod.HeaderText = "Rate Revised";
            col_RateRevise_CreditPeriod.Name = "RateRevised";
            col_RateRevise_CreditPeriod.Items.Clear();
            col_RateRevise_CreditPeriod.Items.Add("AMOUNT");
            col_RateRevise_CreditPeriod.Items.Add("WESTAGE");
            col_RateRevise_CreditPeriod.Items.Add("BOTH");
            col_RateRevise_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_RateRevise_CreditPeriod);

            col_Matltype_CreditPeriod.DataPropertyName = "Category";
            col_Matltype_CreditPeriod.HeaderText = "Category";
            col_Matltype_CreditPeriod.Name = "Category";
            if (_Category == "" || _Category == "COMMON")
            {
                col_Matltype_CreditPeriod.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
            }
            else
            {
                col_Matltype_CreditPeriod.DataSource = CommanHelper.GetProduct().Where(r => r.Category == _Category).Select(x => x.Category).Distinct().ToList();
            }
            col_Matltype_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Matltype_CreditPeriod);

            col_Product_CreditPeriod.DataPropertyName = "Product";
            col_Product_CreditPeriod.HeaderText = "Product";
            col_Product_CreditPeriod.Name = "Product";
            col_Product_CreditPeriod.DataSource = CommanHelper.GetProduct().Select(x => x.ProductName).Distinct().ToList();
            col_Product_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Product_CreditPeriod);

            col_Westage_CreditPeriod.DataPropertyName = "Westage";
            col_Westage_CreditPeriod.HeaderText = "Westage";
            col_Westage_CreditPeriod.Name = "Westage";
            dgv.Columns.Add(col_Westage_CreditPeriod);

            col_Amount_CreditPeriod.DataPropertyName = "AmountWeight";
            col_Amount_CreditPeriod.HeaderText = "Amount";
            col_Amount_CreditPeriod.Name = "AmountWeight";
            dgv.Columns.Add(col_Amount_CreditPeriod);

            col_TranType_CreditPeriod.DataPropertyName = "Tran_Type";
            col_TranType_CreditPeriod.HeaderText = "TranType";
            col_TranType_CreditPeriod.Name = "Tran_Type";
            col_TranType_CreditPeriod.Items.Clear();
            col_TranType_CreditPeriod.Items.Add("JAMA");
            col_TranType_CreditPeriod.Items.Add("NAAM");
            col_TranType_CreditPeriod.Items.Add("BOTH");
            col_TranType_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_TranType_CreditPeriod);

            col_Days_CreditPeriod.DataPropertyName = "Days";
            col_Days_CreditPeriod.HeaderText = "Days";
            col_Days_CreditPeriod.Name = "Days";
            dgv.Columns.Add(col_Days_CreditPeriod);

            dgv.Rows[0].Cells[0].Value = CommanHelper.FDate;
            dgv.Rows[0].Cells[1].Value = CommanHelper.TDate;


        }
    }

    public static class CreditPeriodFactory
    {
        public static void Insert(String _PartyName, DateTime _DateFrom, DateTime _DateTo, String _RateRevised, String _Category, String _Product, Decimal _Westage, Decimal _Amount, String _Tran_Type, int _Days, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO CreditPeriod(PartyName,DateFrom,DateTo,RateRevised,Category,Product,Westage,Amount,Tran_Type,Days,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@RateRevised,@Category,@Product,@Westage,@Amount,@Tran_Type,@Days,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@DateFrom", _DateFrom);
            cmdInsert.Parameters.AddWithValue("@DateTo", _DateTo);
            cmdInsert.Parameters.AddWithValue("@RateRevised", _RateRevised);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Westage", _Westage);
            cmdInsert.Parameters.AddWithValue("@Amount", _Amount);
            cmdInsert.Parameters.AddWithValue("@Tran_Type", _Tran_Type);
            cmdInsert.Parameters.AddWithValue("@Days", _Days);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
