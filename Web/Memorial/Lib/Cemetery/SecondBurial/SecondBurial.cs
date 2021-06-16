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
    public class SecondBurial : Transaction, ISecondBurial
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IPlot _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public SecondBurial(
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

        public void SetSecondBurial(string AF)
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

            NewNumber(cemeteryTransactionDto.CemeteryItemDtoId);

            SummaryItem(cemeteryTransactionDto);

            if (CreateNewTransaction(cemeteryTransactionDto))
            {
                _plot.SetPlot(cemeteryTransactionDto.PlotDtoId);

                if (cemeteryTransactionDto.DeceasedDto1Id != null)
                {
                    SetDeceased((int)cemeteryTransactionDto.DeceasedDto1Id);
                    if (!_deceased.SetPlot(cemeteryTransactionDto.PlotDtoId))
                    {
                        return false;
                    }                        
                }

                _tracking.AddDeceased(cemeteryTransactionDto.PlotDtoId, (int)cemeteryTransactionDto.DeceasedDto1Id);

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
            if (_invoice.GetInvoicesByAF(cemeteryTransactionDto.AF).Any() && cemeteryTransactionDto.Price <
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

                _tracking.ChangeDeceased(cemeteryTransactionDto.PlotDtoId, cemeteryTransactionDto.AF, (int)deceased1InDb, (int)cemeteryTransactionDto.DeceasedDto1Id);

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

                if(dbDeceasedId != null)
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
            _plot.SetPlot(_transaction.PlotId);

            DeleteTransaction();

            if (_transaction.Deceased1Id != null)
            {
                SetDeceased((int)_transaction.Deceased1Id);
                _deceased.RemovePlot();
            }

            _tracking.RemoveDeceased(_transaction.PlotId, _transaction.AF, (int)_transaction.Deceased1Id);

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(CemeteryTransactionDto trx)
        {
            _plot.SetPlot(trx.PlotDtoId);

            trx.SummaryItem = "AF: " + (string.IsNullOrEmpty(trx.AF) ? _AFnumber : trx.AF) + "<BR/>" +
                Resources.Mix.Plot + ": " + _plot.GetName() + "<BR/>" +
                Resources.Mix.Remark + ": " + trx.Remark;
        }

    }
}