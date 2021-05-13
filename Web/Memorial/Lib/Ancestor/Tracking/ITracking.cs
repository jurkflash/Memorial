using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface ITracking
    {
        void Add(int quadrangleId, string ancestorTransactionAF);

        void Add(int quadrangleId, string ancestorTransactionAF, int applicantId);

        void Add(int quadrangleId, string ancestorTransactionAF, int applicantId, int? deceasedId);

        void Change(int quadrangleId, string ancestorTransactionAF, int? applicantId, int? deceasedId);

        void Remove(int quadrangleId, string ancestorTransactionAF);

        Core.Domain.AncestorTracking GetLatestFirstTransactionByAncestorId(int ancestorId);

        IEnumerable<Core.Domain.AncestorTracking> GetTrackingByAncestorId(int ancestorId);

        Core.Domain.AncestorTracking GetTrackingByTransactionAF(string ancestorTransactionAF);

        void Delete(string ancestorTransactionAF);

        bool IsLatestTransaction(int ancestorId, string ancestorTransactionAF);
    }
}