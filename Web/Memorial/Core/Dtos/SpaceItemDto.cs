using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class SpaceItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public Boolean isOrder { get; set; }

        public bool AllowDeposit { get; set; }

        public bool AllowDoubleBook { get; set; }

        public byte ToleranceHour { get; set; }

        public SpaceDto SpaceDto { get; set; }

        public int SpaceDtoId { get; set; }

        public string FormView { get; set; }

        public DateTime CreateDate { get; set; }

    }
}