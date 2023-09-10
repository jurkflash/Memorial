using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.AncestralTablet
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;
        private readonly IWithdraw _withdraw;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestralTablet ancestralTablet,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking,
            IWithdraw withdraw
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
            _withdraw = withdraw;
        }

        public bool Add(Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            if (ancestralTabletTransaction.DeceasedId != null)
            {
                var deceased = _deceased.GetById((int)ancestralTabletTransaction.DeceasedId);
                if (deceased.AncestralTabletId != null)
                    return false;
            }

            ancestralTabletTransaction.AF = _number.GetNewAF(ancestralTabletTransaction.AncestralTabletItemId, System.DateTime.Now.Year);
            SummaryItem(ancestralTabletTransaction);

            _unitOfWork.AncestralTabletTransactions.Add(ancestralTabletTransaction);

            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletTransaction.AncestralTabletId);
            ancestralTablet.ApplicantId = ancestralTabletTransaction.ApplicantId;

            if (ancestralTabletTransaction.DeceasedId != null)
            {
                var deceased = _deceased.GetById((int)ancestralTabletTransaction.DeceasedId);
                deceased.AncestralTabletId = ancestralTabletTransaction.AncestralTabletId;
                ancestralTablet.hasDeceased = true;
            }

            _tracking.Add(ancestralTabletTransaction.AncestralTabletId, ancestralTabletTransaction.AF, ancestralTabletTransaction.ApplicantId, ancestralTabletTransaction.DeceasedId);

            _withdraw.RemoveWithdrew(ancestralTabletTransaction.AncestralTabletId);

            _unitOfWork.Complete();
            
            return true;
        }

        public bool Remove(string AF)
        {
            var transactionInDb = _unitOfWork.AncestralTabletTransactions.GetByAF(AF);

            if (!_tracking.IsLatestTransaction(transactionInDb.AncestralTabletId, transactionInDb.AF))
                return false;

            if (_unitOfWork.Invoices.GetByActiveAncestralTabletAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByAncestralTabletAF(AF).Any())
                return false;

            var ancestralTablet = _ancestralTablet.GetById(_transaction.AncestralTabletId);
            var deceaseds = _deceased.GetByAncestralTabletId(_transaction.AncestralTabletId);

            foreach (var deceased in deceaseds)
            {
                deceased.AncestralTablet = null;
                deceased.AncestralTabletId = null;

            }

            ancestralTablet.hasDeceased = false;
            ancestralTablet.Applicant = null;
            ancestralTablet.ApplicantId = null;

            _tracking.Delete(_transaction.AF);

            _unitOfWork.AncestralTabletTransactions.Remove(transactionInDb);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.AncestralTabletTransaction ancestralTabletTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveAncestralTabletAF(ancestralTabletTransaction.AF).ToList();

            if (invoices.Any() && ancestralTabletTransaction.Price + (float)ancestralTabletTransaction.Maintenance < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(ancestralTabletTransaction);

            var ancestralTabletTransactionInDb = GetByAF(ancestralTabletTransaction.AF);
            ancestralTabletTransactionInDb.Price = ancestralTabletTransaction.Price;
            ancestralTabletTransactionInDb.SummaryItem = ancestralTabletTransaction.SummaryItem;
            ancestralTabletTransactionInDb.Remark = ancestralTabletTransaction.Remark;

            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletTransaction.AncestralTabletId);
            if (ancestralTabletTransaction.DeceasedId != ancestralTabletTransactionInDb.DeceasedId)
            {
                if (ancestralTabletTransaction.DeceasedId == null)
                {
                    var deceased = _deceased.GetById((int)ancestralTabletTransactionInDb.DeceasedId);
                    deceased.AncestralTablet = null;
                    deceased.AncestralTabletId = null;
                }
                else
                {
                    var deceased = _deceased.GetById((int)ancestralTabletTransaction.DeceasedId);
                    if (deceased.AncestralTabletId != null && deceased.AncestralTabletId != ancestralTablet.Id)
                    {
                        return false;
                    }
                    else
                    {
                        deceased.AncestralTabletId = ancestralTablet.Id;
                        ancestralTablet.hasDeceased = true;
                    }
                }
            }


            if (ancestralTabletTransaction.DeceasedId == null)
                ancestralTablet.hasDeceased = false;

            _tracking.Change(ancestralTabletTransaction.AncestralTabletId, ancestralTabletTransaction.AF, ancestralTabletTransaction.ApplicantId, ancestralTabletTransaction.DeceasedId);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(AncestralTabletTransaction ancestralTabletTransaction)
        {
            var ancestralTablet = _ancestralTablet.GetById(ancestralTabletTransaction.AncestralTabletId);

            ancestralTabletTransaction.SummaryItem = "AF: " + ancestralTabletTransaction.AF + "<BR/>" +
                Resources.Mix.AncestralTablet + ": " + ancestralTablet.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + ancestralTabletTransaction.Remark;
        }
    }
}