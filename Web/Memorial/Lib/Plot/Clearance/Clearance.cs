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
    public class Clearance : Transaction, IClearance
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IPlot _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Clearance(
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

        public void SetClearance(string AF)
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
            NewNumber(plotTransactionDto.PlotItemId);

            if (CreateNewTransaction(plotTransactionDto))
            {
                _plot.SetPlot(plotTransactionDto.PlotDtoId);

                plotTransactionDto.ClearedApplicantId = _plot.GetApplicantId();

                var deceaseds = _deceased.GetDeceasedsByPlotId(plotTransactionDto.PlotDtoId);

                if (deceaseds.Count() > 0)
                {
                    plotTransactionDto.DeceasedDto1Id = deceaseds.ElementAt(0).Id;
                }

                if (deceaseds.Count() > 1)
                {
                    plotTransactionDto.DeceasedDto2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() > 2)
                {
                    plotTransactionDto.DeceasedDto3Id = deceaseds.ElementAt(2).Id;
                }

                foreach (var deceased in deceaseds)
                {
                    _deceased.SetDeceased(deceased.Id);
                    _deceased.RemovePlot();
                }

                _plot.SetHasDeceased(false);

                _plot.RemoveApplicant();

                _plot.SetHasCleared(true);

                _tracking.Add(plotTransactionDto.PlotDtoId, _AFnumber, plotTransactionDto.ApplicantDtoId);

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

            if (UpdateTransaction(plotTransactionDto))
            {
                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.PlotId, _transaction.AF))
                return false;

            _plot.SetPlot(_transaction.PlotId);

            var lastTransactionOfPlot = GetLastPlotTransactionByPlotId(_transaction.PlotId);

            DeleteTransaction();

            if (lastTransactionOfPlot.AF == _transaction.AF)
            {
                _plot.SetApplicant(lastTransactionOfPlot.ApplicantId);

                _plot.SetHasDeceased(true);

                _plot.SetHasCleared(false);

                SetDeceased((int)_transaction.Deceased1Id);
                if (!_deceased.SetPlot(_transaction.PlotId))
                {
                    return false;
                }

                if (lastTransactionOfPlot.Deceased2Id != null)
                {
                    SetDeceased((int)_transaction.Deceased2Id);
                    if (!_deceased.SetPlot(_transaction.PlotId))
                    {
                        return false;
                    }
                }

                if (lastTransactionOfPlot.Deceased3Id != null)
                {
                    SetDeceased((int)_transaction.Deceased3Id);
                    if (!_deceased.SetPlot(_transaction.PlotId))
                    {
                        return false;
                    }
                }
            }

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _tracking.Remove(_transaction.PlotId, _transaction.AF);

            _unitOfWork.Complete();

            return true;
        }

    }
}