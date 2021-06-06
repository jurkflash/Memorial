using Memorial.Core;
using System.Collections.Generic;

namespace Memorial.Lib.Cemetery
{
    public class Tracking : ITracking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Tracking(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int plotId, string cemeteryTransactionAF)
        {
            Create(plotId, cemeteryTransactionAF);
        }

        public void Add(int plotId, string cemeteryTransactionAF, int applicantId)
        {
            Create(plotId, cemeteryTransactionAF, applicantId);
        }

        public void Add(int plotId, string cemeteryTransactionAF, int applicantId, int? deceased1Id)
        {
            Create(plotId, cemeteryTransactionAF, applicantId, deceased1Id);
        }

        public void Add(int plotId, string cemeteryTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id)
        {
            Create(plotId, cemeteryTransactionAF, applicantId, deceased1Id, deceased2Id);
        }

        public void Add(int plotId, string cemeteryTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? deceased3Id)
        {
            Create(plotId, cemeteryTransactionAF, applicantId, deceased1Id, deceased2Id, deceased3Id);
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

        private void Create(int plotId, string cemeteryTransactionAF, int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null, int? deceased3Id = null)
        {
            _unitOfWork.CemeteryTrackings.Add(new Core.Domain.CemeteryTracking()
            {
                PlotId = plotId,
                CemeteryTransactionAF = cemeteryTransactionAF,
                ApplicantId = applicantId,
                Deceased1Id = deceased1Id,
                Deceased2Id = deceased2Id,
                Deceased3Id = deceased3Id,
                ActionDate = System.DateTime.Now
            });

        }

        public void Change(int plotId, string cemeteryTransactionAF, int? applicantId, int? deceased1Id)
        {
            var tracking = _unitOfWork.CemeteryTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, cemeteryTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.Deceased1Id = deceased1Id;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void ChangeDeceased(int plotId, string cemeteryTransactionAF, int oldDeceasedId, int newDeceasedId)
        {
            var tracking = _unitOfWork.CemeteryTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, cemeteryTransactionAF);

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

        public void Remove(int plotId, string cemeteryTransactionAF)
        {
            var tracking = _unitOfWork.CemeteryTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, cemeteryTransactionAF);

            _unitOfWork.CemeteryTrackings.Remove(tracking);

        }

        public void RemoveDeceased(int plotId, string cemeteryTransactionAF, int deceasedId)
        {
            var tracking = _unitOfWork.CemeteryTrackings.GetTrackingByPlotIdAndTransactionAF(plotId, cemeteryTransactionAF);

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

        public Core.Domain.CemeteryTracking GetLatestFirstTransactionByPlotId(int plotId)
        {
            return _unitOfWork.CemeteryTrackings.GetLatestFirstTransactionByPlotId(plotId);
        }

        public IEnumerable<Core.Domain.CemeteryTracking> GetTrackingByPlotId(int plotId)
        {
            return _unitOfWork.CemeteryTrackings.GetTrackingByPlotId(plotId);
        }

        public Core.Domain.CemeteryTracking GetTrackingByTransactionAF(string cemeteryTransactionAF)
        {
            return _unitOfWork.CemeteryTrackings.GetTrackingByTransactionAF(cemeteryTransactionAF);
        }

        public void Delete(string cemeteryTransactionAF)
        {
            var tracking = _unitOfWork.CemeteryTrackings.GetTrackingByTransactionAF(cemeteryTransactionAF);
            if (tracking != null)
                _unitOfWork.CemeteryTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int plotId, string cemeteryTransactionAF)
        {
            return GetLatestFirstTransactionByPlotId(plotId).CemeteryTransactionAF == cemeteryTransactionAF;
        }
    }
}