using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class MiscellaneousItemDto
    {
        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public Boolean? isOrder { get; set; }

        public MiscellaneousDto MiscellaneousDto { get; set; }

        public int MiscellaneousDtoId { get; set; }

        public SubProductServiceDto SubProductServiceDto { get; set; }

        public int SubProductServiceDtoId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}