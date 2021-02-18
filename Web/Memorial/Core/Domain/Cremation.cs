using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Cremation
    {
        public Cremation()
        {
            CremationItems = new HashSet<CremationItem>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public ICollection<CremationItem> CremationItems { get; set; }
    }
}