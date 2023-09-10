using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface ICentre
    {
        Core.Domain.ColumbariumCentre GetById(int id);
        IEnumerable<Core.Domain.ColumbariumCentre> GetBySite(int sitId);
        int Add(Core.Domain.ColumbariumCentre columbariumCentre);
        bool Change(int id, Core.Domain.ColumbariumCentre columbariumCentre);
        bool Remove(int id);
    }
}