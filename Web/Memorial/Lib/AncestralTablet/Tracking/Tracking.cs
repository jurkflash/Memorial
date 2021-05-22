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
            _unitOfWork.AncestralTabletTrackings.Add(new Core.Domain.AncestralTabletTracking()
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
            var tracking = _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestorIdAndTransactionAF(ancestorId, ancestralTabletTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.DeceasedId = deceasedId;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void Remove(int ancestorId, string ancestralTabletTransactionAF)
        {
            var tracking = _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestorIdAndTransactionAF(ancestorId, ancestralTabletTransactionAF);

            _unitOfWork.AncestralTabletTrackings.Remove(tracking);

        }

        public Core.Domain.AncestralTabletTracking GetLatestFirstTransactionByAncestorId(int ancestorId)
        {
            return _unitOfWork.AncestralTabletTrackings.GetLatestFirstTransactionByAncestorId(ancestorId);
        }

        public IEnumerable<Core.Domain.AncestralTabletTracking> GetTrackingByAncestorId(int ancestorId)
        {
            return _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestorId(ancestorId);
        }

        public Core.Domain.AncestralTabletTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF)
        {
            return _unitOfWork.AncestralTabletTrackings.GetTrackingByTransactionAF(ancestralTabletTransactionAF);
        }

        public void Delete(string ancestralTabletTransactionAF)
        {
            var tracking = GetTrackingByTransactionAF(ancestralTabletTransactionAF);
            if (tracking != null)
                _unitOfWork.AncestralTabletTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int ancestorId, string ancestralTabletTransactionAF)
        {
            return GetLatestFirstTransactionByAncestorId(ancestorId).AncestralTabletTransactionAF == ancestralTabletTransactionAF;
        }
    }
}