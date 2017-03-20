﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverGold.Entity
{
    class CompanyLoginEntity
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string DataBaseName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string FinancialYear { get; set; }
        public string DataBasePath { get; set; }
    }
}
