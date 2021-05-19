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

        public void Add(int quadrangleId, string quadrangleTransactionAF)
        {
            Create(quadrangleId, quadrangleTransactionAF);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId, deceased1Id);
        }

        public void Add(int quadrangleId, string quadrangleTransactionAF, int applicantId, int? deceased1Id, int? deceased2Id)
        {
            Create(quadrangleId, quadrangleTransactionAF, applicantId, deceased1Id, deceased2Id);
        }

        private void Create(int quadrangleId, string quadrangleTransactionAF, int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null)
        {
            _unitOfWork.ColumbariumTrackings.Add(new Core.Domain.ColumbariumTracking()
            {
                NicheId = quadrangleId,
                ColumbariumTransactionAF = quadrangleTransactionAF,
                ApplicantId = applicantId,
                Deceased1Id = deceased1Id,
                Deceased2Id = deceased2Id,
                ActionDate = System.DateTime.Now
            });
        }

        public void Change(int quadrangleId, string quadrangleTransactionAF, int? applicantId, int? deceased1Id, int? deceased2Id)
        {
            var tracking = _unitOfWork.ColumbariumTrackings.GetTrackingByNicheIdAndTransactionAF(quadrangleId, quadrangleTransactionAF);

            tracking.ApplicantId = applicantId;

            tracking.Deceased1Id = deceased1Id;

            tracking.Deceased2Id = deceased2Id;

            tracking.ActionDate = System.DateTime.Now;
        }

        public void ChangeQuadrangleId(int trackingId, int quadrangleId)
        {
            var tracking = _unitOfWork.ColumbariumTrackings.Get(trackingId);

            tracking.NicheId = quadrangleId;
        }

        public void Remove(int quadrangleId, string quadrangleTransactionAF)
        {
            var tracking = _unitOfWork.ColumbariumTrackings.GetTrackingByNicheIdAndTransactionAF(quadrangleId, quadrangleTransactionAF);

            _unitOfWork.ColumbariumTrackings.Remove(tracking);
        }

        public Core.Domain.ColumbariumTracking GetLatestFirstTransactionByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.ColumbariumTrackings.GetLatestFirstTransactionByNicheId(quadrangleId);
        }

        public IEnumerable<Core.Domain.ColumbariumTracking> GetTrackingByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.ColumbariumTrackings.GetTrackingByNicheId(quadrangleId);
        }

        public Core.Domain.ColumbariumTracking GetTrackingByTransactionAF(string quadrangleTransactionAF)
        {
            return _unitOfWork.ColumbariumTrackings.GetTrackingByTransactionAF(quadrangleTransactionAF);
        }

        public void Delete(string quadrangleTransactionAF)
        {
            var tracking = GetTrackingByTransactionAF(quadrangleTransactionAF);
            if (tracking != null)
                _unitOfWork.ColumbariumTrackings.Remove(tracking);
        }

        public bool IsLatestTransaction(int quadrangleId, string quadrangleTransactionAF)
        {
            return GetLatestFirstTransactionByQuadrangleId(quadrangleId).ColumbariumTransactionAF == quadrangleTransactionAF;
        }
    }
}