using System;
using System.Collections.Generic;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class MiscellaneousItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public Boolean isOrder { get; set; }

        public Miscellaneous Miscellaneous { get; set; }

        public int MiscellaneousId { get; set; }

        public string SystemCode { get; set; }

        public DateTime CreateDate { get; set; }
    }
}