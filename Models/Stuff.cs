using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class Stuff
    {
        public Stuff()
        {
            Payroll = new HashSet<Payroll>();
            ProductSales = new HashSet<ProductSales>();
            Production = new HashSet<Production>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public byte Position { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Positions PositionNavigation { get; set; }
        public virtual ICollection<Payroll> Payroll { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
        public virtual ICollection<Production> Production { get; set; }
    }
}
