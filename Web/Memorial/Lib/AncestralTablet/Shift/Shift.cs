using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.AncestralTablet
{
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;
        private readonly IMaintenance _maintenance;

        public Shift(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestralTablet ancestralTablet,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking,
            IMaintenance maintenance
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
            _maintenance = maintenance;
        }

        private bool ShiftAncestralTabletApplicantDeceaseds(int oldQuadranlgeId, int newAncestralTabletId, int newApplicantId)
        {
            var oldAncestralTablet = _ancestralTablet.GetById(oldQuadranlgeId);
            var deceaseds = _deceased.GetByAncestralTabletId(oldQuadranlgeId);

            foreach (var deceased in deceaseds)
            {
                deceased.AncestralTabletId = newAncestralTabletId;
            }

            if (deceaseds.Any())
            {
                oldAncestralTablet.hasDeceased = false;
            }

            oldAncestralTablet.Applicant = null;
            oldAncestralTablet.ApplicantId = null;

            var newAncestralTablet = _ancestralTablet.GetById(newAncestralTabletId);
            newAncestralTablet.ApplicantId = newApplicantId;
            newAncestralTablet.hasDeceased = deceaseds.Any();

            return true;
        }

        public bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            var ancestralTablet = _ancestralTablet.GetById((int)ancestralTabletTransaction.ShiftedAncestralTabletId);
            if (ancestralTablet.ApplicantId != null)
                return false;

            if (!SetTransactionDeceasedIdBasedOnAncestralTablet(ancestralTabletTransaction, (int)ancestralTabletTransaction.ShiftedAncestralTabletId))
                return false;

            ancestralTabletTransaction.ShiftedAncestralTabletTransactionAF = _tracking.GetLatestFirstTransactionByAncestralTabletId((int)ancestralTabletTransaction.ShiftedAncestralTabletId).AncestralTabletTransactionAF;

            GetByAF(ancestralTabletTransaction.ShiftedAncestralTabletTransactionAF).DeletedUtcTime = System.DateTime.UtcNow;

            _tracking.Remove((int)ancestralTabletTransaction.ShiftedAncestralTabletId, ancestralTabletTransaction.ShiftedAncestralTabletTransactionAF);

            ancestralTabletTransaction.AF = _number.GetNewAF(ancestralTabletTransaction.AncestralTabletItemId, System.DateTime.Now.Year);

            SummaryItem(ancestralTabletTransaction);

            _unitOfWork.AncestralTabletTransactions.Add(ancestralTabletTransaction);

            ShiftAncestralTabletApplicantDeceaseds(ancestralTabletTransaction.AncestralTabletId, (int)ancestralTabletTransaction.ShiftedAncestralTabletId, ancestralTabletTransaction.ApplicantId);

            _maintenance.ChangeAncestralTablet((int)ancestralTabletTransaction.ShiftedAncestralTabletId, ancestralTabletTransaction.AncestralTabletId);

            _tracking.Add(ancestralTabletTransaction.AncestralTabletId, ancestralTabletTransaction.AF, ancestralTabletTransaction.ApplicantId, ancestralTabletTransaction.DeceasedId);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, AncestralTabletTransaction ancestralTabletTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveAncestralTabletAF(ancestralTabletTransaction.AF).ToList();

            if (invoices.Any() && ancestralTabletTransaction.Price + (float)ancestralTabletTransaction.Maintenance < invoices.Max(i => i.Amount))
                return false;

            var ancestralTabletTransactionInDb = GetByAF(ancestralTabletTransaction.AF);

            if (ancestralTabletTransactionInDb.ShiftedAncestralTabletId != ancestralTabletTransaction.ShiftedAncestralTabletId)
            {
                if (!SetTransactionDeceasedIdBasedOnAncestralTablet(ancestralTabletTransaction, ancestralTabletTransactionInDb.AncestralTabletId))
                    return false;

                _tracking.Remove(ancestralTabletTransactionInDb.AncestralTabletId, ancestralTabletTransaction.AF);

                _tracking.Add(ancestralTabletTransaction.AncestralTabletId, ancestralTabletTransaction.AF, ancestralTabletTransaction.ApplicantId, ancestralTabletTransaction.DeceasedId);

                ShiftAncestralTabletApplicantDeceaseds(ancestralTabletTransactionInDb.AncestralTabletId, ancestralTabletTransaction.AncestralTabletId, ancestralTabletTransaction.ApplicantId);

                _maintenance.ChangeAncestralTablet(ancestralTabletTransactionInDb.AncestralTabletId, ancestralTabletTransaction.AncestralTabletId);

                SummaryItem(ancestralTabletTransaction);

                ancestralTabletTransactionInDb.Price = ancestralTabletTransaction.Price;
                ancestralTabletTransactionInDb.SummaryItem = ancestralTabletTransaction.SummaryItem;
                ancestralTabletTransactionInDb.Remark = ancestralTabletTransaction.Remark;

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Remove(string AF)
        {
            var transactionInDb = _unitOfWork.AncestralTabletTransactions.GetByAF(AF);

            if (GetTransactionsByShiftedAncestralTabletTransactionAF(AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction((int)transactionInDb.ShiftedAncestralTabletId, AF))
                return false;

            var shiftedAncestralTablet = _ancestralTablet.GetById((int)transactionInDb.ShiftedAncestralTabletId);
            if (shiftedAncestralTablet.ApplicantId != null)
                return false;

            _unitOfWork.AncestralTabletTransactions.Remove(transactionInDb);

            var ancestralTablet = _ancestralTablet.GetById(transactionInDb.AncestralTabletId);
            ancestralTablet.Applicant = null;
            ancestralTablet.ApplicantId = null;

            ancestralTablet.hasDeceased = false;

            var deceaseds = _deceased.GetByAncestralTabletId(transactionInDb.AncestralTabletId);

            foreach (var deceased in deceaseds)
            {
                deceased.Niche = null;
                deceased.NicheId = null;
            }

            _tracking.Remove(transactionInDb.AncestralTabletId, AF);


            var previousTransaction = GetExclusive(transactionInDb.ShiftedAncestralTabletTransactionAF);

            var previousAncestralTablet = _ancestralTablet.GetById(previousTransaction.AncestralTabletId);
            previousAncestralTablet.ApplicantId = previousTransaction.ApplicantId;

            if (previousTransaction.DeceasedId != null)
            {
                var deceased = _deceased.GetById((int)previousTransaction.DeceasedId);
                if (deceased.AncestralTabletId != null && deceased.AncestralTabletId != transactionInDb.AncestralTabletId)
                    return false;

                deceased.AncestralTabletId = previousTransaction.AncestralTabletId;
                previousAncestralTablet.hasDeceased = true;
            }

            previousTransaction.DeletedUtcTime = null;

            _tracking.Add(previousTransaction.AncestralTabletId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.DeceasedId);

            _maintenance.ChangeAncestralTablet(transactionInDb.AncestralTabletId, previousTransaction.AncestralTabletId);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(AncestralTabletTransaction ancestralTabletTransaction)
        {
            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletTransaction.AncestralTabletId);

            ancestralTabletTransaction.SummaryItem = "AF: " + ancestralTabletTransaction.AF + "<BR/>" +
                Resources.Mix.AncestralTablet + ": " + _ancestralTablet.GetById((int)ancestralTabletTransaction.ShiftedAncestralTabletId).Name + "<BR/>" +
                Resources.Mix.ShiftTo + "<BR/>" +
                Resources.Mix.AncestralTablet + ": " + ancestralTablet.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + ancestralTabletTransaction.Remark;
        }
    }
}