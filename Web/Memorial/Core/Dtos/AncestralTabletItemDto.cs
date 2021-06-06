using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class AncestralTabletItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public bool? isOrder { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public AncestralTabletAreaDto AncestralTabletAreaDto { get; set; }

        public int AncestralTabletAreaDtoId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}