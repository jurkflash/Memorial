using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class QuadrangleType
    {
        public QuadrangleType()
        {
            Quadrangles = new HashSet<Quadrangle>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfPlacement { get; set; }

        public ICollection<Quadrangle> Quadrangles { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

    }
}