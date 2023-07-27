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
                .Where(a => a.IC == IC).FirstOrDefault();
        }

        public bool GetExistsByIC(string IC, int? excludeId = null)
        {
            if(excludeId == null)
                return MemorialContext.Applicants.Where(a => a.IC == IC).Any();
            return MemorialContext.Applicants.Where(a => a.IC == IC && a.Id != excludeId).Any();
        }

        public Applicant GetActive(int id)
        {
            return MemorialContext.Applicants
                .Where(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Applicant> GetAllActive(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return MemorialContext.Applicants.ToList();
            }
            else
            {
                return MemorialContext.Applicants.Where(a => a.Name.Contains(filter) || a.IC.Contains(filter) || a.Name2.Contains(filter)).ToList();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}