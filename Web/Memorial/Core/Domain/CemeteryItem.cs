using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class CemeteryItem : Base
    {
        public CemeteryItem()
        {
            CemeteryTransactions = new HashSet<CemeteryTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public bool? isOrder { get; set; }

        public Plot Plot { get; set; }

        public int PlotId { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions { get; set; }
    }
}