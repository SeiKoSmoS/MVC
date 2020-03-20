using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class FinishedProducts
    {
        public FinishedProducts()
        {
            Ingredients = new HashSet<Ingredients>();
            ProductSales = new HashSet<ProductSales>();
            Production = new HashSet<Production>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public byte Unit { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }

        public virtual Units UnitNavigation { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
        public virtual ICollection<Production> Production { get; set; }
    }
}
