using System.Collections.Generic;

namespace Memorial.Core.Dtos
{
    public class SubProductServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public ProductDto ProductDto { get; set; }

        public int ProductDtoId { get; set; }

    }
}