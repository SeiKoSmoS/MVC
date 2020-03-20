using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class Budget
    {
        public short Id { get; set; }
        public decimal AmountOfBudget { get; set; }
        public byte Percent { get; set; }
    }
}
