using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class Ingredients
    {
        public short Id { get; set; }
        public byte Product { get; set; }
        public byte Material { get; set; }
        public double Quantity { get; set; }

        public virtual RawMaterials MaterialNavigation { get; set; }
        public virtual FinishedProducts ProductNavigation { get; set; }
    }
}
