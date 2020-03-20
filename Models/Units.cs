using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class Units
    {
        public Units()
        {
            FinishedProducts = new HashSet<FinishedProducts>();
            RawMaterials = new HashSet<RawMaterials>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FinishedProducts> FinishedProducts { get; set; }
        public virtual ICollection<RawMaterials> RawMaterials { get; set; }
    }
}
