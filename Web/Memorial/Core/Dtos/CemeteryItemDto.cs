using System;

namespace Memorial.Core.Dtos
{
    public class CemeteryItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public int PlotTypeId { get; set; }

        public int CemeteryAreaId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}