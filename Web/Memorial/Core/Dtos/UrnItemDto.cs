using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class UrnItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        [StringLength(5)]
        public string Code { get; set; }

        public UrnDto UrnDto { get; set; }

        public int UrnDtoId { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public bool? isOrder { get; set; }

        public DateTime CreateDate { get; set; }

    }
}