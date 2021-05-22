using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestorItem
    {
        public AncestorItem()
        {
            AncestralTabletTransactions = new HashSet<AncestralTabletTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public AncestorArea AncestorArea { get; set; }

        public int AncestorAreaId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<AncestralTabletTransaction> AncestralTabletTransactions { get; set; }
    }
}