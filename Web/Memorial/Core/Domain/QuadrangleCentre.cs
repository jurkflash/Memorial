using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class QuadrangleCentre
    {
        public QuadrangleCentre()
        {
            QuadrangleAreas = new HashSet<QuadrangleArea>();

            QuadrangleItems = new HashSet<QuadrangleItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public ICollection<QuadrangleArea> QuadrangleAreas { get; set; }

        public ICollection<QuadrangleItem> QuadrangleItems { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}