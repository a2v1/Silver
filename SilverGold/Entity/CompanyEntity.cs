using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class CompanyEntity
    {
        public string CompanyName { get; set; }
        public string DataBaseName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string FinancialYear { get; set; }
        public string DataBasePath { get; set; }

        public void AddCompany(String _CompanyName, String _DataBaseName, String _DateFrom, String _DateTo, String _FinancialYear, String _DataBasePath)
        {
            CompanyName = _CompanyName;
            DataBaseName = _DataBaseName;
            DateFrom = _DateFrom;
            DateTo = _DateTo;
            FinancialYear = _FinancialYear;
            DataBasePath = _DataBasePath;
        }
    }


    class CompanyDetailsEntity
    {
        public string DisplayName { get; set; }
        public string CompanyName { get; set; }
        public string DataBaseName { get; set; }
        public string FinancialYear { get; set; }
        public string DataBasePath { get; set; }
        public int Sno { get; set; }


        public void AddCompanyDetails(String _DisplayName, String _CompanyName, String _DataBaseName, String _FinancialYear, String _DataBasePath, int _Sno)
        {
            DisplayName = _DisplayName;
            CompanyName = _CompanyName;
            DataBaseName = _DataBaseName;
            FinancialYear = _FinancialYear;
            DataBasePath = _DataBasePath;
            Sno = _Sno;
        }
    }

    public static class CompanyFactory
    {
        public static void Insert(String _CompanyName, String _DateFrom, String _DateTo, String _FinancialYear, String _DatabasePath, String _DataBaseName, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO Company(CompanyName,DateFrom,DateTo,FinancialYear,DatabasePath,CompanyName,DataBaseName)VALUES(@CompanyName,@DateFrom,@DateTo,@FinancialYear,@DatabasePath,@CompanyName,@DataBaseName)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@CompanyName", _CompanyName);
            cmdInsert.Parameters.AddWithValue("@DateFrom", _DateFrom);
            cmdInsert.Parameters.AddWithValue("@DateTo", _DateTo);
            cmdInsert.Parameters.AddWithValue("@FinancialYear", _FinancialYear);
            cmdInsert.Parameters.AddWithValue("@DatabasePath", _DatabasePath);
            cmdInsert.Parameters.AddWithValue("@CompanyName", _CompanyName);
            cmdInsert.Parameters.AddWithValue("@DataBaseName", _DataBaseName); 
            cmdInsert.ExecuteNonQuery();
        }
    }

}

