using SilverGold.Comman;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class PartyOpeningEntity
    {
    }

    public static class PartyOpeningFactory
    {
        public static void Insert(String _PartyName, String _ItemName, Decimal _Weight, Decimal _ClosingRate, String _DrCr, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO PartyOpening(PartyName,ItemName,Weight,ClosingRate,DrCr,Company,UserId)VALUES(@PartyName,@ItemName,@Weight,@ClosingRate,@DrCr,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@ItemName", _ItemName);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@ClosingRate", _ClosingRate);
            cmdInsert.Parameters.AddWithValue("@DrCr", _DrCr);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
