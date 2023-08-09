using System.Collections.Generic;

namespace Memorial.Lib.CemeteryLandscapeCompany
{
    public interface ICemeteryLandscapeCompany
    {
        Core.Domain.CemeteryLandscapeCompany Get(int id);
        IEnumerable<Core.Domain.CemeteryLandscapeCompany> GetAll();
        int Add(Core.Domain.CemeteryLandscapeCompany CemeteryLandscapeCompany);
        bool Change(int id, Core.Domain.CemeteryLandscapeCompany CemeteryLandscapeCompany);
        bool Remove(int id);
    }
}