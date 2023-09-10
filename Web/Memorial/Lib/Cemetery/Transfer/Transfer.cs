using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Cemetery
{
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public Transfer(
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

        public bool AllowPlotDeceasePairing(Core.Domain.Plot plot, int applicantId)
        {
            if (plot.hasDeceased)
            {
                var deceaseds = _deceased.GetByPlotId(plot.Id);
                foreach (var deceased in deceaseds)
                {
                    var applicantDeceased = _applicantDeceased.GetByApplicantDeceasedId(applicantId, deceased.Id);
                    if (applicantDeceased != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public bool Add(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            var plot = _plot.GetById(cemeteryTransaction.PlotId);
            if (plot.ApplicantId == cemeteryTransaction.ApplicantId)
                return false;

            if (!AllowPlotDeceasePairing(plot, cemeteryTransaction.ApplicantId))
                return false;

            if (!SetTransactionDeceasedIdBasedOnPlot(cemeteryTransaction, cemeteryTransaction.PlotId))
                return false;

            cemeteryTransaction.TransferredCemeteryTransactionAF = _tracking.GetLatestFirstTransactionByPlotId(cemeteryTransaction.PlotId).CemeteryTransactionAF;

            GetByAF(cemeteryTransaction.TransferredCemeteryTransactionAF).DeletedUtcTime = System.DateTime.UtcNow;

            _tracking.Remove(cemeteryTransaction.PlotId, cemeteryTransaction.TransferredCemeteryTransactionAF);

            cemeteryTransaction.AF = _number.GetNewAF(cemeteryTransaction.CemeteryItemId, System.DateTime.Now.Year);

            SummaryItem(cemeteryTransaction);

            _unitOfWork.CemeteryTransactions.Add(cemeteryTransaction);

            plot.ApplicantId = cemeteryTransaction.ApplicantId;

            _tracking.Add(cemeteryTransaction.PlotId, cemeteryTransaction.AF, cemeteryTransaction.ApplicantId, cemeteryTransaction.Deceased1Id, cemeteryTransaction.Deceased2Id, cemeteryTransaction.Deceased3Id);

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

            plot.Applicant = null;
            plot.ApplicantId = null;

            plot.hasDeceased = false;

            var deceaseds = _deceased.GetByPlotId(transactionInDb.PlotId);

            foreach (var deceased in deceaseds)
            {
                deceased.Niche = null;
                deceased.NicheId = null;
            }

            _tracking.Remove(transactionInDb.PlotId, transactionInDb.AF);


            var previousTransaction = GetTransactionExclusive(transactionInDb.TransferredCemeteryTransactionAF);

            var previousPlot = _plot.GetById(previousTransaction.PlotId);

            previousPlot.ApplicantId = previousTransaction.ApplicantId;

            if (previousTransaction.Deceased1Id != null)
            {
                var d = _deceased.GetById((int)previousTransaction.Deceased1Id);

                if (d.PlotId != null && d.PlotId != transactionInDb.PlotId)
                    return false;

                d.NicheId = previousTransaction.PlotId;

                previousPlot.hasDeceased = true;
            }

            if (previousTransaction.Deceased2Id != null)
            {
                var d = _deceased.GetById((int)previousTransaction.Deceased2Id);

                if (d.PlotId != null && d.PlotId != transactionInDb.PlotId)
                    return false;

                d.NicheId = previousTransaction.PlotId;

                previousPlot.hasDeceased = true;
            }

            if (previousTransaction.Deceased3Id != null)
            {
                var d = _deceased.GetById((int)previousTransaction.Deceased3Id);

                if (d.PlotId != null && d.PlotId != transactionInDb.PlotId)
                    return false;

                d.NicheId = previousTransaction.PlotId;

                previousPlot.hasDeceased = true;
            }

            _unitOfWork.CemeteryTransactions.Remove(transactionInDb);

            previousTransaction.DeletedUtcTime = null;

            _tracking.Add(previousTransaction.PlotId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id, previousTransaction.Deceased3Id);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            cemeteryTransaction.SummaryItem = "AF: " + cemeteryTransaction.AF + "<BR/>" +
                Resources.Mix.Applicant + ": " + cemeteryTransaction.Plot.Name + "<BR/>" +
                Resources.Mix.TransferToBR + 
                Resources.Mix.Applicant + ": " + cemeteryTransaction.Applicant.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + cemeteryTransaction.Remark;
        }
    }
}