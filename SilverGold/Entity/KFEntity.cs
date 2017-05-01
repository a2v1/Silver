using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class KFEntity
    {
        public string BillNo { get; set; }
        public DateTime TrDate { get; set; }
        public string MetalCategory { get; set; }
        public string MetalName { get; set; }
        public string PaatNo { get; set; }
        public decimal Weight { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Fine { get; set; }
        public string TranType { get; set; }
        public string Narration { get; set; }
        public int Sno { get; set; }


    }

    public static class KFFactory
    {
        
        public static void Insert(String _MetalCategory, String _MetalName, String _PaatNo, Decimal _Weight, Decimal _Tunch1, Decimal _Tunch2, Decimal _Fine, String _TranType, String _Company, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO KfDetails(MetalCategory,MetalName,Weight,Tunch1,Tunch2,Fine,TranType,Company,UserId)VALUES(@MetalCategory,@MetalName,@Weight,@Tunch1,@Tunch2,@Fine,@TranType,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@MetalCategory", _MetalCategory);
            cmdInsert.Parameters.AddWithValue("@MetalName", _MetalName);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@Tunch1", _Tunch1);
            cmdInsert.Parameters.AddWithValue("@Tunch2", _Tunch2);
            cmdInsert.Parameters.AddWithValue("@Fine", _Fine);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }
    }
}
