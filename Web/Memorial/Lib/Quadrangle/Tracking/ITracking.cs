using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Lib.Quadrangle
{
    public interface ITracking
    {
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? quadrangleTrackingParentId);
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? quadrangleTrackingParentId);
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? quadrangleTrackingParentId);
        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? shiftedFromQuadrangleId, int? quadrangleTrackingParentId);
        void Add(int quadrangleId, string quadrangleTransactionAF, int? quadrangleTrackingParentId);
        void Change(int quadrangleId, string quadrangleTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id);
        void ChangeQuadrangleId(int trackingId, int quadrangleId);
        void Delete(string quadrangleTransactionAF);
        QuadrangleTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId);
        IEnumerable<QuadrangleTracking> GetTrackingByQuadrangleId(int quadrangleId);
        QuadrangleTracking GetTrackingByTransactionAF(string quadrangleTransactionAF);
        bool IsLatestTransaction(int quadrangleId, string quadrangleTransactionAF);
        void Remove(int quadrangleId, string quadrangleTransactionAF);
    }
}