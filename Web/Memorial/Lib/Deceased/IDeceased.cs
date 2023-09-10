using System.Collections.Generic;

namespace Memorial.Lib.Deceased
{
    public interface IDeceased
    {
        IEnumerable<Core.Domain.Deceased> GetByApplicantId(int applicantId);
        Core.Domain.Deceased GetById(int id);
        bool GetExistsByIC(string ic, int? excludeId = null);
        IEnumerable<Core.Domain.Deceased> GetExcludeFilter(int applicantId, string deceasedName);
        IEnumerable<Core.Domain.Deceased> GetByNicheId(int nicheId);
        IEnumerable<Core.Domain.Deceased> GetByAncestralTabletId(int ancestralTabletId);
        IEnumerable<Core.Domain.Deceased> GetByPlotId(int plotId);
        bool IsRecordLinked(int id);
        int Add(Core.Domain.Deceased deceased, int applicantId, byte relationshipTypeId);
        bool Change(int id, Core.Domain.Deceased deceased);
        bool Remove(int id);
    }
}