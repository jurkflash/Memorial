using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IDeceasedRepository : IRepository<Deceased>
    {
        Deceased GetByIC(string IC);

        Deceased GetActive(int id);

        IEnumerable<Deceased> GetByApplicant(int id);

        IEnumerable<Deceased> GetAllExcludeFilter(int applicantId, string deceasedName);

        IEnumerable<Deceased> GetByQuadrangle(int quadrangleId);

        IEnumerable<Deceased> GetByAncestor(int ancestorId);

        IEnumerable<Deceased> GetByPlot(int plotId);
    }
}
