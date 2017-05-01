using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

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

        public CreditPeriodEntity(String _PartyName, DateTime _DateFrom, DateTime _DateTo, string _RateRevised, string _Category, string _Product, Decimal _Westage, Decimal _Amount, string _Tran_Type, int _Days, string _Company, string _UserId)
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
