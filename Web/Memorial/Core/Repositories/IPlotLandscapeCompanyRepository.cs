using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IPlotLandscapeCompanyRepository : IRepository<PlotLandscapeCompany>
    {
        PlotLandscapeCompany GetActive(int id);

        IEnumerable<PlotLandscapeCompany> GetAllActive();
    }
}
