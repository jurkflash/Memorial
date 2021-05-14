using Memorial.Core;
using System.Collections.Generic;

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

        public void Add(int plotId, string plotTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id)
        {
            Create(plotId, plotTransactionAF, applicantId, deceased1Id, deceased2Id);
        }

        public void Add(int plotId, string plotTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? deceased3Id)
        {
            Create(plotId, plotTransactionAF, applicantId, deceased1Id, deceased2Id, deceased3Id);
        }

        public void AddDeceased(int plotId, int deceasedId)
        {
            var tracking = GetLatestFirstTransactionByPlotId(plotId);

            if (tracking.Deceased1Id == null)
            {
                tracking.Deceased1Id = deceasedId;
            }
            else if (tracking.Deceased2Id == null)
            {
                tracking.Deceased2Id = deceasedId;
            }
            else if (tracking.Deceased3Id == null)
            {
                tracking.Deceased3Id = deceasedId;
            }
            else
            {
                return;
            }
        }

        private void Create(int plotId, string plotTransactionAF, int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null, int? deceased3Id = null)
        {
            _unitOfWork.PlotTrackings.Add(new Core.Domain.PlotTracking()
            {
                PlotId = plotId,
                PlotTransactionAF = plotTransactionAF,
                ApplicantId = applicantId,
                Deceased1Id = deceased1Id,
                Deceased2Id = deceased2Id,
                Deceased3Id = deceased3Id,
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

        public void ChangeDeceased(int plotId, string plotTransactionAF, int oldDeceasedId, int newDeceasedId)
        {
            var tracking = _unitOfWork.PlotTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, plotTransactionAF);

            if(tracking.Deceased1Id == oldDeceasedId)
            {
                tracking.Deceased1Id = newDeceasedId;
            }
            else if (tracking.Deceased2Id == oldDeceasedId)
            {
                tracking.Deceased2Id = newDeceasedId;
            }
            else if (tracking.Deceased3Id == oldDeceasedId)
            {
                tracking.Deceased3Id = newDeceasedId;
            }
            else
            {
                return;
            }

            tracking.ActionDate = System.DateTime.Now;
        }

        public void Remove(int plotId, string plotTransactionAF)
        {
            var tracking = _unitOfWork.PlotTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, plotTransactionAF);

            _unitOfWork.PlotTrackings.Remove(tracking);

        }

        public void RemoveDeceased(int plotId, string plotTransactionAF, int deceasedId)
        {
            var tracking = _unitOfWork.PlotTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, plotTransactionAF);

            if (tracking.Deceased1Id == deceasedId)
            {
                tracking.Deceased1Id = null;
            }
            else if (tracking.Deceased2Id == deceasedId)
            {
                tracking.Deceased2Id = null;
            }
            else if (tracking.Deceased3Id == deceasedId)
            {
                tracking.Deceased3Id = null;
            }
            else
            {
                return;
            }
        }

        public Core.Domain.PlotTracking GetLatestFirstTransactionByPlotId(int plotId)
        {
            return _unitOfWork.PlotTrackings.GetLatestFirstTransactionByPlotId(plotId);
        }

        public IEnumerable<Core.Domain.PlotTracking> GetTrackingByPlotId(int plotId)
        {
            return _unitOfWork.PlotTrackings.GetTrackingByPlotId(plotId);
        }

        public Core.Domain.PlotTracking GetTrackingByTransactionAF(string plotTransactionAF)
        {
            return _unitOfWork.PlotTrackings.GetTrackingByTransactionAF(plotTransactionAF);
        }

        public void Delete(string plotTransactionAF)
        {
            var tracking = _unitOfWork.PlotTrackings.GetTrackingByTransactionAF(plotTransactionAF);
            if (tracking != null)
                _unitOfWork.PlotTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int plotId, string plotTransactionAF)
        {
            return GetLatestFirstTransactionByPlotId(plotId).PlotTransactionAF == plotTransactionAF;
        }
    }
}