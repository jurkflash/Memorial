using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class MiscellaneousNumber
    {
        public int Id { get; set; }

        public MiscellaneousItem MiscellaneousItem { get; set; }

        public int MiscellaneousItemId { get; set; }

        public int Year { get; set; }

        public int AF { get; set; }

        public int PO { get; set; }

        public int IV { get; set; }

        public int RE { get; set; }
    }
}