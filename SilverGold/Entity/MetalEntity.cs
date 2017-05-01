using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class MetalEntity
    {
        public string MetalCategory { get; set; }
        public string MetalName { get; set; }
        public string WeightType { get; set; }
        public string KachchiFine { get; set; }
        public decimal AmountWeight { get; set; }
        public string DrCr { get; set; }
        public int Sno { get; set; }
        public string CompanyName { get; set; }
        public string UserId { get; set; }

        
    }

    public static class MetalFactory
    {
        public static void InsertMetal(String _MetalCategory, String _MetalName, String _WeightType, String _KachchiFine, String _CompanyName, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO Metal(MetalCategory,MetalName,WeightType,KachchiFine,CompanyName,UserId)VALUES(@MetalCategory,@MetalName,@WeightType,@KachchiFine,@CompanyName,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@MetalCategory", _MetalCategory);
            cmdInsert.Parameters.AddWithValue("@MetalName", _MetalName);
            cmdInsert.Parameters.AddWithValue("@WeightType", _WeightType);
            cmdInsert.Parameters.AddWithValue("@KachchiFine", _KachchiFine);
            cmdInsert.Parameters.AddWithValue("@CompanyName", _CompanyName);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }
    }

}
