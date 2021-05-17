using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Plot
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IPlot _plot;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;
        protected Core.Domain.PlotTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            IPlot plot, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _plot = plot;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.PlotTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.PlotTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.PlotTransaction GetTransaction()
        {
            return _transaction;
        }

        public PlotTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.PlotTransaction, PlotTransactionDto>(GetTransaction());
        }

        public Core.Domain.PlotTransaction GetTransaction(string AF)
        {
            return _unitOfWork.PlotTransactions.GetActive(AF);
        }

        public Core.Domain.PlotTransaction GetTransactionExclusive(string AF)
        {
            return _unitOfWork.PlotTransactions.GetExclusive(AF);
        }

        public PlotTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.PlotTransaction, PlotTransactionDto>(GetTransaction(AF));
        }

        public IEnumerable<Core.Domain.PlotTransaction> GetTransactionsByPlotId(int plotId)
        {
            return _unitOfWork.PlotTransactions.GetByPlotId(plotId);
        }

        public string GetTransactionAF()
        {
            return _transaction.AF;
        }

        public float GetTransactionAmount()
        {
            return _transaction.Price +
                (_transaction.Maintenance == null ? 0 : (float)_transaction.Maintenance) +
                (_transaction.Wall == null ? 0 : (float)_transaction.Wall) +
                (_transaction.Dig == null ? 0 : (float)_transaction.Dig) +
                (_transaction.Brick == null ? 0 : (float)_transaction.Brick);             
        }

        public int GetTransactionPlotId()
        {
            return _transaction.PlotId;
        }

        public int GetItemId()
        {
            return _transaction.PlotItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.PlotItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.PlotItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.PlotItemId);
            return _item.IsOrder();
        }

        public int GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public int? GetTransactionDeceased1Id()
        {
            return _transaction.Deceased1Id;
        }

        public IEnumerable<Core.Domain.PlotTransaction> GetTransactionsByPlotIdAndItemId(int plotId, int itemId, string filter)
        {
            return _unitOfWork.PlotTransactions.GetByPlotIdAndItem(plotId, itemId, filter);
        }

        public IEnumerable<PlotTransactionDto> GetTransactionDtosByPlotIdAndItemId(int plotId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotTransaction>, IEnumerable<PlotTransactionDto>>(GetTransactionsByPlotIdAndItemId(plotId, itemId, filter));
        }

        public Core.Domain.PlotTransaction GetTransactionsByPlotIdAndDeceased1Id(int plotId, int deceased1Id)
        {
            return _unitOfWork.PlotTransactions.GetByPlotIdAndDeceased(plotId, deceased1Id);
        }

        public IEnumerable<Core.Domain.PlotTransaction> GetTransactionsByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId)
        {
            return _unitOfWork.PlotTransactions.GetByPlotIdAndItemAndApplicant(plotId, itemId, applicantId);
        }

        public IEnumerable<PlotTransactionDto> GetTransactionDtosByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotTransaction>, IEnumerable<PlotTransactionDto>>(GetTransactionsByPlotIdAndItemIdAndApplicantId(plotId, itemId, applicantId));
        }

        public Core.Domain.PlotTransaction GetLastPlotTransactionByPlotId(int plotId)
        {
            return _unitOfWork.PlotTransactions.GetLastPlotTransactionByPlotId(plotId);
        }

        protected bool CreateNewTransaction(PlotTransactionDto plotTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.PlotTransaction();

            Mapper.Map(plotTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.PlotTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(PlotTransactionDto plotTransactionDto)
        {
            var plotTransactionInDb = GetTransaction(plotTransactionDto.AF);

            Mapper.Map(plotTransactionDto, plotTransactionInDb);

            plotTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteAllTransactionWithSamePlotId()
        {
            var datetimeNow = System.DateTime.Now;

            var transactions = GetTransactionsByPlotId(_transaction.PlotId);

            foreach (var transaction in transactions)
            {
                transaction.DeleteDate = datetimeNow;
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnPlot(PlotTransactionDto plotTransactionDto, int plotId)
        {
            _plot.SetPlot(plotId);

            if (_plot.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByPlotId(plotId);

                if (_plot.GetNumberOfPlacement() < deceaseds.Count())
                    return false;

                if (deceaseds.Count() > 2)
                {
                    if (_applicantDeceased.GetApplicantDeceased(plotTransactionDto.ApplicantDtoId, deceaseds.ElementAt(2).Id) == null)
                    {
                        return false;
                    }

                    plotTransactionDto.DeceasedDto3Id = deceaseds.ElementAt(2).Id;
                }

                if (deceaseds.Count() > 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(plotTransactionDto.ApplicantDtoId, deceaseds.ElementAt(1).Id) == null)
                    {
                        return false;
                    }

                    plotTransactionDto.DeceasedDto2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(plotTransactionDto.ApplicantDtoId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    plotTransactionDto.DeceasedDto1Id = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool SetDeceasedIdBasedOnPlotLastTransaction(PlotTransactionDto plotTransactionDto)
        {
            if (_plot.HasDeceased())
            {
                var lastTransactionOfPlot = GetLastPlotTransactionByPlotId(_plot.GetPlot().Id);

                if (lastTransactionOfPlot != null)
                {
                    SetDeceasedIdBasedOnPlotLastTransaction(lastTransactionOfPlot, plotTransactionDto);
                }
            }

            return true;
        }

        private bool SetDeceasedIdBasedOnPlotLastTransaction(Core.Domain.PlotTransaction lastPlotTransaction, PlotTransactionDto plotTransactionDto)
        {
            if (lastPlotTransaction != null)
            {
                if (lastPlotTransaction.Deceased1Id != null &&
                    _applicantDeceased.GetApplicantDeceased(plotTransactionDto.ApplicantDtoId, (int)lastPlotTransaction.Deceased1Id) == null)
                {
                    return false;
                }

                if (lastPlotTransaction.Deceased2Id != null &&
                    _applicantDeceased.GetApplicantDeceased(plotTransactionDto.ApplicantDtoId, (int)lastPlotTransaction.Deceased2Id) == null)
                {
                    return false;
                }

                plotTransactionDto.DeceasedDto1Id = lastPlotTransaction.Deceased1Id;

                plotTransactionDto.DeceasedDto1Id = lastPlotTransaction.Deceased2Id;
            }

            return true;
        }

    }
}