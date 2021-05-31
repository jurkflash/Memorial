using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using AutoMapper;

namespace Memorial.Lib.Urn
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IUrn _urn;
        protected IItem _item;
        protected IApplicant _applicant;
        protected INumber _number;
        protected Core.Domain.UrnTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            IUrn urn,
            IApplicant applicant,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _urn = urn;
            _applicant = applicant;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.UrnTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.UrnTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.UrnTransaction GetTransaction()
        {
            return _transaction;
        }

        public UrnTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.UrnTransaction, UrnTransactionDto>(GetTransaction());
        }

        public Core.Domain.UrnTransaction GetTransaction(string AF)
        {
            return _unitOfWork.UrnTransactions.GetActive(AF);
        }

        public UrnTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.UrnTransaction, UrnTransactionDto>(GetTransaction(AF));
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
            return _transaction.UrnItem.Urn.Site.Header;
        }

        public int GetItemId()
        {
            return _transaction.UrnItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.UrnItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.UrnItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.UrnItemId);
            return _item.IsOrder();
        }

        public int GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public IEnumerable<Core.Domain.UrnTransaction> GetTransactionsByItemId(int itemId, string filter)
        {
            return _unitOfWork.UrnTransactions.GetByItem(itemId, filter);
        }

        public IEnumerable<UrnTransactionDto> GetTransactionDtosByItemId(int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnTransaction>, IEnumerable<UrnTransactionDto>>(GetTransactionsByItemId(itemId, filter));
        }

        protected bool CreateNewTransaction(UrnTransactionDto urnTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.UrnTransaction();

            Mapper.Map(urnTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.UrnTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(UrnTransactionDto urnTransactionDto)
        {
            var urnTransactionInDb = GetTransaction(urnTransactionDto.AF);

            Mapper.Map(urnTransactionDto, urnTransactionInDb);

            urnTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }
    }
}