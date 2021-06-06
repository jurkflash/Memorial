using System;

namespace Memorial.Core.Dtos
{
    public class CatalogDto
    {
        public int Id { get; set; }

        public ProductDto ProductDto { get; set; }

        public int ProductDtoId { get; set; }

        public SiteDto SiteDto { get; set; }

        public byte SiteDtoId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}