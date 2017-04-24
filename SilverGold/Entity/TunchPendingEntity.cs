using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

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

    }
}
