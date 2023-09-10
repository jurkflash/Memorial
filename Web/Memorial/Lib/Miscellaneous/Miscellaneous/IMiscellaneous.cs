using System.Collections.Generic;

namespace Memorial.Lib.Miscellaneous
{
    public interface IMiscellaneous
    {
        Core.Domain.Miscellaneous Get(int id);
        IEnumerable<Core.Domain.Miscellaneous> GetBySite(int siteId);
        int Add(Core.Domain.Miscellaneous miscellaneous);
        bool Change(int spaceId, Core.Domain.Miscellaneous miscellaneous);
        bool Remove(int id);
    }
}