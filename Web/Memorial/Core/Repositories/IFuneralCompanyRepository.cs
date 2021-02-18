using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IFuneralCompanyRepository : IRepository<FuneralCompany>
    {
        FuneralCompany GetActive(int id);

        IEnumerable<FuneralCompany> GetAllActive();
    }
}
