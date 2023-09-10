using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.Space
{
    public class House : Transaction, IHouse
    {
        private readonly IUnitOfWork _unitOfWork;

        public House(
            IUnitOfWork unitOfWork,
            IItem item,
            ISpace space,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            ) : 
            base(
                unitOfWork, 
                item,
                space, 
                applicant, 
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _space = space;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public bool Add(Core.Domain.SpaceTransaction spaceTransaction)
        {
            spaceTransaction.AF = _number.GetNewAF(spaceTransaction.SpaceItemId, System.DateTime.Now.Year);

            SummaryItem(spaceTransaction);

            _unitOfWork.SpaceTransactions.Add(spaceTransaction);
            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.SpaceTransaction spaceTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveSpaceAF(spaceTransaction.AF).ToList();

            if (invoices.Any() && spaceTransaction.Amount < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(spaceTransaction);

            var spaceTransactionInDb = GetByAF(spaceTransaction.AF);
            spaceTransactionInDb.Amount = spaceTransaction.Amount;
            spaceTransactionInDb.SummaryItem = spaceTransaction.SummaryItem;
            spaceTransactionInDb.Remark = spaceTransaction.Remark;
            spaceTransactionInDb.FuneralCompanyId = spaceTransaction.FuneralCompanyId;
            spaceTransactionInDb.FromDate = spaceTransaction.FromDate;
            spaceTransactionInDb.ToDate = spaceTransaction.ToDate;
            spaceTransactionInDb.BasePrice = spaceTransaction.BasePrice;
            spaceTransactionInDb.OtherCharges = spaceTransaction.OtherCharges;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveSpaceAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetBySpaceAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.SpaceTransactions.GetByAF(AF);
            _unitOfWork.SpaceTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(SpaceTransaction spaceTransaction)
        {
            spaceTransaction.SummaryItem = "AF: " +spaceTransaction.AF + "<BR/>" +
                Resources.Mix.Remark + ": " + spaceTransaction.Remark;
        }

    }
}