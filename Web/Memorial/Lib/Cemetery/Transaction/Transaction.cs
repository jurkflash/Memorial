using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Cemetery
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
        protected Core.Domain.CemeteryTransaction _transaction;
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
            _transaction = _unitOfWork.CemeteryTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.CemeteryTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.CemeteryTransaction GetTransaction()
        {
            return _transaction;
        }

        public CemeteryTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.CemeteryTransaction, CemeteryTransactionDto>(GetTransaction());
        }

        public Core.Domain.CemeteryTransaction GetTransaction(string AF)
        {
            return _unitOfWork.CemeteryTransactions.GetActive(AF);
        }

        public Core.Domain.CemeteryTransaction GetTransactionExclusive(string AF)
        {
            return _unitOfWork.CemeteryTransactions.GetExclusive(AF);
        }

        public CemeteryTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.CemeteryTransaction, CemeteryTransactionDto>(GetTransaction(AF));
        }

        public IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotId(int plotId)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotId(plotId);
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
            return _transaction.CemeteryItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.CemeteryItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.CemeteryItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.CemeteryItemId);
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

        public int? GetTransactionTransferredApplicantId()
        {
            return _transaction.TransferredApplicantId;
        }

        public IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotIdAndItemId(int plotId, int itemId, string filter)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotIdAndItem(plotId, itemId, filter);
        }

        public IEnumerable<CemeteryTransactionDto> GetTransactionDtosByPlotIdAndItemId(int plotId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryTransaction>, IEnumerable<CemeteryTransactionDto>>(GetTransactionsByPlotIdAndItemId(plotId, itemId, filter));
        }

        public Core.Domain.CemeteryTransaction GetTransactionsByPlotIdAndDeceased1Id(int plotId, int deceased1Id)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotIdAndDeceased(plotId, deceased1Id);
        }

        public IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotIdAndItemAndApplicant(plotId, itemId, applicantId);
        }

        public IEnumerable<CemeteryTransactionDto> GetTransactionDtosByPlotIdAndItemIdAndApplicantId(int plotId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryTransaction>, IEnumerable<CemeteryTransactionDto>>(GetTransactionsByPlotIdAndItemIdAndApplicantId(plotId, itemId, applicantId));
        }

        public Core.Domain.CemeteryTransaction GetLastCemeteryTransactionTransactionByPlotId(int plotId)
        {
            return _unitOfWork.CemeteryTransactions.GetLastCemeteryTransactionByPlotId(plotId);
        }

        protected bool CreateNewTransaction(CemeteryTransactionDto cemeteryTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.CemeteryTransaction();

            Mapper.Map(cemeteryTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.CemeteryTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(CemeteryTransactionDto cemeteryTransactionDto)
        {
            var cemeteryTransactionInDb = GetTransaction(cemeteryTransactionDto.AF);

            Mapper.Map(cemeteryTransactionDto, cemeteryTransactionInDb);

            cemeteryTransactionInDb.ModifyDate = System.DateTime.Now;

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

        protected bool SetTransactionDeceasedIdBasedOnPlot(CemeteryTransactionDto cemeteryTransactionDto, int plotId)
        {
            _plot.SetPlot(plotId);

            if (_plot.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByPlotId(plotId);

                if (_plot.GetNumberOfPlacement() < deceaseds.Count())
                    return false;

                if (deceaseds.Count() > 2)
                {
                    if (_applicantDeceased.GetApplicantDeceased(cemeteryTransactionDto.ApplicantDtoId, deceaseds.ElementAt(2).Id) == null)
                    {
                        return false;
                    }

                    cemeteryTransactionDto.DeceasedDto3Id = deceaseds.ElementAt(2).Id;
                }

                if (deceaseds.Count() > 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(cemeteryTransactionDto.ApplicantDtoId, deceaseds.ElementAt(1).Id) == null)
                    {
                        return false;
                    }

                    cemeteryTransactionDto.DeceasedDto2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(cemeteryTransactionDto.ApplicantDtoId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    cemeteryTransactionDto.DeceasedDto1Id = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool SetDeceasedIdBasedOnPlotLastTransaction(CemeteryTransactionDto cemeteryTransactionDto)
        {
            if (_plot.HasDeceased())
            {
                var lastTransactionOfPlot = GetLastCemeteryTransactionTransactionByPlotId(_plot.GetPlot().Id);

                if (lastTransactionOfPlot != null)
                {
                    SetDeceasedIdBasedOnPlotLastTransaction(lastTransactionOfPlot, cemeteryTransactionDto);
                }
            }

            return true;
        }

        private bool SetDeceasedIdBasedOnPlotLastTransaction(Core.Domain.CemeteryTransaction lastCemeteryTransaction, CemeteryTransactionDto cemeteryTransactionDto)
        {
            if (lastCemeteryTransaction != null)
            {
                if (lastCemeteryTransaction.Deceased1Id != null &&
                    _applicantDeceased.GetApplicantDeceased(cemeteryTransactionDto.ApplicantDtoId, (int)lastCemeteryTransaction.Deceased1Id) == null)
                {
                    return false;
                }

                if (lastCemeteryTransaction.Deceased2Id != null &&
                    _applicantDeceased.GetApplicantDeceased(cemeteryTransactionDto.ApplicantDtoId, (int)lastCemeteryTransaction.Deceased2Id) == null)
                {
                    return false;
                }

                cemeteryTransactionDto.DeceasedDto1Id = lastCemeteryTransaction.Deceased1Id;

                cemeteryTransactionDto.DeceasedDto1Id = lastCemeteryTransaction.Deceased2Id;
            }

            return true;
        }

    }
}