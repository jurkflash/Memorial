using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface ITracking
    {
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId);
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id);
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id);
        void Add(int quadrangleId, string quadrangleTransactionAF);
        void Change(int quadrangleId, string quadrangleTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id);
        void ChangeQuadrangleId(int trackingId, int quadrangleId);
        void Delete(string quadrangleTransactionAF);
        ColumbariumTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId);
        IEnumerable<ColumbariumTracking> GetTrackingByQuadrangleId(int quadrangleId);
        ColumbariumTracking GetTrackingByTransactionAF(string quadrangleTransactionAF);
        bool IsLatestTransaction(int quadrangleId, string quadrangleTransactionAF);
        void Remove(int quadrangleId, string quadrangleTransactionAF);
    }
}