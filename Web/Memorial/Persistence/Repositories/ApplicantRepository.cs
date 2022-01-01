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
                    a.DeletedDate == null).FirstOrDefault();
        }

        public Applicant GetActive(int id)
        {
            return MemorialContext.Applicants
                .Where(a => a.DeletedDate == null &&
                        a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Applicant> GetAllActive(string filter)
        {
            var applicants = MemorialContext.Applicants
                .Where(a => a.DeletedDate == null);

            if (string.IsNullOrEmpty(filter))
            {
                return applicants.ToList();
            }
            else
            {
                return applicants.Where(a => a.Name.Contains(filter) || a.IC.Contains(filter) || a.Name2.Contains(filter)).ToList();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}