using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Cemetery
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IPlot _invoice;
        private readonly ITracking _tracking;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IPlot invoice,
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
            _invoice = invoice;
            _tracking = tracking;
        }

        public bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            if (cemeteryTransaction.Deceased1Id != null)
            {
                return false;
            }

            cemeteryTransaction.AF = _number.GetNewAF(cemeteryTransaction.CemeteryItemId, System.DateTime.Now.Year);

            SummaryItem(cemeteryTransaction);

            _unitOfWork.CemeteryTransactions.Add(cemeteryTransaction);

            var plot = _plot.GetById(cemeteryTransaction.PlotId);
            plot.ApplicantId = cemeteryTransaction.ApplicantId;

            if (cemeteryTransaction.Deceased1Id != null)
            {
                var d = _deceased.GetById((int)cemeteryTransaction.Deceased1Id);
                d.PlotId = cemeteryTransaction.PlotId;
                plot.hasDeceased = true;
            }

            _tracking.Add(cemeteryTransaction.PlotId, cemeteryTransaction.AF, cemeteryTransaction.ApplicantId, cemeteryTransaction.Deceased1Id);

            _unitOfWork.Complete();
            
            return true;
        }

        public bool Change(string AF, Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            if (_invoice.GetByAF(cemeteryTransaction.AF).Any() && GetTotalAmount(cemeteryTransaction) <
                _invoice.GetByAF(cemeteryTransaction.AF).Max(i => i.Amount))
            {
                return false;
            }

            var cemeteryTransactionInDb = GetByAF(cemeteryTransaction.AF);

            var deceased1InDb = cemeteryTransactionInDb.Deceased1Id;

            SummaryItem(cemeteryTransaction);

            cemeteryTransactionInDb.Total = cemeteryTransaction.Total;
            cemeteryTransactionInDb.SummaryItem = cemeteryTransaction.SummaryItem;
            cemeteryTransactionInDb.Remark = cemeteryTransaction.Remark;

            var plot = _plot.GetById(cemeteryTransaction.PlotId);

            PlotApplicantDeceaseds(plot, cemeteryTransaction.Deceased1Id, deceased1InDb);

            if (cemeteryTransaction.Deceased1Id == null)
                plot.hasDeceased = false;

            _tracking.Change(cemeteryTransaction.PlotId, cemeteryTransaction.AF, cemeteryTransaction.ApplicantId, cemeteryTransaction.Deceased1Id);

            _unitOfWork.Complete();

            return true;
        }

        private bool PlotApplicantDeceaseds(Core.Domain.Plot plot, int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    var d = _deceased.GetById((int)dbDeceasedId);
                    d.Plot = null;
                    d.PlotId = null;

                    return true;
                }

                var deceased = _deceased.GetById((int)deceasedId);
                if (deceased.PlotId != null && deceased.PlotId != plot.Id)
                {
                    return false;
                }

                if (dbDeceasedId != null)
                {
                    var d = _deceased.GetById((int)dbDeceasedId);
                    d.Plot = null;
                    d.PlotId = null;
                }

                var deceased1 = _deceased.GetById((int)deceasedId);
                deceased1.PlotId = plot.Id;
                plot.hasDeceased = true;
            }

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

            DeleteAllTransactionWithSamePlotId(transactionInDb.PlotId);

            _unitOfWork.CemeteryTransactions.Remove(transactionInDb);

            var plot = _plot.GetById(transactionInDb.PlotId);

            var deceaseds = _deceased.GetByPlotId(transactionInDb.PlotId);

            foreach (var deceased in deceaseds)
            {
                deceased.PlotId = null;
            }

            plot.hasDeceased = false;
            plot.Applicant = null;
            plot.ApplicantId = null;

            _tracking.Delete(transactionInDb.AF);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            var plot = _plot.GetById(cemeteryTransaction.PlotId);

            cemeteryTransaction.SummaryItem = "AF: " + cemeteryTransaction.AF + "<BR/>" +
                Resources.Mix.Plot + ": " + cemeteryTransaction.Plot.Name + "<BR/>" +
                Resources.Mix.Type + ": " + plot.PlotType.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + cemeteryTransaction.Remark;
        }
    }
}