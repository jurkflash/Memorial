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
            MiscellaneousNumbers = new HashSet<MiscellaneousNumber>();

            MiscellaneousTransactions = new HashSet<MiscellaneousTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public Boolean isOrder { get; set; }

        public Miscellaneous Miscellaneous { get; set; }

        public int MiscellaneousId { get; set; }

        public string SystemCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<MiscellaneousNumber> MiscellaneousNumbers { get; set; }

        public ICollection<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }
    }
}