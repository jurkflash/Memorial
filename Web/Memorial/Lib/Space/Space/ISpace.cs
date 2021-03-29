using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public interface ISpace
    {
        IEnumerable<SpaceDto> DtosGetBySite(byte siteId);

        double GetAmount(DateTime from, DateTime to, int spaceItemId);

        bool CheckAvailability(DateTime from, DateTime to, int spaceItemId);

        bool CheckAvailability(DateTime from, DateTime to, string AF);

    }
}