using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IApplicantDeceasedRepository : IRepository<ApplicantDeceased>
    {
        ApplicantDeceased GetActive(int id);

        IEnumerable<ApplicantDeceased> GetByDeceasedId(int id);

        IEnumerable<ApplicantDeceased> GetByApplicantId(int id);

        ApplicantDeceased GetByApplicantDeceasedId(int applicantId, int deceasedId);
    }
}
