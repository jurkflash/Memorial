using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface ITracking
    {
        void Add(int nicheId, string columbariumTransactionAF, int applicantId);
        void Add(int nicheId, string columbariumTransactionAF, int applicantId, int? deceased1Id);
        void Add(int nicheId, string columbariumTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id);
        void Add(int nicheId, string columbariumTransactionAF);
        void Change(int nicheId, string columbariumTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id);
        void ChangeNicheId(int trackingId, int nicheId);
        void Delete(string columbariumTransactionAF);
        ColumbariumTracking GetLatestFirstTransactionByNicheId(int nicheId);
        IEnumerable<ColumbariumTracking> GetTrackingByNicheId(int nicheId);
        ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF);
        bool IsLatestTransaction(int nicheId, string columbariumTransactionAF);
        void Remove(int nicheId, string columbariumTransactionAF);
    }
}