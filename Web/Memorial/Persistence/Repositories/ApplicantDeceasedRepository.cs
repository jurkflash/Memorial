using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class ApplicantDeceasedRepository : Repository<ApplicantDeceased>, IApplicantDeceasedRepository
    {
        public ApplicantDeceasedRepository(MemorialContext context) : base(context)
        {
        }

        public ApplicantDeceased GetActive(int id)
        {
            return MemorialContext.ApplicantDeceaseds
                .Include(ad => ad.Applicant)
                .Include(ad => ad.Deceased)
                .Include(ad => ad.RelationshipType)
                .Where(a => a.Id == id).SingleOrDefault();
        }

        public IEnumerable<ApplicantDeceased> GetByDeceasedId(int id)
        {
            return MemorialContext.ApplicantDeceaseds
                .Include(ad => ad.Applicant)
                .Include(ad => ad.Deceased)
                .Include(ad => ad.RelationshipType)
                .Where(a => a.DeceasedId == id).ToList();
        }

        public IEnumerable<ApplicantDeceased> GetByApplicantId(int id)
        {
            return MemorialContext.ApplicantDeceaseds
                .Include(ad => ad.Applicant)
                .Include(ad => ad.Deceased)
                .Include(ad => ad.RelationshipType)
                .Where(a => a.ApplicantId == id).ToList();
        }

        public ApplicantDeceased GetByApplicantDeceasedId(int applicantId, int deceasedId)
        {
            return MemorialContext.ApplicantDeceaseds
                .Include(ad => ad.Applicant)
                .Include(ad => ad.Deceased)
                .Include(ad => ad.RelationshipType)
                .Where(a => a.ApplicantId == applicantId &&
                a.DeceasedId == deceasedId).FirstOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}