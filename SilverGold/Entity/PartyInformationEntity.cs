using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class PartyInformationEntity
    {

    }

    public static class PartyInformationFactory
    {
        public static void Insert(String _Type, String _Category, String _PartyName, String _PartyType, String _Address, String _Email, String _ContactNo, String _GroupHead, String _SubGroup, String _IntroducerName, String _ShowInTrail, String _WithCreditPeriod, int _CreditPeriod, String _RateUpdate, String _Lot, String _LotGenerate, Decimal _BankCredit, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO PartyDetails(Type,Category,PartyName,PartyType,Address,Email,ContactNo,GroupHead,SubGroup,IntroducerName,ShowInTrail,WithCreditPeriod,CreditPeriod,RateUpdate,Lot,LotGenerate,BankCredit,Company,UserId)VALUES" +
                    "(@Type,@Category,@PartyName,@PartyType,@Address,@Email,@ContactNo,@GroupHead,@SubGroup,@IntroducerName,@ShowInTrail,@WithCreditPeriod,@CreditPeriod,@RateUpdate,@Lot,@LotGenerate,@BankCredit,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@Type", _Type);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@PartyType", _PartyType);
            cmdInsert.Parameters.AddWithValue("@Address", _Address);
            cmdInsert.Parameters.AddWithValue("@Email", _Email);
            cmdInsert.Parameters.AddWithValue("@ContactNo", _ContactNo);
            cmdInsert.Parameters.AddWithValue("@GroupHead", _GroupHead);
            cmdInsert.Parameters.AddWithValue("@SubGroup", _SubGroup);
            cmdInsert.Parameters.AddWithValue("@IntroducerName", _IntroducerName);
            cmdInsert.Parameters.AddWithValue("@ShowInTrail", _ShowInTrail);
            cmdInsert.Parameters.AddWithValue("@WithCreditPeriod", _WithCreditPeriod);
            cmdInsert.Parameters.AddWithValue("@CreditPeriod", _CreditPeriod);
            cmdInsert.Parameters.AddWithValue("@RateUpdate", _RateUpdate);
            cmdInsert.Parameters.AddWithValue("@Lot", _Lot);
            cmdInsert.Parameters.AddWithValue("@LotGenerate", _LotGenerate);
            cmdInsert.Parameters.AddWithValue("@BankCredit", _BankCredit);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }

    }
}
