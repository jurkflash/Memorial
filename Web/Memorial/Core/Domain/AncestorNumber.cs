using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestorNumber
    {
        public int Id { get; set; }

        public AncestorItem AncestorItem { get; set; }

        public int AncestorItemId { get; set; }

        public int Year { get; set; }

        public int AF { get; set; }

        public int PO { get; set; }

        public int IV { get; set; }

        public int RE { get; set; }
    }
}