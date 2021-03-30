﻿using Memorial.Core;
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
    public class SingleOrder : Transaction, ISingleOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IPlot _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IPlotApplicantDeceaseds _plotApplicantDeceaseds;

        public SingleOrder(
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

        public void SetSingleOrder(string AF)
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

        public bool Create(PlotTransactionDto plotTransactionDto)
        {
            if (plotTransactionDto.Deceased1Id != null)
            {
                SetDeceased((int)plotTransactionDto.Deceased1Id);
                if (_deceased.GetPlot() != null)
                    return false;
            }

            NewNumber(plotTransactionDto.PlotItemId);

            if (CreateNewTransaction(plotTransactionDto))
            {
                _plot.SetPlot(plotTransactionDto.PlotDtoId);
                _plot.SetApplicant(plotTransactionDto.ApplicantDtoId);

                if (plotTransactionDto.Deceased1Id != null)
                {
                    SetDeceased((int)plotTransactionDto.Deceased1Id);
                    if (_deceased.SetPlot(plotTransactionDto.PlotDtoId))
                    {
                        _plot.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _tracking.Add(plotTransactionDto.PlotDtoId, _AFnumber, plotTransactionDto.ApplicantDtoId, plotTransactionDto.Deceased1Id);

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

                PlotApplicantDeceaseds(plotTransactionDto.Deceased1Id, deceased1InDb);

                if (plotTransactionDto.Deceased1Id == null)
                    _plot.SetHasDeceased(false);

                _tracking.Change(plotTransactionDto.PlotDtoId, plotTransactionDto.AF, plotTransactionDto.ApplicantDtoId, plotTransactionDto.Deceased1Id);

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
                }
                else
                {
                    _deceased.SetDeceased((int)deceasedId);
                    if (_deceased.GetPlot() != null && _deceased.GetPlot().Id != _plot.GetPlot().Id)
                    {
                        return false;
                    }
                    else
                    {
                        _deceased.SetPlot(_plot.GetPlot().Id);
                        _plot.SetHasDeceased(true);
                    }
                }
            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.PlotId, _transaction.AF))
                return false;

            _plot.SetPlot(_transaction.PlotId);
            if (_plot.HasDeceased())
                return false;

            var lastTransactionOfPlot = GetLastPlotTransactionByPlotId(_transaction.PlotId);

            DeleteTransaction();

            if (lastTransactionOfPlot.AF == _transaction.AF)
            {
                if (_transaction.Deceased1Id != null)
                {
                    SetDeceased((int)_transaction.Deceased1Id);
                    _deceased.RemovePlot();
                }

                _plot.SetHasDeceased(false);

                _plot.RemoveApplicant();
            }

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _plotApplicantDeceaseds.RollbackPlotApplicantDeceaseds(_transaction.AF, _transaction.PlotId);

            _unitOfWork.Complete();

            return true;
        }

    }
}