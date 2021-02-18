using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(MemorialContext context) : base(context)
        {
        }

        public Applicant GetByIC(string IC)
        {
            return MemorialContext.Applicants
                .Where(a => a.IC == IC &&
                    a.DeleteDate == null).FirstOrDefault();
        }

        public Applicant GetActive(int id)
        {
            return MemorialContext.Applicants
                .Where(a => a.DeleteDate == null &&
                        a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Applicant> GetAllActive()
        {
            return MemorialContext.Applicants
                .Where(a => a.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}