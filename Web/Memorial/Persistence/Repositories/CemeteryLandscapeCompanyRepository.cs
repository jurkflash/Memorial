using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CemeteryLandscapeCompanyRepository : Repository<CemeteryLandscapeCompany>, ICemeteryLandscapeCompanyRepository
    {
        public CemeteryLandscapeCompanyRepository(MemorialContext context) : base(context)
        {
        }

        public CemeteryLandscapeCompany GetActive(int id)
        {
            return MemorialContext.CemeteryLandscapeCompanies
                .Where(plc => plc.DeleteDate == null &&
                        plc.Id == id).FirstOrDefault();
        }

        public IEnumerable<CemeteryLandscapeCompany> GetAllActive()
        {
            return MemorialContext.CemeteryLandscapeCompanies
                .Where(plc => plc.DeleteDate == null).ToList();
        }
        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}