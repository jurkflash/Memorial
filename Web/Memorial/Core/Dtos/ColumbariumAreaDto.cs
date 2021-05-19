using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class ColumbariumAreaDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

        public int ColumbariumCentreDtoId { get; set; }
    }
}