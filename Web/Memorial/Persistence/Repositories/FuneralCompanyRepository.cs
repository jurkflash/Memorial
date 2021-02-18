using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class FuneralCompanyRepository : Repository<FuneralCompany>, IFuneralCompanyRepository
    {
        public FuneralCompanyRepository(MemorialContext context) : base(context)
        {
        }

        public FuneralCompany GetActive(int id)
        {
            return MemorialContext.FuneralCompanies
                .Where(fc => fc.DeleteDate == null &&
                        fc.Id == id).FirstOrDefault();
        }

        public IEnumerable<FuneralCompany> GetAllActive()
        {
            return MemorialContext.FuneralCompanies
                .Where(fc => fc.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}