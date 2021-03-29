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
    public class AncestorApplicantDeceaseds : IAncestorApplicantDeceaseds
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;
        private readonly IDeceased _deceased;
        private readonly IAncestor _ancestor;

        public AncestorApplicantDeceaseds(
            IUnitOfWork unitOfWork,
            ITracking tracking,
            IDeceased deceased,
            IAncestor ancestor)
        {
            _unitOfWork = unitOfWork;
            _tracking = tracking;
            _deceased = deceased;
            _ancestor = ancestor;
        }

        public void ClearAncestorApplicantAndDeceased()
        {
            var deceaseds = _deceased.GetDeceasedsByAncestorId(_ancestor.GetAncestor().Id);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveAncestor();
            }

            _ancestor.SetHasDeceased(false);

            _ancestor.RemoveApplicant();
        }

        public bool SetAncestorApplicantDeceaseds(int? applicantId = null, int? deceasedId = null)
        {
            if (applicantId != null)
                _ancestor.SetApplicant((int)applicantId);

            if (deceasedId != null)
            {
                _deceased.SetDeceased((int)deceasedId);
                if (_deceased.SetAncestor(_ancestor.GetAncestor().Id))
                {
                    _ancestor.SetHasDeceased(true);
                }
                else
                    return false;
            }

            return true;
        }

        public bool RollbackAncestorApplicantDeceaseds(string ancestorTransactionAF, int ancestorId)
        {
            _ancestor.SetAncestor(ancestorId);
            ClearAncestorApplicantAndDeceased();

            if (!_tracking.IsLatestTransaction(ancestorId, ancestorTransactionAF))
                return false;

            var trackings = _tracking.GetTrackingByTransactionAF(ancestorTransactionAF);

            if (trackings.Count() > 1)
            {
                //Shifted
                _ancestor.SetAncestor(trackings.ElementAt(1).AncestorId);
                ClearAncestorApplicantAndDeceased();
                SetAncestorApplicantDeceaseds(trackings.ElementAt(0).ApplicantId, trackings.ElementAt(0).DeceasedId);

                _tracking.Delete(ancestorTransactionAF);
            }

            return true;
        }
    }
}