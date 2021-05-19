using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class ColumbariumItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public QuadrangleCentre QuadrangleCentre { get; set; }

        public int QuadrangleCentreId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}