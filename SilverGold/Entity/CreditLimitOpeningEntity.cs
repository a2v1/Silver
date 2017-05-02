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
    }

    public static class CreditLimitFactory
    {
        public static void Insert(String _PartyName, String _CreditPeriod, String _RateUpdate, String _ItemName, Decimal _ItemLimit, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO CreditLimit(PartyName,CreditPeriod,RateUpdate,ItemName,ItemLimit,Company,UserId)VALUES(@PartyName,@CreditPeriod,@RateUpdate,@ItemName,@ItemLimit,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@CreditPeriod", _CreditPeriod);
            cmdInsert.Parameters.AddWithValue("@RateUpdate", _RateUpdate);
            cmdInsert.Parameters.AddWithValue("@ItemName", _ItemName);
            cmdInsert.Parameters.AddWithValue("@ItemLimit", _ItemLimit);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }

        public static DataGridViewColumn colLimit = new DataGridViewTextBoxColumn();
        public static void BindCreditLimitOpeningColumn(DataGridView dgv)
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dgv.Columns.Add(col1);


            colLimit.DataPropertyName = "Limit";
            colLimit.HeaderText = "Limit";
            colLimit.Name = "Limit";
            dgv.Columns.Add(colLimit);
        }
    }
}
