using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Cemetery
{
    public class SecondBurial : Transaction, ISecondBurial
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public SecondBurial(
            IUnitOfWork unitOfWork,
            IItem item,
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking
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
            _tracking = tracking;
        }

        public bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            if (cemeteryTransaction.Deceased1Id != null)
            {
                if (_deceased.GetById((int)cemeteryTransaction.Deceased1Id).PlotId != null)
                    return false;
            }

            cemeteryTransaction.AF = _number.GetNewAF(cemeteryTransaction.CemeteryItemId, System.DateTime.Now.Year);

            SummaryItem(cemeteryTransaction);

            _unitOfWork.CemeteryTransactions.Add(cemeteryTransaction);

            var plot = _plot.GetById(cemeteryTransaction.PlotId);

            if (cemeteryTransaction.Deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)cemeteryTransaction.Deceased1Id);
                deceased.PlotId = cemeteryTransaction.PlotId;
            }

            _tracking.AddDeceased(cemeteryTransaction.PlotId, (int)cemeteryTransaction.Deceased1Id);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveCemeteryAF(cemeteryTransaction.AF).ToList();

            if (invoices.Any() && cemeteryTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            var deceased1InDb = cemeteryTransaction.Deceased1Id;

            SummaryItem(cemeteryTransaction);

            var cemeteryTransactionInDb = GetByAF(cemeteryTransaction.AF);

            cemeteryTransactionInDb.Price = cemeteryTransaction.Price;
            cemeteryTransactionInDb.SummaryItem = cemeteryTransaction.SummaryItem;
            cemeteryTransactionInDb.Remark = cemeteryTransaction.Remark;

            var plot = _plot.GetById(cemeteryTransaction.PlotId);

            PlotApplicantDeceaseds(plot, cemeteryTransaction.Deceased1Id, deceased1InDb);

            _tracking.ChangeDeceased(cemeteryTransaction.PlotId, cemeteryTransaction.AF, (int)deceased1InDb, (int)cemeteryTransaction.Deceased1Id);

            _unitOfWork.Complete();
            return true;
        }

        private bool PlotApplicantDeceaseds(Core.Domain.Plot plot, int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    var dbDeceased = _deceased.GetById((int)dbDeceasedId);
                    dbDeceased.Plot = null;
                    dbDeceased.PlotId = null;

                    return true;
                }

                var deceased = _deceased.GetById((int)deceasedId);
                if (deceased.PlotId != null && deceased.PlotId != plot.Id)
                {
                    return false;
                }

                if(dbDeceasedId != null)
                {
                    var dbDeceased = _deceased.GetById((int)dbDeceasedId);
                    dbDeceased.Plot = null;
                    dbDeceased.PlotId = null;
                }

                deceased.PlotId = plot.Id;
                plot.hasDeceased = true;

            }

            return true;
        }

        public bool Remove(string AF)
        {
            var transactionInDb = _unitOfWork.CemeteryTransactions.GetByAF(AF);
            var plot = _plot.GetById(transactionInDb.PlotId);

            if (_unitOfWork.Invoices.GetByActiveCemeteryAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByCemeteryAF(AF).Any())
                return false;

            if (transactionInDb.Deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)transactionInDb.Deceased1Id);
                deceased.Plot = null;
                deceased.PlotId = null;
            }

            _tracking.RemoveDeceased(transactionInDb.PlotId, transactionInDb.AF, (int)transactionInDb.Deceased1Id);

            _unitOfWork.CemeteryTransactions.Remove(transactionInDb);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            cemeteryTransaction.SummaryItem = "AF: " + cemeteryTransaction.AF + "<BR/>" +
                Resources.Mix.Plot + ": " + cemeteryTransaction.Plot.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + cemeteryTransaction.Remark;
        }

    }
}