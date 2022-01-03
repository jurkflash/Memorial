using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected ICremation _cremation;
        protected IItem _item;
        protected IApplicant _applicant;
        protected INumber _number;
        protected Core.Domain.CremationTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            ICremation cremation,
            IApplicant applicant,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _cremation = cremation;
            _applicant = applicant;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.CremationTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.CremationTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.CremationTransaction GetTransaction()
        {
            return _transaction;
        }

        public CremationTransactionDto GetCremationDto()
        {
            return Mapper.Map<Core.Domain.CremationTransaction, CremationTransactionDto>(GetTransaction());
        }

        public Core.Domain.CremationTransaction GetTransaction(string AF)
        {
            return _unitOfWork.CremationTransactions.GetActive(AF);
        }

        public CremationTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.CremationTransaction, CremationTransactionDto>(GetTransaction(AF));
        }

        public string GetTransactionAF()
        {
            return _transaction.AF;
        }

        public float GetTransactionAmount()
        {
            return _transaction.Price;
        }

        public string GetTransactionSummaryItem()
        {
            return _transaction.SummaryItem;
        }

        public string GetSiteHeader()
        {
            return _transaction.CremationItem.Cremation.Site.Header;
        }

        public int GetItemId()
        {
            return _transaction.CremationItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.CremationItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.CremationItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.CremationItemId);
            return _item.IsOrder();
        }

        public int GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public IEnumerable<Core.Domain.CremationTransaction> GetTransactionsByItemId(int itemId, string filter)
        {
            return _unitOfWork.CremationTransactions.GetByItem(itemId, filter);
        }

        public IEnumerable<CremationTransactionDto> GetTransactionDtosByItemId(int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CremationTransaction>, IEnumerable<CremationTransactionDto>>(GetTransactionsByItemId(itemId, filter));
        }

        public IEnumerable<Core.Domain.CremationTransaction> GetTransactionsByItemIdAndDeceasedId(int itemId, int deceasedId)
        {
            return _unitOfWork.CremationTransactions.GetByItemAndDeceased(itemId, deceasedId);
        }

        public IEnumerable<CremationTransactionDto> GetRecent(int siteId, int? applicantId)
        {
            if (applicantId == null)
                return Mapper.Map<IEnumerable<Core.Domain.CremationTransaction>, IEnumerable<CremationTransactionDto>>(_unitOfWork.CremationTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId));
            else
                return Mapper.Map<IEnumerable<Core.Domain.CremationTransaction>, IEnumerable<CremationTransactionDto>>(_unitOfWork.CremationTransactions.GetRecent(null, siteId, applicantId));
        }

        protected bool CreateNewTransaction(CremationTransactionDto cremationTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.CremationTransaction();

            Mapper.Map(cremationTransactionDto, _transaction);

            _transaction.AF = _AFnumber;

            _unitOfWork.CremationTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(CremationTransactionDto cremationTransactionDto)
        {
            var cremationTransactionInDb = GetTransaction(cremationTransactionDto.AF);

            Mapper.Map(cremationTransactionDto, cremationTransactionInDb);

            return true;
        }

        protected bool DeleteTransaction()
        {
            _unitOfWork.CremationTransactions.Remove(_transaction);

            return true;
        }
    }
}