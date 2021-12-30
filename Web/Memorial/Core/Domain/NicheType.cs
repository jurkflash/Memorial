using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class NicheType : Base
    {
        public NicheType()
        {
            Niches = new HashSet<Niche>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfPlacement { get; set; }

        public ICollection<Niche> Niches { get; set; }
    }
}