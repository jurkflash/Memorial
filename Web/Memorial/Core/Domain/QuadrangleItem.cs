using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class QuadrangleItem
    {
        public QuadrangleItem()
        {
            QuadrangleNumbers = new HashSet<QuadrangleNumber>();

            QuadrangleTransactions = new HashSet<QuadrangleTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public QuadrangleCentre QuadrangleCentre { get; set; }

        public int QuadrangleCentreId { get; set; }

        public ICollection<QuadrangleNumber> QuadrangleNumbers { get; set; }

        public ICollection<QuadrangleTransaction> QuadrangleTransactions { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}