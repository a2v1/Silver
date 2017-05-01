using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class CommissionListEntity
    {
    }

    public static class CommissionListFactory
    {
        public static void Insert(String _PartyName,DateTime _DateFrom,DateTime _DateTo,String _WeightPcs,String _Category,String _Product,String _Fine_Amount,Decimal _BrokerageRate,String _PayType,String _JamaNaam,OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO CommissionList(PartyName,DateFrom,DateTo,WeightPcs,Category,Product,Fine_Amount,BrokerageRate,PayType,JamaNaam,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@WeightPcs,@Category,@Product,@Fine_Amount,@BrokerageRate,@PayType,@JamaNaam,@Company,@UserId)";
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
            cmdInsert.Parameters.AddWithValue("@WeightPcs", _WeightPcs);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Fine_Amount", _Fine_Amount);
            cmdInsert.Parameters.AddWithValue("@BrokerageRate", _BrokerageRate);
            cmdInsert.Parameters.AddWithValue("@PayType", _PartyName);
            cmdInsert.Parameters.AddWithValue("@JamaNaam", _JamaNaam);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
