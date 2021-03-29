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

        public void Add(int ancestorId, string ancestorTransactionAF)
        {
            Create(ancestorId, ancestorTransactionAF);
        }

        public void Add(int ancestorId, string ancestorTransactionAF, int applicantId)
        {
            Create(ancestorId, ancestorTransactionAF, applicantId);
        }

        public void Add(int ancestorId, string ancestorTransactionAF, int applicantId, int? deceasedId)
        {
            Create(ancestorId, ancestorTransactionAF, applicantId, deceasedId);
        }


        private void Create(int ancestorId, string ancestorTransactionAF, int? applicantId = null, int? deceasedId = null)
        {
            _unitOfWork.AncestorTrackings.Add(new Core.Domain.AncestorTracking()
            {
                AncestorId = ancestorId,
                AncestorTransactionAF = ancestorTransactionAF,
                ApplicantId = applicantId,
                DeceasedId = deceasedId,
                ActionDate = System.DateTime.Now
            });

        }

        public void Change(int ancestorId, string ancestorTransactionAF, int? applicantId, int? deceasedId)
        {
            var tracking = _unitOfWork.AncestorTrackings.GetTrackingByAncestorIdAndTransactionAF(ancestorId, ancestorTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.DeceasedId = deceasedId;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void Remove(int ancestorId, string ancestorTransactionAF)
        {
            var tracking = _unitOfWork.AncestorTrackings.GetTrackingByAncestorIdAndTransactionAF(ancestorId, ancestorTransactionAF);

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

        public IEnumerable<Core.Domain.AncestorTracking> GetTrackingByTransactionAF(string ancestorTransactionAF)
        {
            return _unitOfWork.AncestorTrackings.GetTrackingByTransactionAF(ancestorTransactionAF);
        }

        public void Delete(string ancestorTransactionAF)
        {
            var trackings = _unitOfWork.AncestorTrackings.GetTrackingByTransactionAF(ancestorTransactionAF);
            foreach(var tracking in trackings)
            {
                _unitOfWork.AncestorTrackings.Remove(tracking);
            }
        }

        public bool IsLatestTransaction(int ancestorId, string ancestorTransactionAF)
        {
            return GetLatestFirstTransactionByAncestorId(ancestorId).AncestorTransactionAF == ancestorTransactionAF;
        }
    }
}