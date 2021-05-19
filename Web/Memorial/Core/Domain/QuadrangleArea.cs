using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class QuadrangleArea
    {
        public QuadrangleArea()
        {
            Quadrangles = new HashSet<Quadrangle>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ColumbariumCentre ColumbariumCentre { get; set; }

        public int ColumbariumCentreId { get; set; }

        public ICollection<Quadrangle> Quadrangles { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}