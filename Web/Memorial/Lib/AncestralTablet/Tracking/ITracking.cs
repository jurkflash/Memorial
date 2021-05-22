﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface ITracking
    {
        void Add(int nicheId, string ancestralTabletTransactionAF);

        void Add(int nicheId, string ancestralTabletTransactionAF, int applicantId);

        void Add(int nicheId, string ancestralTabletTransactionAF, int applicantId, int? deceasedId);

        void Change(int nicheId, string ancestralTabletTransactionAF, int? applicantId, int? deceasedId);

        void Remove(int nicheId, string ancestralTabletTransactionAF);

        Core.Domain.AncestorTracking GetLatestFirstTransactionByAncestorId(int ancestorId);

        IEnumerable<Core.Domain.AncestorTracking> GetTrackingByAncestorId(int ancestorId);

        Core.Domain.AncestorTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF);

        void Delete(string ancestralTabletTransactionAF);

        bool IsLatestTransaction(int ancestorId, string ancestralTabletTransactionAF);
    }
}