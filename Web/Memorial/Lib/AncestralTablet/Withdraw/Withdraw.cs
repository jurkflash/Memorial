using Memorial.Core;
using System;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.AncestralTablet
{
    public class Withdraw : Transaction, IWithdraw
    {
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

        public bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            ancestralTabletTransaction.AF = _number.GetNewAF(ancestralTabletTransaction.AncestralTabletItemId, System.DateTime.Now.Year);

            var trns = GetTransactionsByAncestralTabletId(ancestralTabletTransaction.AncestralTabletId);

            if (trns.Count() == 0)
                return false;

            var trnsAF = string.Join(",", trns.Select(t => t.AF));
            foreach (var trn in trns)
            {
                _unitOfWork.AncestralTabletTransactions.Remove(trn);
            }
            ancestralTabletTransaction.WithdrewAFS = trnsAF;

            var trackingTrns = _tracking.GetByAncestralTabletId(ancestralTabletTransaction.AncestralTabletId);
            foreach (var trackingTrn in trackingTrns)
            {
                trackingTrn.ToDeleteFlag = true;
            }

            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletTransaction.AncestralTabletId);
            if (ancestralTabletTransaction.DeceasedId != null)
            {
                var deceased = _deceased.GetById((int)ancestralTabletTransaction.DeceasedId);
                deceased.AncestralTablet = null;
                deceased.AncestralTabletId = null;
                ancestralTablet.hasDeceased = false;
            }

            ancestralTabletTransaction.WithdrewAncestralTabletApplicantId = (int)ancestralTablet.ApplicantId;
            ancestralTablet.Applicant = null;
            ancestralTablet.ApplicantId = null;

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveAncestralTabletAF(ancestralTabletTransaction.AF).ToList();

            if (invoices.Any() && ancestralTabletTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            var ancestralTabletTransactionInDb = GetByAF(ancestralTabletTransaction.AF);
            ancestralTabletTransactionInDb.Price = ancestralTabletTransaction.Price;
            ancestralTabletTransactionInDb.SummaryItem = ancestralTabletTransaction.SummaryItem;
            ancestralTabletTransactionInDb.Remark = ancestralTabletTransaction.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveAncestralTabletAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByAncestralTabletAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.AncestralTabletTransactions.GetByAF(AF);
            var ancestralTablet = _ancestralTablet.GetById(transactionInDb.AncestralTabletId);

            var AFs = transactionInDb.WithdrewAFS.Split(',');

            foreach (var af in AFs)
            {
                GetByAF(af).DeletedUtcTime = null;
            }

            if (transactionInDb.DeceasedId != null)
            {
                var deceased = _deceased.GetById((int)transactionInDb.DeceasedId);
                deceased.AncestralTabletId = transactionInDb.AncestralTabletId;
                ancestralTablet.hasDeceased = true;
            }

            ancestralTablet.ApplicantId = (int)_transaction.WithdrewAncestralTabletApplicantId;
            var trackings = _tracking.GetByAncestralTabletId(_transaction.AncestralTabletId, true);

            foreach (var tracking in trackings)
            {
                tracking.ToDeleteFlag = false;
            }

            _unitOfWork.AncestralTabletTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

        public bool RemoveWithdrew(int ancestralTabletId)
        {
            var trans = GetTransactionsByAncestralTabletId(ancestralTabletId);

            if (trans.Count() != 1)
            {
                return false;
            }

            trans.ElementAt(0).DeletedUtcTime = DateTime.UtcNow;

            var trackings = _tracking.GetByAncestralTabletId(_transaction.AncestralTabletId, true);

            foreach (var tracking in trackings)
            {
                _tracking.Remove(tracking.AncestralTabletId, tracking.AncestralTabletTransactionAF);
            }

            return true;
        }
    }
}