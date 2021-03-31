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
    public class PlotApplicantDeceaseds : IPlotApplicantDeceaseds
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;
        private readonly IDeceased _deceased;
        private readonly IPlot _plot;

        public PlotApplicantDeceaseds(
            IUnitOfWork unitOfWork,
            ITracking tracking,
            IDeceased deceased,
            IPlot plot)
        {
            _unitOfWork = unitOfWork;
            _tracking = tracking;
            _deceased = deceased;
            _plot = plot;
        }

        public void ClearPlotApplicantAndDeceased(int plotId)
        {
            _plot.SetPlot(plotId);

            var deceaseds = _deceased.GetDeceasedsByPlotId(plotId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemovePlot();
            }

            _plot.SetHasDeceased(false);

            _plot.RemoveApplicant();
        }

        public bool SetPlotApplicantDeceaseds(int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null)
        {
            if (applicantId != null)
                _plot.SetApplicant((int)applicantId);

            if (deceased1Id != null)
            {
                _deceased.SetDeceased((int)deceased1Id);
                if (_deceased.SetPlot(_plot.GetPlot().Id))
                {
                    _plot.SetHasDeceased(true);
                }
                else
                    return false;
            }

            if (deceased2Id != null)
            {
                _deceased.SetDeceased((int)deceased2Id);
                if (_deceased.SetPlot(_plot.GetPlot().Id))
                {
                    _plot.SetHasDeceased(true);
                }
                else
                    return false;
            }

            return true;
        }

        public bool RollbackPlotApplicantDeceaseds(string plotTransactionAF, int plotId)
        {
            if (!_tracking.IsLatestTransaction(plotId, plotTransactionAF))
                return false;

            ClearPlotApplicantAndDeceased(plotId);

            var trackingsByPlotId = _tracking.GetTrackingByPlotId(plotId);

            if(trackingsByPlotId.Count() > 1)
            {
                SetPlotApplicantDeceaseds(trackingsByPlotId.ElementAt(1).ApplicantId, trackingsByPlotId.ElementAt(1).Deceased1Id, trackingsByPlotId.ElementAt(1).Deceased2Id);
            }

            _tracking.Delete(plotTransactionAF);

            return true;
        }
    }
}