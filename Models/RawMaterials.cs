using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class RawMaterials
    {
        public RawMaterials()
        {
            Ingredients = new HashSet<Ingredients>();
            PurchaseOfMaterials = new HashSet<PurchaseOfMaterials>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public byte Unit { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }

        public virtual Units UnitNavigation { get; set; }
        public virtual ICollection<Ingredients> Ingredients { get; set; }
        public virtual ICollection<PurchaseOfMaterials> PurchaseOfMaterials { get; set; }
    }
}
