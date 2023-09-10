using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.Cemetery
{
    public class Clearance : Transaction, IClearance
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public Clearance(
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
            cemeteryTransaction.AF = _number.GetNewAF(cemeteryTransaction.CemeteryItemId, System.DateTime.Now.Year);

            var plot = _plot.GetById(cemeteryTransaction.PlotId);
            cemeteryTransaction.ClearedApplicantId = plot.ApplicantId;

            var deceaseds = _deceased.GetByPlotId(cemeteryTransaction.PlotId);

            if (deceaseds.Count() > 0)
            {
                cemeteryTransaction.Deceased1Id = deceaseds.ElementAt(0).Id;
            }

            if (deceaseds.Count() > 1)
            {
                cemeteryTransaction.Deceased2Id = deceaseds.ElementAt(1).Id;
            }

            if (deceaseds.Count() > 2)
            {
                cemeteryTransaction.Deceased3Id = deceaseds.ElementAt(2).Id;
            }

            SummaryItem(cemeteryTransaction);

            _unitOfWork.CemeteryTransactions.Add(cemeteryTransaction);

            foreach (var deceased in deceaseds)
            {
                deceased.Plot = null;
                deceased.PlotId = null;
            }

            plot.hasDeceased = false;

            plot.Applicant = null;
            plot.ApplicantId = null;

            plot.hasCleared = true;

            _tracking.Add(cemeteryTransaction.PlotId, cemeteryTransaction.AF, cemeteryTransaction.ApplicantId);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveCemeteryAF(cemeteryTransaction.AF).ToList();

            if (invoices.Any() && cemeteryTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(cemeteryTransaction);

            var cemeteryTransactionInDb = GetByAF(cemeteryTransaction.AF);
            cemeteryTransactionInDb.Price = cemeteryTransaction.Price;
            cemeteryTransactionInDb.SummaryItem = cemeteryTransaction.SummaryItem;
            cemeteryTransactionInDb.Remark = cemeteryTransaction.Remark;

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            var transactionInDb = _unitOfWork.CemeteryTransactions.GetByAF(AF);

            if (!_tracking.IsLatestTransaction(transactionInDb.PlotId, transactionInDb.AF))
                return false;

            if (_unitOfWork.Invoices.GetByActiveCemeteryAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByCemeteryAF(AF).Any())
                return false;

            var plot = _plot.GetById(transactionInDb.PlotId);

            var lastTransactionOfPlot = GetLastCemeteryTransactionTransactionByPlotId(transactionInDb.PlotId);

            _unitOfWork.CemeteryTransactions.Remove(transactionInDb);

            if (lastTransactionOfPlot.AF == transactionInDb.AF)
            {
                plot.ApplicantId = lastTransactionOfPlot.ApplicantId;
                plot.hasDeceased = true;
                plot.hasCleared = false;

                var deceased = _deceased.GetById((int)transactionInDb.Deceased1Id);
                deceased.PlotId = transactionInDb.PlotId;

                if (lastTransactionOfPlot.Deceased2Id != null)
                {
                    var deceased2 = _deceased.GetById((int)transactionInDb.Deceased2Id);
                    deceased2.PlotId = transactionInDb.PlotId;
                }

                if (lastTransactionOfPlot.Deceased3Id != null)
                {
                    var deceased3 = _deceased.GetById((int)transactionInDb.Deceased3Id);
                    deceased3.PlotId = transactionInDb.PlotId;
                }
            }

            _tracking.Remove(transactionInDb.PlotId, transactionInDb.AF);

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