using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IMiscellaneous
    {
        IEnumerable<MiscellaneousDto> DtosGetBySite(byte siteId);

    }
}