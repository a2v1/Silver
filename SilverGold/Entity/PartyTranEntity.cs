using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class PartyTranEntity
    {
    }

    public static class PartyTranFactory
    {
        public static void InsertPartyInformation(String _TrDate, String _Category, String _PartyName, String _MetalCategory, String _MetalName, Decimal _Debit, Decimal _Credit, Decimal _Weight, Decimal _MCXRate, String _TranType, String _ContCode, String _Narration, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO PartyTran(TrDate,Category,PartyName,MetalCategory,MetalName,Debit,Credit,Weight,MCXRate,TranType,ContCode,Narration,Company,UserId)VALUES(@TrDate,@Category,@PartyName,@MetalCategory,@MetalName,@Debit,@Credit,@Weight,@MCXRate,@TranType,@ContCode,@Narration,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@TrDate", _TrDate);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@MetalCategory", _MetalCategory);
            cmdInsert.Parameters.AddWithValue("@MetalName", _MetalName);
            cmdInsert.Parameters.AddWithValue("@Debit", _Debit);
            cmdInsert.Parameters.AddWithValue("@Credit", _Credit);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@MCXRate", _MCXRate);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@ContCode", _ContCode);
            cmdInsert.Parameters.AddWithValue("@Narration", _Narration);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
