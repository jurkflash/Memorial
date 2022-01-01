using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public class Withdraw : Transaction, IWithdraw
    {
        private const string _systemCode = "Withrdaw";
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public Withdraw(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking
            ) :
            base(
                unitOfWork,
                item,
                niche,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _niche = niche;
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

        public bool Create(ColumbariumTransactionDto columbariumTransactionDto)
        {
            NewNumber(columbariumTransactionDto.ColumbariumItemDtoId);

            var trns = GetTransactionsByNicheId(columbariumTransactionDto.NicheDtoId);

            if (trns.Count() == 0)
                return false;

            var trnsAF = string.Join(",", trns.Select(t => t.AF));
            foreach (var trn in trns)
            {
                trn.DeletedDate = DateTime.Now;
            }
            columbariumTransactionDto.WithdrewAFS = trnsAF;


            var trackingTrns = _tracking.GetTrackingByNicheId(columbariumTransactionDto.NicheDtoId);
            foreach (var trackingTrn in trackingTrns)
            {
                trackingTrn.ToDeleteFlag = true;
            }
            

            _niche.SetNiche(columbariumTransactionDto.NicheDtoId);
            if (columbariumTransactionDto.DeceasedDto1Id != null)
            {
                _deceased.SetDeceased((int)columbariumTransactionDto.DeceasedDto1Id);
                _deceased.RemoveAncestralTablet();
                _niche.SetHasDeceased(false);
            }

            if (columbariumTransactionDto.DeceasedDto2Id != null)
            {
                _deceased.SetDeceased((int)columbariumTransactionDto.DeceasedDto2Id);
                _deceased.RemoveAncestralTablet();
                _niche.SetHasDeceased(false);
            }


            columbariumTransactionDto.WithdrewAncestralTabletApplicantId = (int)_niche.GetApplicantId();
            _niche.RemoveApplicant();


            if (CreateNewTransaction(columbariumTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(ColumbariumTransactionDto columbariumTransactionDto)
        {
            UpdateTransaction(columbariumTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            var AFs = _transaction.WithdrewAFS.Split(',');

            foreach(var AF in AFs)
            {
                GetTransaction(AF).DeletedDate = null;
            }

            _niche.SetNiche(_transaction.NicheId);
            if (_transaction.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)_transaction.Deceased1Id);
                _deceased.SetAncestralTablet(_transaction.NicheId);
                _niche.SetHasDeceased(true);
            }

            if (_transaction.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)_transaction.Deceased2Id);
                _deceased.SetAncestralTablet(_transaction.NicheId);
                _niche.SetHasDeceased(true);
            }


            _niche.SetApplicant((int)_transaction.WithdrewColumbariumApplicantId);
            

            var trackings = _tracking.GetTrackingByNicheId(_transaction.NicheId, true);

            foreach(var tracking in trackings)
            {
                tracking.ToDeleteFlag = false;
            }

            DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        public bool RemoveWithdrew(int nicheId)
        {
            var trans = GetTransactionsByNicheId(nicheId);

            if(trans.Count() != 1)
            {
                return false;
            }

            trans.ElementAt(0).DeletedDate = DateTime.Now;

            var trackings = _tracking.GetTrackingByNicheId(_transaction.NicheId, true);

            foreach (var tracking in trackings)
            {
                _tracking.Remove(tracking.NicheId, tracking.ColumbariumTransactionAF);
            }

            return true;
        }
    }
}