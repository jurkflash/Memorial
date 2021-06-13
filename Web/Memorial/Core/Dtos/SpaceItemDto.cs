using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class SpaceItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public Boolean? isOrder { get; set; }

        public bool AllowDeposit { get; set; }

        public bool AllowDoubleBook { get; set; }

        public byte ToleranceHour { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public SpaceDto SpaceDto { get; set; }

        public int SpaceDtoId { get; set; }

        public string FormView { get; set; }

        public DateTime CreateDate { get; set; }

    }
}