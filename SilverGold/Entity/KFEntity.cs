using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class KFEntity
    {
        public string BillNo { get; set; }
        public DateTime TrDate { get; set; }
        public string MetalCategory { get; set; }
        public string MetalName { get; set; }
        public string PaatNo { get; set; }
        public string Weight { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Fine { get; set; }
        public string TranType { get; set; }
        public string Narration { get; set; }
        public int Sno { get; set; }
    }
}
