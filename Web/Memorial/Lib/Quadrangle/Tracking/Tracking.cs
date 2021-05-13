using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public class Tracking : ITracking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Tracking(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int? quadrangleTrackingParentId)
        {
            Create(quadrangleId, quadrangleTransactionAF, quadrangleTrackingParentId);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? quadrangleTrackingParentId)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId, quadrangleTrackingParentId);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? quadrangleTrackingParentId)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId, deceased1Id, quadrangleTrackingParentId);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? quadrangleTrackingParentId)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId, deceased1Id, deceased2Id, quadrangleTrackingParentId);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id, int? shiftedFromQuadrangleId, int? quadrangleTrackingParentId)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId, deceased1Id, deceased2Id, shiftedFromQuadrangleId, quadrangleTrackingParentId);
        }

        private void Create(int quadrangleId, string quadrangleTransactionAF, int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null, int? shiftedFromQuadrangleId = null, int? quadrangleTrackingParentId = null)
        {
            _unitOfWork.QuadrangleTrackings.Add(new Core.Domain.QuadrangleTracking()
            {
                QuadrangleId = quadrangleId,
                QuadrangleTransactionAF = quadrangleTransactionAF,
                ApplicantId = applicantId,
                Deceased1Id = deceased1Id,
                Deceased2Id = deceased2Id,
                ShiftedFromQuadrangleId = shiftedFromQuadrangleId,
                QuadrangleTrackingParentId = quadrangleTrackingParentId,
                ActionDate = System.DateTime.Now
            });
        }

        public void Change(int quadrangleId, string quadrangleTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id)
        {
            var tracking = _unitOfWork.QuadrangleTrackings.GetTrackingByQuadrangleIdAndTransactionAF(quadrangleId, quadrangleTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.Deceased1Id = deceased1Id;

            tracking.Deceased2Id = deceased2Id;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void ChangeQuadrangleId(int trackingId, int quadrangleId)
        {
            var tracking = _unitOfWork.QuadrangleTrackings.Get(trackingId);

            tracking.QuadrangleId = quadrangleId;
        }

        public void Remove(int quadrangleId, string quadrangleTransactionAF)
        {
            var tracking = _unitOfWork.QuadrangleTrackings.GetTrackingByQuadrangleIdAndTransactionAF(quadrangleId, quadrangleTransactionAF);

            _unitOfWork.QuadrangleTrackings.Remove(tracking);
        }

        public Core.Domain.QuadrangleTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.QuadrangleTrackings.GetLatestFirstTransactionByQuadrangleId(quadrangleId);
        }

        public IEnumerable<Core.Domain.QuadrangleTracking> GetTrackingByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.QuadrangleTrackings.GetTrackingByQuadrangleId(quadrangleId);
        }

        public Core.Domain.QuadrangleTracking GetTrackingByTransactionAF(string quadrangleTransactionAF)
        {
            return _unitOfWork.QuadrangleTrackings.GetTrackingByTransactionAF(quadrangleTransactionAF);
        }

        public void Delete(string quadrangleTransactionAF)
        {
            var tracking = GetTrackingByTransactionAF(quadrangleTransactionAF);
            if (tracking != null)
                _unitOfWork.QuadrangleTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int quadrangleId, string quadrangleTransactionAF)
        {
            return GetLatestFirstTransactionByQuadrangleId(quadrangleId).QuadrangleTransactionAF == quadrangleTransactionAF;
        }
    }
}