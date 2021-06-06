using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Catalog
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}