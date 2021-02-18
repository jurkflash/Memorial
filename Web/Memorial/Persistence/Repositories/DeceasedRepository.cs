using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class DeceasedRepository : Repository<Deceased>, IDeceasedRepository
    {
        public DeceasedRepository(MemorialContext context) : base(context)
        {
        }

        public Deceased GetByIC(string IC)
        {
            return MemorialContext.Deceaseds
                .Where(d => d.IC == IC &&
                    d.DeleteDate == null).FirstOrDefault();
        }

        public Deceased GetActive(int id)
        {
            return MemorialContext.Deceaseds
                .Include(d => d.GenderType)
                .Include(d => d.MaritalType)
                .Include(d => d.NationalityType)
                .Include(d => d.RelationshipType)
                .Include(d => d.ReligionType)
                .Where(d => d.DeleteDate == null &&
                        d.Id == id).SingleOrDefault();
        }

        public IEnumerable<Deceased> GetByApplicant(int id)
        {
            return MemorialContext.Deceaseds
                    .Include(d => d.RelationshipType)
                    .Where(d => d.ApplicantId == id &&
                    d.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}