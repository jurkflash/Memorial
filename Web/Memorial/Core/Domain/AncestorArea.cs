using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestorArea
    {
        public AncestorArea()
        {
            Ancestors = new HashSet<Ancestor>();

            AncestorItems = new HashSet<AncestorItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public string Remark { get; set; }

        public ICollection<Ancestor> Ancestors { get; set; }

        public ICollection<AncestorItem> AncestorItems { get; set; }
    }
}