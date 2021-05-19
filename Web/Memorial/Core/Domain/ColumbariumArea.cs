using System;
using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class ColumbariumArea
    {
        public ColumbariumArea()
        {
            Niches = new HashSet<Niche>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ColumbariumCentre ColumbariumCentre { get; set; }

        public int ColumbariumCentreId { get; set; }

        public ICollection<Niche> Niches { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}