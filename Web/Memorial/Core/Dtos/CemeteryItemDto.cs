using System;

namespace Memorial.Core.Dtos
{
    public class CemeteryItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public bool? isOrder { get; set; }

        public PlotDto PlotDto { get; set; }

        public int PlotDtoId { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}