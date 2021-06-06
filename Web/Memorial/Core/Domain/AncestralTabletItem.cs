using System;
using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class AncestralTabletItem
    {
        public AncestralTabletItem()
        {
            AncestralTabletTransactions = new HashSet<AncestralTabletTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public bool? isOrder { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public AncestralTabletArea AncestralTabletArea { get; set; }

        public int AncestralTabletAreaId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<AncestralTabletTransaction> AncestralTabletTransactions { get; set; }
    }
}