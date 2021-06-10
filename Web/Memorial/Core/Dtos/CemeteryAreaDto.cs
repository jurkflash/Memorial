using System;

namespace Memorial.Core.Dtos
{
    public class CemeteryAreaDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SiteDto SiteDto { get; set; }

        public int SiteDtoId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}