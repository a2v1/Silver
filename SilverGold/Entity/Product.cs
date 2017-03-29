using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class Product
    {
        public string Category { get; set; }
        public string Unit { get; set; }
        public Decimal Weight_Packet { get; set; }
        public string ProductName { get; set; }
        public string SubGroup { get; set; }
        public string PGroup { get; set; }
        public Decimal Opening { get; set; }
        public Decimal Pcs { get; set; }
        public Decimal Tunch { get; set; }
        public Decimal Westage { get; set; }
        public Decimal LabourRate { get; set; }
        public Decimal Fine { get; set; }
        public Decimal Amount { get; set; }
        public string RawDefine { get; set; }
        public DateTime OpenDate { get; set; }
        public string Narration { get; set; }
        public string Company { get; set; }
        public string UserId { get; set; }

        public Product(string _Category, string _Unit, Decimal _Weight_Packet, string _ProductName, string _SubGroup, string _PGroup, Decimal _Opening, Decimal _Pcs, Decimal _Tunch, Decimal _Westage,Decimal _LabourRate,  Decimal _Fine , Decimal _Amount , string _RawDefine , DateTime _OpenDate  ,string _Narration  ,string _Company , string _UserId )
        {
            Category = _Category;
            Unit = _Unit;
            Weight_Packet = _Weight_Packet;
            ProductName = _ProductName;
            PGroup = _PGroup;
            Opening = _Opening;
            Pcs = _Pcs;
            Tunch = _Tunch;
            Westage = _Westage;
            LabourRate = _LabourRate;
            Fine = _Fine;
            Amount = _Amount;
            RawDefine = _RawDefine;
            OpenDate = _OpenDate;
            Narration = _Narration;
            Company = _Company;
            UserId = _UserId;
        }

    }
}
