using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Cemetery
{
    public class Reciprocate : Transaction, IReciprocate
    {
        private readonly IUnitOfWork _unitOfWork;

        public Reciprocate(
            IUnitOfWork unitOfWork,
            IItem item,
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            ) :
            base(
                unitOfWork,
                item,
                plot,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _plot = plot;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            cemeteryTransaction.AF = _number.GetNewAF(cemeteryTransaction.CemeteryItemId, System.DateTime.Now.Year);

            var plot = _plot.GetById(cemeteryTransaction.PlotId);
            cemeteryTransaction.ApplicantId = (int)plot.ApplicantId;

            _unitOfWork.CemeteryTransactions.Add(cemeteryTransaction);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveCemeteryAF(cemeteryTransaction.AF).ToList();

            if (invoices.Any() && cemeteryTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            var cemeteryTransactionInDb = GetByAF(cemeteryTransaction.AF);
            cemeteryTransactionInDb.Price = cemeteryTransaction.Price;
            cemeteryTransactionInDb.SummaryItem = cemeteryTransaction.SummaryItem;
            cemeteryTransactionInDb.Remark = cemeteryTransaction.Remark;

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveCemeteryAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByCemeteryAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.CemeteryTransactions.GetByAF(AF);
            _unitOfWork.CemeteryTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

    }
}