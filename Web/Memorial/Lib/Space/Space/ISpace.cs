using System;
using System.Collections.Generic;

namespace Memorial.Lib.Space
{
    public interface ISpace
    {
        Core.Domain.Space Get(int id);
        IEnumerable<Core.Domain.Space> GetBySite(int siteId);
        int Add(Core.Domain.Space space);
        bool Change(int spaceId, Core.Domain.Space space);
        bool Remove(int id);
        bool CheckAvailability(DateTime from, DateTime to, int spaceItemId);
        bool CheckAvailability(DateTime from, DateTime to, string AF);
        double GetAmount(DateTime from, DateTime to, int spaceItemId);
    }
}