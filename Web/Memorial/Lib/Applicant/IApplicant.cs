using System.Collections.Generic;

namespace Memorial.Lib.Applicant
{
    public interface IApplicant
    {
        Core.Domain.Applicant Get(int id);
        IEnumerable<Core.Domain.Applicant> GetAll(string filter);
        bool IsRecordLinked(int id);
        int Add(Core.Domain.Applicant applicant);
        bool Change(int id, Core.Domain.Applicant applicant);
        bool Remove(int id);
        bool GetExistsByIC(string ic, int? excludeId = null);
    }
}