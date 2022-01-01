using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Base
    {
        public bool ActiveStatus { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedById { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int DeletedById { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}