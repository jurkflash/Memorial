using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class UrnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public float Price { get; set; }

        public Site Site { get; set; }

        public byte SiteId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}