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
        private readonly IPlot _quadrangle;

        public PlotApplicantDeceaseds(
            IUnitOfWork unitOfWork,
            ITracking tracking,
            IDeceased deceased,
            IPlot quadrangle)
        {
            _unitOfWork = unitOfWork;
            _tracking = tracking;
            _deceased = deceased;
            _quadrangle = quadrangle;
        }

        public void ClearPlotApplicantAndDeceased()
        {
            var deceaseds = _deceased.GetDeceasedsByPlotId(_quadrangle.GetPlot().Id);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemovePlot();
            }

            _quadrangle.SetHasDeceased(false);

            _quadrangle.RemoveApplicant();
        }

        public bool SetPlotApplicantDeceaseds(int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null)
        {
            if (applicantId != null)
                _quadrangle.SetApplicant((int)applicantId);

            if (deceased1Id != null)
            {
                _deceased.SetDeceased((int)deceased1Id);
                if (_deceased.SetPlot(_quadrangle.GetPlot().Id))
                {
                    _quadrangle.SetHasDeceased(true);
                }
                else
                    return false;
            }

            if (deceased2Id != null)
            {
                _deceased.SetDeceased((int)deceased2Id);
                if (_deceased.SetPlot(_quadrangle.GetPlot().Id))
                {
                    _quadrangle.SetHasDeceased(true);
                }
                else
                    return false;
            }

            return true;
        }

        public bool RollbackPlotApplicantDeceaseds(string quadrangleTransactionAF, int quadrangleId)
        {
            _quadrangle.SetPlot(quadrangleId);
            ClearPlotApplicantAndDeceased();

            if (!_tracking.IsLatestTransaction(quadrangleId, quadrangleTransactionAF))
                return false;

            var trackings = _tracking.GetTrackingByTransactionAF(quadrangleTransactionAF);

            if (trackings.Count() > 1)
            {
                //Shifted
                _quadrangle.SetPlot(trackings.ElementAt(1).PlotId);
                ClearPlotApplicantAndDeceased();
                SetPlotApplicantDeceaseds(trackings.ElementAt(0).ApplicantId, trackings.ElementAt(0).Deceased1Id, trackings.ElementAt(0).Deceased2Id);
            }

            if (trackings.Count() == 1)
            {
                var trackingsByPlotId = _tracking.GetTrackingByPlotId(quadrangleId);
                SetPlotApplicantDeceaseds(trackingsByPlotId.ElementAt(1).ApplicantId, trackingsByPlotId.ElementAt(1).Deceased1Id, trackingsByPlotId.ElementAt(1).Deceased2Id);
            }

            _tracking.Delete(quadrangleTransactionAF);

            return true;
        }
    }
}