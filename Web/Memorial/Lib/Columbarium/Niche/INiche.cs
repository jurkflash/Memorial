using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface INiche
    {
        IEnumerable<Core.Domain.Niche> GetAvailableNichesByAreaId(int id);
        IEnumerable<Core.Domain.Niche> GetByAreaIdAndTypeId(int areaId, int typeId, string filter);
        Core.Domain.Niche GetById(int id);
        Core.Domain.Niche GetByAreaIdAndPostions(int areaId, int positionX, int positionY);
        IEnumerable<Core.Domain.Niche> GetByAreaId(int id);
        IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId);
        int Add(Core.Domain.Niche niche);
        bool Change(int id, Core.Domain.Niche niche);
        bool Remove(int id);
    }
}