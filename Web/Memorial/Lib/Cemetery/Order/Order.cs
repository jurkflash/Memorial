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

        public bool Create(CemeteryTransactionDto cemeteryTransactionDto)
        {
            if (cemeteryTransactionDto.DeceasedDto1Id != null)
            {
                SetDeceased((int)cemeteryTransactionDto.DeceasedDto1Id);
                if (_deceased.GetPlot() != null)
                    return false;
            }

            NewNumber(cemeteryTransactionDto.CemeteryItemId);

            SummaryItem(cemeteryTransactionDto);

            if (CreateNewTransaction(cemeteryTransactionDto))
            {
                _plot.SetPlot(cemeteryTransactionDto.PlotDtoId);
                _plot.SetApplicant(cemeteryTransactionDto.ApplicantDtoId);

                if (cemeteryTransactionDto.DeceasedDto1Id != null)
                {
                    SetDeceased((int)cemeteryTransactionDto.DeceasedDto1Id);
                    if (_deceased.SetPlot(cemeteryTransactionDto.PlotDtoId))
                    {
                        _plot.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _tracking.Add(cemeteryTransactionDto.PlotDtoId, _AFnumber, cemeteryTransactionDto.ApplicantDtoId, cemeteryTransactionDto.DeceasedDto1Id);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(CemeteryTransactionDto cemeteryTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(cemeteryTransactionDto.AF).Any() && 
                cemeteryTransactionDto.Price + 
                (float)cemeteryTransactionDto.Maintenance + 
                (float)cemeteryTransactionDto.Dig + 
                (float)cemeteryTransactionDto.Brick + 
                (float)cemeteryTransactionDto.Wall 
                <
                _invoice.GetInvoicesByAF(cemeteryTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var cemeteryTransactionInDb = GetTransaction(cemeteryTransactionDto.AF);

            var deceased1InDb = cemeteryTransactionInDb.Deceased1Id;

            SummaryItem(cemeteryTransactionDto);

            if (UpdateTransaction(cemeteryTransactionDto))
            {
                _plot.SetPlot(cemeteryTransactionDto.PlotDtoId);

                PlotApplicantDeceaseds(cemeteryTransactionDto.DeceasedDto1Id, deceased1InDb);

                if (cemeteryTransactionDto.DeceasedDto1Id == null)
                    _plot.SetHasDeceased(false);

                _tracking.Change(cemeteryTransactionDto.PlotDtoId, cemeteryTransactionDto.AF, cemeteryTransactionDto.ApplicantDtoId, cemeteryTransactionDto.DeceasedDto1Id);

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

        private void SummaryItem(CemeteryTransactionDto trx)
        {
            _plot.SetPlot(trx.PlotDtoId);

            trx.SummaryItem = "AF: " + trx.AF == null ? _AFnumber : trx.AF + "<BR/>" +
                Resources.Mix.Plot + ": " + _plot.GetName() + "<BR/>" +
                Resources.Mix.Type + ": " + _plot.GetPlot().PlotType.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + trx.Remark;
        }
    }
}