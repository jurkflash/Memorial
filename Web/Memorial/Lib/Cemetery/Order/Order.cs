using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IPlot _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IPlotApplicantDeceaseds _plotApplicantDeceaseds;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IPlot invoice,
            IPayment payment,
            ITracking tracking,
            IPlotApplicantDeceaseds plotApplicantDeceaseds
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
            _plotApplicantDeceaseds = plotApplicantDeceaseds;
        }

        public void SetOrder(string AF)
        {
            SetTransaction(AF);
        }

        private void SetDeceased(int id)
        {
            _deceased.SetDeceased(id);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(CemeteryTransactionDto plotTransactionDto)
        {
            if (plotTransactionDto.DeceasedDto1Id != null)
            {
                SetDeceased((int)plotTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null)
                    return false;
            }

            NewNumber(plotTransactionDto.PlotItemId);

            if (CreateNewTransaction(plotTransactionDto))
            {
                _plot.SetPlot(plotTransactionDto.PlotDtoId);
                _plot.SetApplicant(plotTransactionDto.ApplicantDtoId);

                if (plotTransactionDto.DeceasedDto1Id != null)
                {
                    SetDeceased((int)plotTransactionDto.DeceasedDto1Id);
                    if (_deceased.SetPlot(plotTransactionDto.PlotDtoId))
                    {
                        _plot.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _tracking.Add(plotTransactionDto.PlotDtoId, _AFnumber, plotTransactionDto.ApplicantDtoId, plotTransactionDto.DeceasedDto1Id);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(CemeteryTransactionDto plotTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(plotTransactionDto.AF).Any() && 
                plotTransactionDto.Price + 
                (float)plotTransactionDto.Maintenance + 
                (float)plotTransactionDto.Dig + 
                (float)plotTransactionDto.Brick + 
                (float)plotTransactionDto.Wall 
                <
                _invoice.GetInvoicesByAF(plotTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var plotTransactionInDb = GetTransaction(plotTransactionDto.AF);

            var deceased1InDb = plotTransactionInDb.Deceased1Id;

            if (UpdateTransaction(plotTransactionDto))
            {
                _plot.SetPlot(plotTransactionDto.PlotDtoId);

                PlotApplicantDeceaseds(plotTransactionDto.DeceasedDto1Id, deceased1InDb);

                if (plotTransactionDto.DeceasedDto1Id == null)
                    _plot.SetHasDeceased(false);

                _tracking.Change(plotTransactionDto.PlotDtoId, plotTransactionDto.AF, plotTransactionDto.ApplicantDtoId, plotTransactionDto.DeceasedDto1Id);

                _unitOfWork.Complete();
            }

            return true;
        }

        private bool PlotApplicantDeceaseds(int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    _deceased.SetDeceased((int)dbDeceasedId);
                    _deceased.RemovePlot();

                    return true;
                }

                _deceased.SetDeceased((int)deceasedId);
                if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != _plot.GetPlot().Id)
                {
                    return false;
                }

                if (dbDeceasedId != null)
                {
                    _deceased.SetDeceased((int)dbDeceasedId);
                    _deceased.RemovePlot();
                }

                _deceased.SetDeceased((int)deceasedId);
                _deceased.SetPlot(_plot.GetPlot().Id);
                _plot.SetHasDeceased(true);

            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.PlotId, _transaction.AF))
                return false;

            DeleteAllTransactionWithSamePlotId();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();


            _plot.SetPlot(_transaction.PlotId);

            var deceaseds = _deceased.GetDeceasedsByPlotId(_transaction.PlotId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemovePlot();
            }

            _plot.SetHasDeceased(false);

            _plot.RemoveApplicant();

            _tracking.Delete(_transaction.AF);

            _unitOfWork.Complete();

            return true;
        }

    }
}