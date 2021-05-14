using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IPlot _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Transfer(
            IUnitOfWork unitOfWork,
            IItem item,
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IPlot invoice,
            IPayment payment,
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
            _payment = payment;
            _tracking = tracking;
        }

        public void SetTransfer(string AF)
        {
            SetTransaction(AF);
        }

        public float GetPrice(int itemId)
        {
            _item.SetItem(itemId);
            return _item.GetPrice();
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool AllowPlotDeceasePairing(IPlot plot, int applicantId)
        {
            if (plot.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByPlotId(plot.GetPlot().Id);
                foreach (var deceased in deceaseds)
                {
                    var applicantDeceased = _applicantDeceased.GetApplicantDeceased(applicantId, deceased.Id);
                    if (applicantDeceased != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public bool Create(PlotTransactionDto plotTransactionDto)
        {
            _plot.SetPlot(plotTransactionDto.PlotDtoId);
            if (_plot.GetApplicantId() == plotTransactionDto.ApplicantDtoId)
                return false;

            if (!AllowPlotDeceasePairing(_plot, plotTransactionDto.ApplicantDtoId))
                return false;

            if (!SetTransactionDeceasedIdBasedOnPlot(plotTransactionDto, plotTransactionDto.PlotDtoId))
                return false;

            plotTransactionDto.TransferredPlotTransactionAF = _tracking.GetLatestFirstTransactionByPlotId(plotTransactionDto.PlotDtoId).PlotTransactionAF;

            GetTransaction(plotTransactionDto.TransferredPlotTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove(plotTransactionDto.PlotDtoId, plotTransactionDto.TransferredPlotTransactionAF);

            NewNumber(plotTransactionDto.PlotItemId);

            if (CreateNewTransaction(plotTransactionDto))
            {
                _plot.SetApplicant(plotTransactionDto.ApplicantDtoId);

                _tracking.Add(plotTransactionDto.PlotDtoId, _AFnumber, plotTransactionDto.ApplicantDtoId, plotTransactionDto.DeceasedDto1Id, plotTransactionDto.DeceasedDto2Id, plotTransactionDto.DeceasedDto3Id);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
 

            return true;
        }

        public bool Update(PlotTransactionDto plotTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(plotTransactionDto.AF).Any() && plotTransactionDto.Price <
                _invoice.GetInvoicesByAF(plotTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            UpdateTransaction(plotTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.PlotId, _transaction.AF))
                return false;

            _plot.SetPlot(_transaction.PlotId);

            _plot.RemoveApplicant();

            _plot.SetHasDeceased(false);

            var deceaseds = _deceased.GetDeceasedsByPlotId(_transaction.PlotId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveQuadrangle();
            }

            _tracking.Remove(_transaction.PlotId, _transaction.AF);


            var previousTransaction = GetTransactionExclusive(_transaction.TransferredPlotTransactionAF);

            _plot.SetPlot(previousTransaction.PlotId);

            _plot.SetApplicant(previousTransaction.ApplicantId);

            if (previousTransaction.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased1Id);

                if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != _transaction.PlotId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.PlotId);

                _plot.SetHasDeceased(true);
            }

            if (previousTransaction.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased2Id);

                if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != _transaction.PlotId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.PlotId);

                _plot.SetHasDeceased(true);
            }

            if (previousTransaction.Deceased3Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased3Id);

                if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != _transaction.PlotId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.PlotId);

                _plot.SetHasDeceased(true);
            }

            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

    }
}