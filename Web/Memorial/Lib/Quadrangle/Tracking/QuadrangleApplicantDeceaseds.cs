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
    public class QuadrangleApplicantDeceaseds : IQuadrangleApplicantDeceaseds
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;
        private readonly IDeceased _deceased;
        private readonly IQuadrangle _quadrangle;

        public QuadrangleApplicantDeceaseds(
            IUnitOfWork unitOfWork,
            ITracking tracking,
            IDeceased deceased,
            IQuadrangle quadrangle)
        {
            _unitOfWork = unitOfWork;
            _tracking = tracking;
            _deceased = deceased;
            _quadrangle = quadrangle;
        }

        public void ClearQuadrangleApplicantAndDeceased()
        {
            var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_quadrangle.GetQuadrangle().Id);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveQuadrangle();
            }

            _quadrangle.SetHasDeceased(false);

            _quadrangle.RemoveApplicant();
        }

        public bool SetQuadrangleApplicantDeceaseds(int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null)
        {
            if (applicantId != null)
                _quadrangle.SetApplicant((int)applicantId);

            if (deceased1Id != null)
            {
                _deceased.SetDeceased((int)deceased1Id);
                if (_deceased.SetQuadrangle(_quadrangle.GetQuadrangle().Id))
                {
                    _quadrangle.SetHasDeceased(true);
                }
                else
                    return false;
            }

            if (deceased2Id != null)
            {
                _deceased.SetDeceased((int)deceased2Id);
                if (_deceased.SetQuadrangle(_quadrangle.GetQuadrangle().Id))
                {
                    _quadrangle.SetHasDeceased(true);
                }
                else
                    return false;
            }

            return true;
        }

        public bool RollbackQuadrangleApplicantDeceaseds(string quadrangleTransactionAF, int quadrangleId)
        {
            _quadrangle.SetQuadrangle(quadrangleId);
            ClearQuadrangleApplicantAndDeceased();

            if (!_tracking.IsLatestTransaction(quadrangleId, quadrangleTransactionAF))
                return false;

            var trackings = _tracking.GetTrackingByTransactionAF(quadrangleTransactionAF);

            if (trackings.Count() > 1)
            {
                //Shifted
                _quadrangle.SetQuadrangle(trackings.ElementAt(1).QuadrangleId);
                ClearQuadrangleApplicantAndDeceased();
                SetQuadrangleApplicantDeceaseds(trackings.ElementAt(0).ApplicantId, trackings.ElementAt(0).Deceased1Id, trackings.ElementAt(0).Deceased2Id);
            }

            if (trackings.Count() == 1)
            {
                var trackingsByQuadrangleId = _tracking.GetTrackingByQuadrangleId(quadrangleId);
                SetQuadrangleApplicantDeceaseds(trackingsByQuadrangleId.ElementAt(1).ApplicantId, trackingsByQuadrangleId.ElementAt(1).Deceased1Id, trackingsByQuadrangleId.ElementAt(1).Deceased2Id);
            }

            _tracking.Delete(quadrangleTransactionAF);

            return true;
        }
    }
}