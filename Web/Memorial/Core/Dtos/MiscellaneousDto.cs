using Memorial.Core.Domain;
using System;

namespace Memorial.Core.Dtos
{
    public class MiscellaneousDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public SiteDto SiteDto { get; set; }

        public byte SiteDtoId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}