using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface IArea
    {
        int Add(Core.Domain.CemeteryArea cemeteryArea);
        bool Remove(int id);
        CemeteryArea GetById(int areaId);
        IEnumerable<CemeteryArea> GetBySite(int siteId);
        IEnumerable<Core.Domain.CemeteryArea> GetAll();
        bool Change(int id, Core.Domain.CemeteryArea cemeteryArea);
    }
}