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
    class CreditLimitOpeningEntity
    {
        public string Name { get; set; }
        public decimal Limit { get; set; }
        public string JN { get; set; }
    }

    public static class CreditLimitFactory
    {
        public static void Insert(String _PartyName, String _ItemName, Decimal _ItemLimit, String _JN, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO CreditLimit(PartyName,ItemName,ItemLimit,JN,Company,UserId)VALUES(@PartyName,@ItemName,@ItemLimit,@JN,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@ItemName", _ItemName);
            cmdInsert.Parameters.AddWithValue("@ItemLimit", _ItemLimit);
            cmdInsert.Parameters.AddWithValue("@JN", _JN);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }

       
    }
}
