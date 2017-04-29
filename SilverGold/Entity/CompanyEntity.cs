using System;
using System.Collections.Generic;
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
        public int Sno{ get; set; }


        public void AddCompanyDetails(String _DisplayName,String _CompanyName, String _DataBaseName, String _FinancialYear, String _DataBasePath, int _Sno)
        {
            DisplayName = _DisplayName;
            CompanyName = _CompanyName;
            DataBaseName = _DataBaseName;
            FinancialYear = _FinancialYear;
            DataBasePath = _DataBasePath;
            Sno = _Sno;
        }
    }
         

}

