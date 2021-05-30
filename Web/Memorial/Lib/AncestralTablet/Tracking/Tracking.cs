﻿using Memorial.Core;
using System.Collections.Generic;

namespace Memorial.Lib.AncestralTablet
{
    public class Tracking : ITracking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Tracking(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int ancestralTabletId, string ancestralTabletTransactionAF)
        {
            Create(ancestralTabletId, ancestralTabletTransactionAF);
        }

        public void Add(int ancestralTabletId, string ancestralTabletTransactionAF, int applicantId)
        {
            Create(ancestralTabletId, ancestralTabletTransactionAF, applicantId);
        }

        public void Add(int ancestralTabletId, string ancestralTabletTransactionAF, int applicantId, int? deceasedId)
        {
            Create(ancestralTabletId, ancestralTabletTransactionAF, applicantId, deceasedId);
        }


        private void Create(int ancestralTabletId, string ancestralTabletTransactionAF, int? applicantId = null, int? deceasedId = null)
        {
            _unitOfWork.AncestralTabletTrackings.Add(new Core.Domain.AncestralTabletTracking()
            {
                AncestralTabletId = ancestralTabletId,
                AncestralTabletTransactionAF = ancestralTabletTransactionAF,
                ApplicantId = applicantId,
                DeceasedId = deceasedId,
                ToDeleteFlag = false,
                ActionDate = System.DateTime.Now
            });

        }

        public void Change(int ancestralTabletId, string ancestralTabletTransactionAF, int? applicantId, int? deceasedId)
        {
            var tracking = _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestralTabletIdAndTransactionAF(ancestralTabletId, ancestralTabletTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.DeceasedId = deceasedId;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void Remove(int ancestralTabletId, string ancestralTabletTransactionAF)
        {
            var tracking = _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestralTabletIdAndTransactionAF(ancestralTabletId, ancestralTabletTransactionAF);

            _unitOfWork.AncestralTabletTrackings.Remove(tracking);

        }

        public Core.Domain.AncestralTabletTracking GetLatestFirstTransactionByAncestralTabletId(int ancestralTabletId)
        {
            return _unitOfWork.AncestralTabletTrackings.GetLatestFirstTransactionByAncestralTabletId(ancestralTabletId);
        }

        public IEnumerable<Core.Domain.AncestralTabletTracking> GetTrackingByAncestralTabletId(int ancestralTabletId)
        {
            return _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestralTabletId(ancestralTabletId, false);
        }

        public IEnumerable<Core.Domain.AncestralTabletTracking> GetTrackingByAncestralTabletId(int ancestralTabletId, bool ToDeleteFlag)
        {
            return _unitOfWork.AncestralTabletTrackings.GetTrackingByAncestralTabletId(ancestralTabletId, ToDeleteFlag);
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

        public bool IsLatestTransaction(int ancestralTabletId, string ancestralTabletTransactionAF)
        {
            return GetLatestFirstTransactionByAncestralTabletId(ancestralTabletId).AncestralTabletTransactionAF == ancestralTabletTransactionAF;
        }
    }
}