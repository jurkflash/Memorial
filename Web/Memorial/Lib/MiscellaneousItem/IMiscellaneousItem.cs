using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IMiscellaneousItem
    {
        IEnumerable<MiscellaneousItemDto> DtosGetByMiscellaneous(int miscellaneousId);

        bool IsOrderFlag(int miscellaneousItemId);
    }
}