using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface IArea
    {
        Core.Domain.ColumbariumArea GetById(int areaId);
        IEnumerable<Core.Domain.ColumbariumArea> GetByCentre(int centreId);
        int Add(Core.Domain.ColumbariumArea columbariumArea);
        bool Change(int id, Core.Domain.ColumbariumArea columbariumArea);
        bool Remove(int id);
    }
}