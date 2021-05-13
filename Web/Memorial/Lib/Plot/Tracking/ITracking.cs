﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface ITracking
    {
        void Add(int plotId, string plotTransactionAF);

        void Add(int plotId, string plotTransactionAF, int applicantId);

        void Add(int plotId, string plotTransactionAF, int applicantId, int? deceased1Id);

        void Change(int plotId, string plotTransactionAF, int? applicantId, int? deceased1Id);

        void Remove(int plotId, string plotTransactionAF);

        Core.Domain.PlotTracking GetLatestFirstTransactionByPlotId(int plotId);

        IEnumerable<Core.Domain.PlotTracking> GetTrackingByPlotId(int plotId);

        IEnumerable<Core.Domain.PlotTracking> GetTrackingByTransactionAF(string plotTransactionAF);

        void Delete(string plotTransactionAF);

        bool IsLatestTransaction(int plotId, string plotTransactionAF);
    }
}