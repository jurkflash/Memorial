using System;
using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class AncestralTabletArea
    {
        public AncestralTabletArea()
        {
            AncestralTablets = new HashSet<AncestralTablet>();

            AncestralTabletItems = new HashSet<AncestralTabletItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public int SiteId { get; set; }

        public string Remark { get; set; }

        public ICollection<AncestralTablet> AncestralTablets { get; set; }

        public ICollection<AncestralTabletItem> AncestralTabletItems { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}