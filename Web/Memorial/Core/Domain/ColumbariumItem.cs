using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class ColumbariumItem : Base
    {
        public ColumbariumItem()
        {
            ColumbariumTransactions = new HashSet<ColumbariumTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public bool? isOrder { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public ColumbariumCentre ColumbariumCentre { get; set; }

        public int ColumbariumCentreId { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions { get; set; }
    }
}