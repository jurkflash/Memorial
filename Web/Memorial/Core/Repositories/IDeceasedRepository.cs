using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IDeceasedRepository : IRepository<Deceased>
    {
        Deceased GetByIC(string IC);

        Deceased GetActive(int id);

        bool GetExistsByIC(string IC, int? excludeId = null);

        IEnumerable<Deceased> GetByApplicant(int id);

        IEnumerable<Deceased> GetAllExcludeFilter(int applicantId, string deceasedName);

        IEnumerable<Deceased> GetByNiche(int nicheId);

        IEnumerable<Deceased> GetByAncestralTablet(int ancestralTabletId);

        IEnumerable<Deceased> GetByPlot(int plotId);
    }
}
