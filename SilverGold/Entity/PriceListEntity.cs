using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class PriceListEntity
    {
        public DateTime TrDate { get; set; }
        public string PartyCat { get; set; }
        public string PartyName { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public decimal Westage { get; set; }
        public decimal LabourRs { get; set; }
        public string TranType { get; set; }
        public string Company { get; set; }
        public string UserId { get; set; }
        public int Sno { get; set; }

        public void InsertPriceList(DateTime _TrDate, String _PartyCat, String _PartyName, String _Category, String _Product, Decimal _Westage, Decimal _LabourRs, String _TranType, String _Company, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }
            strInsert = "INSERT INTO PriceList(TrDate, PartyCat,PartyName,Category,Product,Westage,LabourRs,TranType,Company,UserId )VALUES(@TrDate, @PartyCat,@PartyName,@Category,@Product,@Westage,@LabourRs,@TranType,@Company,@UserId)";
            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@TrDate", _TrDate);
            cmdInsert.Parameters.AddWithValue("@PartyCat", _PartyCat);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Westage", _Westage);
            cmdInsert.Parameters.AddWithValue("@LabourRs", _LabourRs);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }
    }
}
