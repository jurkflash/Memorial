using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AccessControl : Base
    {
        public int Id { get; set; }

        public byte AncestralTablet { get; set; }

        public byte Cemetery { get; set; }

        public byte Columbarium { get; set; }

        public byte Cremation { get; set; }

        public byte Miscellaneous { get; set; }

        public byte Urn { get; set; }

        public byte Space { get; set; }

    }
}