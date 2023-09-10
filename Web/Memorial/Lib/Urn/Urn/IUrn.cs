using System.Collections.Generic;

namespace Memorial.Lib.Urn
{
    public interface IUrn
    {
        Core.Domain.Urn Get(int id);
        IEnumerable<Core.Domain.Urn> GetBySite(int siteId);
        int Add(Core.Domain.Urn urn);
        bool Change(int id, Core.Domain.Urn urn);
        bool Remove(int id);
    }
}