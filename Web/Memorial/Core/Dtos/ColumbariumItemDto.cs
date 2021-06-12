using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class ColumbariumItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public bool? isOrder { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

        public int ColumbariumCentreDtoId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}