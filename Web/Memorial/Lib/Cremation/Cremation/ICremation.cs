using System.Collections.Generic;

namespace Memorial.Lib.Cremation
{
    public interface ICremation
    {
        Core.Domain.Cremation GetById(int id);
        IEnumerable<Core.Domain.Cremation> GetBySite(int siteId);
        int Add(Core.Domain.Cremation cremation);
        bool Change(int id, Core.Domain.Cremation cremation);
        bool Remove(int id);
    }
}