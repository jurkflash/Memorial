using System.Collections.Generic;

namespace Memorial.Lib.Site
{
    public interface ISite
    {
        Core.Domain.Site Get(int id);
        IEnumerable<Core.Domain.Site> GetAll();
        int Add(Core.Domain.Site site);
        bool Change(int id, Core.Domain.Site site);
        bool Remove(int id);
    }
}