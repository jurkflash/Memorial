using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public class Tracking : ITracking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Tracking(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int plotId, string plotTransactionAF)
        {
            Create(plotId, plotTransactionAF);
        }

        public void Add(int plotId, string plotTransactionAF, int applicantId)
        {
            Create(plotId, plotTransactionAF, applicantId);
        }

        public void Add(int plotId, string plotTransactionAF, int applicantId, int? deceased1Id)
        {
            Create(plotId, plotTransactionAF, applicantId, deceased1Id);
        }

        private void Create(int plotId, string plotTransactionAF, int? applicantId = null, int? deceased1Id = null)
        {
            _unitOfWork.PlotTrackings.Add(new Core.Domain.PlotTracking()
            {
                PlotId = plotId,
                PlotTransactionAF = plotTransactionAF,
                ApplicantId = applicantId,
                Deceased1Id = deceased1Id,
                ActionDate = System.DateTime.Now
            });

        }

        public void Change(int plotId, string plotTransactionAF, int? applicantId, int? deceased1Id)
        {
            var tracking = _unitOfWork.PlotTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, plotTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.Deceased1Id = deceased1Id;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void Remove(int plotId, string plotTransactionAF)
        {
            var tracking = _unitOfWork.PlotTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, plotTransactionAF);

            _unitOfWork.PlotTrackings.Remove(tracking);

        }

        public Core.Domain.PlotTracking GetLatestFirstTransactionByPlotId(int plotId)
        {
            return _unitOfWork.PlotTrackings.GetLatestFirstTransactionByPlotId(plotId);
        }

        public IEnumerable<Core.Domain.PlotTracking> GetTrackingByPlotId(int plotId)
        {
            return _unitOfWork.PlotTrackings.GetTrackingByPlotId(plotId);
        }

        public IEnumerable<Core.Domain.PlotTracking> GetTrackingByTransactionAF(string plotTransactionAF)
        {
            return _unitOfWork.PlotTrackings.GetTrackingByTransactionAF(plotTransactionAF);
        }

        public void Delete(string plotTransactionAF)
        {
            var trackings = _unitOfWork.PlotTrackings.GetTrackingByTransactionAF(plotTransactionAF);
            foreach(var tracking in trackings)
            {
                _unitOfWork.PlotTrackings.Remove(tracking);
            }
        }

        public bool IsLatestTransaction(int plotId, string plotTransactionAF)
        {
            return GetLatestFirstTransactionByPlotId(plotId).PlotTransactionAF == plotTransactionAF;
        }
    }
}