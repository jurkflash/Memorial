using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class PlotLandscapeCompanyRepository : Repository<PlotLandscapeCompany>, IPlotLandscapeCompanyRepository
    {
        public PlotLandscapeCompanyRepository(MemorialContext context) : base(context)
        {
        }

        public PlotLandscapeCompany GetActive(int id)
        {
            return MemorialContext.PlotLandscapeCompanies
                .Where(plc => plc.DeleteDate == null &&
                        plc.Id == id).FirstOrDefault();
        }

        public IEnumerable<PlotLandscapeCompany> GetAllActive()
        {
            return MemorialContext.PlotLandscapeCompanies
                .Where(plc => plc.DeleteDate == null).ToList();
        }
        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}