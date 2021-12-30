using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class SpaceItem : Base
    {
        public SpaceItem()
        {
            SpaceTransactions = new HashSet<SpaceTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public Boolean? isOrder { get; set; }

        public bool AllowDeposit { get; set; }

        public bool AllowDoubleBook { get; set; }

        public byte ToleranceHour { get; set; }

        public Space Space { get; set; }

        public int SpaceId { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public string FormView { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }
    }
}