using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class Positions
    {
        public Positions()
        {
            Stuff = new HashSet<Stuff>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stuff> Stuff { get; set; }
    }
}
