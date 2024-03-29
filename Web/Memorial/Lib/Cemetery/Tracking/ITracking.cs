﻿using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public interface ITracking
    {
        void Add(int plotId, string cemeteryTransactionAF);

        void Add(int plotId, string cemeteryTransactionAF, int applicantId);

        void Add(int plotId, string cemeteryTransactionAF, int applicantId, int? deceased1Id);

        void Add(int plotId, string cemeteryTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id);

        void Add(int plotId, string cemeteryTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? deceased3Id);

        void AddDeceased(int plotId, int deceasedId);

        void Change(int plotId, string cemeteryTransactionAF, int? applicantId, int? deceased1Id);

        void ChangeDeceased(int plotId, string cemeteryTransactionAF, int oldDeceasedId, int newDeceasedId);

        void Remove(int plotId, string cemeteryTransactionAF);

        void RemoveDeceased(int plotId, string cemeteryTransactionAF, int deceasedId);

        Core.Domain.CemeteryTracking GetLatestFirstTransactionByPlotId(int plotId);

        IEnumerable<Core.Domain.CemeteryTracking> GetTrackingByPlotId(int plotId);

        Core.Domain.CemeteryTracking GetTrackingByTransactionAF(string cemeteryTransactionAF);

        void Delete(string cemeteryTransactionAF);

        bool IsLatestTransaction(int plotId, string cemeteryTransactionAF);
    }
}