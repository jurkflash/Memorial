using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class ColumbariumCentre : Base
    {
        public ColumbariumCentre()
        {
            ColumbariumAreas = new HashSet<ColumbariumArea>();

            ColumbariumItems = new HashSet<ColumbariumItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public int SiteId { get; set; }

        public ICollection<ColumbariumArea> ColumbariumAreas { get; set; }

        public ICollection<ColumbariumItem> ColumbariumItems { get; set; }
    }
}