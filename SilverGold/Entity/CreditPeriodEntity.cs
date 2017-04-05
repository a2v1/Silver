using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class CreditPeriodEntity
    {
        public string PartyName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string RateRevised { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public Decimal Westage { get; set; }
        public Decimal Amount { get; set; }
        public string Tran_Type { get; set; }
        public int Days { get; set; }
        public string Company { get; set; }
        public string UserId { get; set; }

        public CreditPeriodEntity(String _PartyName, DateTime _DateFrom, DateTime _DateTo, string _RateRevised, string _Category, string _Product, Decimal _Westage, Decimal _Amount, string _Tran_Type, int _Days, string _Company, string _UserId)  
        {
            PartyName = _PartyName;
            DateFrom = _DateFrom;
            DateTo = _DateTo;
            RateRevised = _RateRevised;
            Category = _Category;
            Product = _Product;
            Westage = _Westage;
            Amount = _Amount;
            Tran_Type = _Tran_Type;
            Days = _Days;
            Company = _Company;
            UserId = _UserId;
        }  
    }
}
