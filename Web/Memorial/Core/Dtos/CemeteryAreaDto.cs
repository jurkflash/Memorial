using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class CemeteryAreaDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte SiteId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}