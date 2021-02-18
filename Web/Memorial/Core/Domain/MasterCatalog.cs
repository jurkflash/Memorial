using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public enum MasterCatalog:byte
    {
        Ancestor = 1,
        Cremation = 2,
        Miscellaneous = 3,
        Urn = 4,
        Space = 5,
        Quadrangle = 6,
        Plot = 7
    }
}