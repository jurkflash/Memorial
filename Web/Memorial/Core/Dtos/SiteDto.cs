using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class SiteDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }
    }
}