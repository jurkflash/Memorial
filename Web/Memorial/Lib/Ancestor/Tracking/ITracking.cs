using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface ITracking
    {
        void Add(int nicheId, string ancestorTransactionAF);

        void Add(int nicheId, string ancestorTransactionAF, int applicantId);

        void Add(int nicheId, string ancestorTransactionAF, int applicantId, int? deceasedId);

        void Change(int nicheId, string ancestorTransactionAF, int? applicantId, int? deceasedId);

        void Remove(int nicheId, string ancestorTransactionAF);

        Core.Domain.AncestorTracking GetLatestFirstTransactionByAncestorId(int ancestorId);

        IEnumerable<Core.Domain.AncestorTracking> GetTrackingByAncestorId(int ancestorId);

        Core.Domain.AncestorTracking GetTrackingByTransactionAF(string ancestorTransactionAF);

        void Delete(string ancestorTransactionAF);

        bool IsLatestTransaction(int ancestorId, string ancestorTransactionAF);
    }
}