using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class MiscellaneousItem
    {
        public MiscellaneousItem()
        {
            MiscellaneousTransactions = new HashSet<MiscellaneousTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public Boolean? isOrder { get; set; }

        public Miscellaneous Miscellaneous { get; set; }

        public int MiscellaneousId { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }
    }
}