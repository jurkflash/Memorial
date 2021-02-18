using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Miscellaneous
    {
        public Miscellaneous()
        {
            MiscellaneousItems = new HashSet<MiscellaneousItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public ICollection<MiscellaneousItem> MiscellaneousItems { get; set; }
    }
}