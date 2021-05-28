using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ICemeteryLandscapeCompanyRepository : IRepository<CemeteryLandscapeCompany>
    {
        CemeteryLandscapeCompany GetActive(int id);

        IEnumerable<CemeteryLandscapeCompany> GetAllActive();
    }
}
