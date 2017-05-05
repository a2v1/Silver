using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class OpeningMCXEntity
    {
        public DateTime OpeningDate { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Closing { get; set; }
        public string DrCr { get; set; }
        public string Narration { get; set; }
    }

    class OpeningOtherEntity
    {
        public DateTime OpeningDate { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string DrCr { get; set; }
        public string Narration { get; set; }
        public string Category { get; set; }
    }

    public static class CompnayOpeningFactory
    {
        public static void Insert(String _MetalName,Decimal _Weight,String _DrCr,String _CompanyName,String _UserId,OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO CompanyOpening(MetalName,Weight,DrCr,CompanyName,UserId)VALUES(@MetalName,@Weight,@DrCr,@CompanyName,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@MetalName", _MetalName);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@DrCr", _DrCr);
            cmdInsert.Parameters.AddWithValue("@CompanyName", _CompanyName);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }
    }

    
}
