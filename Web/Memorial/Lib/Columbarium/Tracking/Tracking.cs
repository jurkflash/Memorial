using Memorial.Core;
using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public class Tracking : ITracking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Tracking(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int nicheId, string columbariumTransactionAF)
        {
            Create(nicheId, columbariumTransactionAF);
        }

        public void Add(int nicheId, string columbariumTransactionAF, int applicantId)
        {
            Create(nicheId, columbariumTransactionAF, applicantId);
        }

        public void Add(int nicheId, string columbariumTransactionAF, int applicantId, int? deceased1Id)
        {
            Create(nicheId, columbariumTransactionAF, applicantId, deceased1Id);
        }

        public void Add(int nicheId, string columbariumTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id)
        {
            Create(nicheId, columbariumTransactionAF, applicantId, deceased1Id, deceased2Id);
        }

        private void Create(int nicheId, string columbariumTransactionAF, int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null)
        {
            _unitOfWork.ColumbariumTrackings.Add(new Core.Domain.ColumbariumTracking()
            {
                NicheId = nicheId,
                ColumbariumTransactionAF = columbariumTransactionAF,
                ApplicantId = applicantId,
                Deceased1Id = deceased1Id,
                Deceased2Id = deceased2Id,
                ToDeleteFlag = false,
                ActionDate = System.DateTime.Now
            });
        }

        public void Change(int nicheId, string columbariumTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id)
        {
            var tracking = _unitOfWork.ColumbariumTrackings.GetTrackingByNicheIdAndTransactionAF(nicheId, columbariumTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.Deceased1Id = deceased1Id;

            tracking.Deceased2Id = deceased2Id;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void ChangeNicheId(int trackingId, int nicheId)
        {
            var tracking = _unitOfWork.ColumbariumTrackings.Get(trackingId);

            tracking.NicheId = nicheId;
        }

        public void Remove(int nicheId, string columbariumTransactionAF)
        {
            var tracking = _unitOfWork.ColumbariumTrackings.GetTrackingByNicheIdAndTransactionAF(nicheId, columbariumTransactionAF);

            _unitOfWork.ColumbariumTrackings.Remove(tracking);
        }

        public Core.Domain.ColumbariumTracking GetLatestFirstTransactionByNicheId(int nicheId)
        {
            return _unitOfWork.ColumbariumTrackings.GetLatestFirstTransactionByNicheId(nicheId);
        }

        public IEnumerable<Core.Domain.ColumbariumTracking> GetTrackingByNicheId(int nicheId)
        {
            return _unitOfWork.ColumbariumTrackings.GetTrackingByNicheId(nicheId, false);
        }

        public IEnumerable<Core.Domain.ColumbariumTracking> GetTrackingByNicheId(int nicheId, bool toDeleteFlag)
        {
            return _unitOfWork.ColumbariumTrackings.GetTrackingByNicheId(nicheId, toDeleteFlag);
        }

        public Core.Domain.ColumbariumTracking GetTrackingByTransactionAF(string columbariumTransactionAF)
        {
            return _unitOfWork.ColumbariumTrackings.GetTrackingByTransactionAF(columbariumTransactionAF);
        }

        public void Delete(string columbariumTransactionAF)
        {
            var tracking = GetTrackingByTransactionAF(columbariumTransactionAF);
            if (tracking != null)
                _unitOfWork.ColumbariumTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int nicheId, string columbariumTransactionAF)
        {
            return GetLatestFirstTransactionByNicheId(nicheId).ColumbariumTransactionAF == columbariumTransactionAF;
        }
    }
}