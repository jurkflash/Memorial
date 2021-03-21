using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface ITracking
    {
        void Add(int quadrangleId, string quadrangleTransactionAF);

        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId);

        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id);

        void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id);

        void Change(int quadrangleId, string quadrangleTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id);

        void Remove(int quadrangleId, string quadrangleTransactionAF);

        Core.Domain.QuadrangleTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId);

        IEnumerable<Core.Domain.QuadrangleTracking> GetTrackingByQuadrangleId(int quadrangleId);

        IEnumerable<Core.Domain.QuadrangleTracking> GetTrackingByTransactionAF(string quadrangleTransactionAF);

        void Delete(string quadrangleTransactionAF);

        bool IsLatestTransaction(int quadrangleId, string quadrangleTransactionAF);
    }
}