using Memorial.Core;
using System;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.Space
{
    public class Booking : Transaction, IBooking
    {
        private readonly IUnitOfWork _unitOfWork;

        public Booking(
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

        public bool IsAvailable(int itemId, DateTime from, DateTime to)
        {
            if (_space.CheckAvailability(from, to, itemId))
                return true;
            return false;
        }

        public bool IsAvailable(string AF, DateTime from, DateTime to)
        {
            if (_space.CheckAvailability(from, to, AF))
                return true;
            return false;
        }

        public bool Add(SpaceTransaction spaceTransaction)
        {
            if (!IsAvailable(spaceTransaction.SpaceItemId, (DateTime)spaceTransaction.FromDate, (DateTime)spaceTransaction.ToDate))
            {
                return false;
            }

            spaceTransaction.AF = _number.GetNewAF(spaceTransaction.SpaceItemId, System.DateTime.Now.Year);

            SummaryItem(spaceTransaction);

            _unitOfWork.SpaceTransactions.Add(spaceTransaction);

            _unitOfWork.Complete();
            
            return true;
        }

        public bool Change(string AF, SpaceTransaction spaceTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveSpaceAF(spaceTransaction.AF).ToList();

            if (invoices.Any() && spaceTransaction.Amount < invoices.Max(i => i.Amount))
                return false;

            if (!IsAvailable(spaceTransaction.AF, (DateTime)spaceTransaction.FromDate, (DateTime)spaceTransaction.ToDate))
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
            var item = _item.GetById(spaceTransaction.SpaceItemId);
            spaceTransaction.SummaryItem = "AF: " + spaceTransaction.AF + "<BR/>" +
                Resources.Mix.Space + ": " + item.Space.Name +"<BR/>"+ 
                Resources.Mix.From + ": " + spaceTransaction.FromDate.Value.ToString("yyyy-MMM-dd HH:mm") + " " + Resources.Mix.To + ": " + spaceTransaction.ToDate.Value.ToString("yyyy-MMM-dd HH:mm") + "<BR/>"+
                Resources.Mix.Remark + ": " + spaceTransaction.Remark;
        }

    }
}