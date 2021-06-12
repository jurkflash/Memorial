using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class MiscellaneousItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public Boolean? isOrder { get; set; }

        public MiscellaneousDto MiscellaneousDto { get; set; }

        public int MiscellaneousDtoId { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}