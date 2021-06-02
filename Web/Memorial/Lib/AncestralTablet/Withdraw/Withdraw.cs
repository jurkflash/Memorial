using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public class Withdraw : Transaction, IWithdraw
    {
        private const string _systemCode = "Withrdaw";
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public Withdraw(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestralTablet ancestralTablet,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking
            ) :
            base(
                unitOfWork,
                item,
                ancestralTablet,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestralTablet = ancestralTablet;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _tracking = tracking;
        }

        public void SetWithrdaw(string AF)
        {
            SetTransaction(AF);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            NewNumber(ancestralTabletTransactionDto.AncestralTabletItemDtoId);

            var trns = GetTransactionsByAncestralTabletId(ancestralTabletTransactionDto.AncestralTabletDtoId);

            if (trns.Count() == 0)
                return false;

            var trnsAF = string.Join(",", trns.Select(t => t.AF));
            foreach (var trn in trns)
            {
                trn.DeleteDate = DateTime.Now;
            }
            ancestralTabletTransactionDto.WithdrewAFS = trnsAF;


            var trackingTrns = _tracking.GetTrackingByAncestralTabletId(ancestralTabletTransactionDto.AncestralTabletDtoId);
            foreach (var trackingTrn in trackingTrns)
            {
                trackingTrn.ToDeleteFlag = true;
            }
            

            _ancestralTablet.SetAncestralTablet(ancestralTabletTransactionDto.AncestralTabletDtoId);
            if (ancestralTabletTransactionDto.DeceasedDtoId != null)
            {
                _deceased.SetDeceased((int)ancestralTabletTransactionDto.DeceasedDtoId);
                _deceased.RemoveAncestralTablet();
                _ancestralTablet.SetHasDeceased(false);
            }

            
            ancestralTabletTransactionDto.WithdrewAncestralTabletApplicantId = (int)_ancestralTablet.GetApplicantId();
            _ancestralTablet.RemoveApplicant();


            if (CreateNewTransaction(ancestralTabletTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            UpdateTransaction(ancestralTabletTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            var AFs = _transaction.WithdrewAFS.Split(',');

            foreach(var AF in AFs)
            {
                GetTransaction(AF).DeleteDate = null;
            }

            _ancestralTablet.SetAncestralTablet(_transaction.AncestralTabletId);
            if (_transaction.DeceasedId != null)
            {
                _deceased.SetDeceased((int)_transaction.DeceasedId);
                _deceased.SetAncestralTablet(_transaction.AncestralTabletId);
                _ancestralTablet.SetHasDeceased(true);
            }


            _ancestralTablet.SetApplicant((int)_transaction.WithdrewAncestralTabletApplicantId);
            

            var trackings = _tracking.GetTrackingByAncestralTabletId(_transaction.AncestralTabletId, true);

            foreach(var tracking in trackings)
            {
                tracking.ToDeleteFlag = false;
            }

            DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        public bool RemoveWithdrew(int ancestralTabletId)
        {
            var trans = GetTransactionsByAncestralTabletId(ancestralTabletId);

            if(trans.Count() != 1)
            {
                return false;
            }

            trans.ElementAt(0).DeleteDate = DateTime.Now;

            var trackings = _tracking.GetTrackingByAncestralTabletId(_transaction.AncestralTabletId, true);

            foreach (var tracking in trackings)
            {
                _tracking.Remove(tracking.AncestralTabletId, tracking.AncestralTabletTransactionAF);
            }

            return true;
        }
    }
}