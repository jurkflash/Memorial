using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Urn
    {
        public Urn()
        {
            UrnItems = new HashSet<UrnItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public float Price { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<UrnItem> UrnItems { get; set; }
    }
}