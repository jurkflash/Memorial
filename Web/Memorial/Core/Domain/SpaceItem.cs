using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class SpaceItem
    {
        public SpaceItem()
        {
            SpaceNumbers = new HashSet<SpaceNumber>();

            SpaceTransactions = new HashSet<SpaceTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public Boolean isOrder { get; set; }

        public bool AllowDeposit { get; set; }

        public bool AllowDoubleBook { get; set; }

        public byte ToleranceHour { get; set; }

        public Space Space { get; set; }

        public int SpaceId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<SpaceNumber> SpaceNumbers { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }
    }
}