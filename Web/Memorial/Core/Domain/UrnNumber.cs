using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class UrnNumber
    {
        public int Id { get; set; }

        public UrnItem UrnItem { get; set; }

        public int UrnItemId { get; set; }

        public int Year { get; set; }

        public int AF { get; set; }

        public int PO { get; set; }

        public int IV { get; set; }

        public int RE { get; set; }
    }
}