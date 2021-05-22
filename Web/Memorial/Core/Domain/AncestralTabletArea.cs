using System;
using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class AncestralTabletArea
    {
        public AncestralTabletArea()
        {
            Ancestors = new HashSet<Ancestor>();

            AncestralTabletItems = new HashSet<AncestralTabletItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public string Remark { get; set; }

        public ICollection<Ancestor> Ancestors { get; set; }

        public ICollection<AncestralTabletItem> AncestralTabletItems { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}