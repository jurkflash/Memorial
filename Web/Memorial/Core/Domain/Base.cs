using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Base
    {
        public bool ActiveStatus { get; set; }

        public string CreatedById { get; set; }

        public DateTime CreatedUtcTime { get; set; }

        public string ModifiedById { get; set; }

        public DateTime? ModifiedUtcTime { get; set; }

        public string DeletedById { get; set; }

        public DateTime? DeletedUtcTime { get; set; }
    }
}