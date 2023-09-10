using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public interface ITracking
    {
        void Add(int ancestralTabletId, string ancestralTabletTransactionAF);

        void Add(int ancestralTabletId, string ancestralTabletTransactionAF, int applicantId);

        void Add(int ancestralTabletId, string ancestralTabletTransactionAF, int applicantId, int? deceasedId);

        void Change(int ancestralTabletId, string ancestralTabletTransactionAF, int? applicantId, int? deceasedId);

        void Remove(int ancestralTabletId, string ancestralTabletTransactionAF);

        Core.Domain.AncestralTabletTracking GetLatestFirstTransactionByAncestralTabletId(int ancestralTabletId);

        IEnumerable<Core.Domain.AncestralTabletTracking> GetByAncestralTabletId(int ancestralTabletId);

        IEnumerable<Core.Domain.AncestralTabletTracking> GetByAncestralTabletId(int ancestralTabletId, bool ToDeleteFlag);

        Core.Domain.AncestralTabletTracking GetByAF(string ancestralTabletTransactionAF);

        void Delete(string ancestralTabletTransactionAF);

        bool IsLatestTransaction(int ancestralTabletId, string ancestralTabletTransactionAF);
    }
}