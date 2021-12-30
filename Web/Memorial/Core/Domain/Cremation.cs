using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Cremation : Base
    {
        public Cremation()
        {
            CremationItems = new HashSet<CremationItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public int SiteId { get; set; }

        public ICollection<CremationItem> CremationItems { get; set; }
    }
}