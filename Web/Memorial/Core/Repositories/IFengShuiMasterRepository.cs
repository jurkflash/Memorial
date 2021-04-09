using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IFengShuiMasterRepository : IRepository<FengShuiMaster>
    {
        FengShuiMaster GetActive(int id);

        IEnumerable<FengShuiMaster> GetAllActive();
    }
}
