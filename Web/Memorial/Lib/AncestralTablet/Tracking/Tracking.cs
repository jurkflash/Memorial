using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public class Tracking : ITracking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Tracking(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int ancestorId, string ancestralTabletTransactionAF)
        {
            Create(ancestorId, ancestralTabletTransactionAF);
        }

        public void Add(int ancestorId, string ancestralTabletTransactionAF, int applicantId)
        {
            Create(ancestorId, ancestralTabletTransactionAF, applicantId);
        }

        public void Add(int ancestorId, string ancestralTabletTransactionAF, int applicantId, int? deceasedId)
        {
            Create(ancestorId, ancestralTabletTransactionAF, applicantId, deceasedId);
        }


        private void Create(int ancestorId, string ancestralTabletTransactionAF, int? applicantId = null, int? deceasedId = null)
        {
            _unitOfWork.AncestorTrackings.Add(new Core.Domain.AncestorTracking()
            {
                AncestorId = ancestorId,
                AncestralTabletTransactionAF = ancestralTabletTransactionAF,
                ApplicantId = applicantId,
                DeceasedId = deceasedId,
                ActionDate = System.DateTime.Now
            });

        }

        public void Change(int ancestorId, string ancestralTabletTransactionAF, int? applicantId, int? deceasedId)
        {
            var tracking = _unitOfWork.AncestorTrackings.GetTrackingByAncestorIdAndTransactionAF(ancestorId, ancestralTabletTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.DeceasedId = deceasedId;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void Remove(int ancestorId, string ancestralTabletTransactionAF)
        {
            var tracking = _unitOfWork.AncestorTrackings.GetTrackingByAncestorIdAndTransactionAF(ancestorId, ancestralTabletTransactionAF);

            _unitOfWork.AncestorTrackings.Remove(tracking);

        }

        public Core.Domain.AncestorTracking GetLatestFirstTransactionByAncestorId(int ancestorId)
        {
            return _unitOfWork.AncestorTrackings.GetLatestFirstTransactionByAncestorId(ancestorId);
        }

        public IEnumerable<Core.Domain.AncestorTracking> GetTrackingByAncestorId(int ancestorId)
        {
            return _unitOfWork.AncestorTrackings.GetTrackingByAncestorId(ancestorId);
        }

        public Core.Domain.AncestorTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF)
        {
            return _unitOfWork.AncestorTrackings.GetTrackingByTransactionAF(ancestralTabletTransactionAF);
        }

        public void Delete(string ancestralTabletTransactionAF)
        {
            var tracking = GetTrackingByTransactionAF(ancestralTabletTransactionAF);
            if (tracking != null)
                _unitOfWork.AncestorTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int ancestorId, string ancestralTabletTransactionAF)
        {
            return GetLatestFirstTransactionByAncestorId(ancestorId).AncestralTabletTransactionAF == ancestralTabletTransactionAF;
        }
    }
}