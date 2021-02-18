using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Applicant GetByIC(string IC);

        Applicant GetActive(int id);

        IEnumerable<Applicant> GetAllActive();
    }
}
