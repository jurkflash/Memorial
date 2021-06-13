using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class CremationItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public CremationDto CremationDto { get; set; }

        public byte CremationDtoId { get; set; }

        public bool? isOrder { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}