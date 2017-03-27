using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class OpeningMCXEntity
    {
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Closing { get; set; }
        public string DrCr { get; set; }
    }

    class OpeningOtherEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string DrCr { get; set; }
    }

    
}
