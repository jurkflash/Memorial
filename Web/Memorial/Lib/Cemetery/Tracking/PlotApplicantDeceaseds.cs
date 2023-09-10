using Memorial.Core;
using System.Linq;
using Memorial.Lib.Deceased;

namespace Memorial.Lib.Cemetery
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

        private void ClearPlotApplicantAndDeceased(Core.Domain.Plot plot)
        {
            var deceaseds = _deceased.GetByPlotId(plot.Id);

            foreach (var deceased in deceaseds)
            {
                deceased.Plot = null;
                deceased.PlotId = null;
            }

            plot.hasDeceased = false;

            plot.Applicant = null;
            plot.ApplicantId = null;
        }

        private bool SetPlotApplicantDeceaseds(Core.Domain.Plot plot, int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null)
        {
            if (applicantId != null)
                plot.ApplicantId = (int)applicantId;

            if (deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)deceased1Id);
                deceased.PlotId = plot.Id;
                plot.hasDeceased = true;
            }

            if (deceased2Id != null)
            {
                var deceased = _deceased.GetById((int)deceased2Id);
                deceased.PlotId = plot.Id;
                plot.hasDeceased = true;
            }

            return true;
        }

        public bool RollbackPlotApplicantDeceaseds(string cemeteryTransactionAF, int plotId)
        {
            if (!_tracking.IsLatestTransaction(plotId, cemeteryTransactionAF))
                return false;

            var plot = _plot.GetById(plotId);
            ClearPlotApplicantAndDeceased(plot);

            var trackingsByPlotId = _tracking.GetTrackingByPlotId(plotId);

            if(trackingsByPlotId.Count() > 1)
            {
                SetPlotApplicantDeceaseds(plot, trackingsByPlotId.ElementAt(1).ApplicantId, trackingsByPlotId.ElementAt(1).Deceased1Id, trackingsByPlotId.ElementAt(1).Deceased2Id);
            }

            _tracking.Delete(cemeteryTransactionAF);

            return true;
        }
    }
}