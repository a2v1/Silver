using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class JamaNaamEntity
    {
        public string PGroup { get; set; }
        public string Product { get; set; }
        public decimal Weight { get; set; }
        public decimal Pcs { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Westage { get; set; }
        public decimal LaboursRate { get; set; }
        public decimal Fine { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public int Sno { get; set; }


        public void InsertJamaNaam(String _BillNo, DateTime _TrDate, String _MetalCategory, String _PartyName, String _PGroup, string _Product, Decimal _Weight, Decimal _Pcs, Decimal _Tunch1, Decimal _Tunch2, Decimal _Westage, Decimal _LaboursRate, Decimal _Debit, Decimal _Credit, Decimal _Amount, string _Narration, String _TranType, String _EntryFrom, String _Company, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();

            strInsert = "INSERT into PartyTran( BillNo,TrDate,MetalCategory,PartyName,PGroup, Product, Weight, Pcs, Tunch1, Tunch2, Westage, LaboursRate,Debit, Credit, Amount, Narration,TranType,EntryFrom,Company,UserId ) Values ( @BillNo,@TrDate,@MetalCategory,@PartyName,@PGroup, @Product, @Weight, @Pcs, @Tunch1, @Tunch2, @Westage, @LaboursRate,@Debit, @Credit, @Amount, @Narration,@TranType,@EntryFrom,@Company,@UserId )";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }
            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@BillNo", _BillNo);
            cmdInsert.Parameters.AddWithValue("@TrDate", _TrDate);
            cmdInsert.Parameters.AddWithValue("@MetalCategory", _MetalCategory);
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@PGroup", _PGroup);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@Pcs", _Pcs);
            cmdInsert.Parameters.AddWithValue("@Tunch1", _Tunch1);
            cmdInsert.Parameters.AddWithValue("@Tunch2", _Tunch2);
            cmdInsert.Parameters.AddWithValue("@Westage", _Westage);
            cmdInsert.Parameters.AddWithValue("@LaboursRate", _LaboursRate);
            cmdInsert.Parameters.AddWithValue("@Debit", _Debit);
            cmdInsert.Parameters.AddWithValue("@Credit", _Credit);
            cmdInsert.Parameters.AddWithValue("@Amount", _Amount);
            cmdInsert.Parameters.AddWithValue("@Narration", _Narration);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@EntryFrom", _EntryFrom);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }

        public void GetBillNo_ListBox(ListBox _ListBox, DateTime _Date,String _TranType, OleDbConnection _Con)
        {
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Billno,PartyName from PartyTran where TrDate=#" + _Date + "# And TranType='" + _TranType + "' Group by billno,PartyName", _Con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _ListBox.Items.Add(dr[0].ToString() + "..." + dr[1].ToString());
            }
            dr.Close();
            _Con.Close();
        }
    }
}
