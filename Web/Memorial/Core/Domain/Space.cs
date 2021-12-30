using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Space : Base
    {
        public Space()
        {
            SpaceItems = new HashSet<SpaceItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public Site Site { get; set; }

        public int SiteId { get; set; }

        public string ColorCode { get; set; }

        public ICollection<SpaceItem> SpaceItems { get; set; }
    }
}