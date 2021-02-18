using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface ISpaceItem
    {
        IEnumerable<SpaceItemDto> DtosGetBySpace(int spaceId);

        bool IsOrderFlag(int spaceItemId);

        bool AllowDoubleBook(int spaceItemId);
    }
}