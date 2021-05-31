using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected ISpace _space;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;
        protected Core.Domain.SpaceTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            ISpace space,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _space = space;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.SpaceTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.SpaceTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.SpaceTransaction GetTransaction()
        {
            return _transaction;
        }

        public SpaceTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.SpaceTransaction, SpaceTransactionDto>(GetTransaction());
        }

        public Core.Domain.SpaceTransaction GetTransaction(string AF)
        {
            return _unitOfWork.SpaceTransactions.GetActive(AF);
        }

        public SpaceTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.SpaceTransaction, SpaceTransactionDto>(GetTransaction(AF));
        }

        public string GetTransactionAF()
        {
            return _transaction.AF;
        }

        public float GetTransactionAmount()
        {
            return _transaction.Amount;
        }

        public string GetTransactionSummaryItem()
        {
            return _transaction.SummaryItem;
        }

        public float GetTransactionOtherCharges()
        {
            return _transaction.OtherCharges;
        }

        public float GetTransactionTotalAmount()
        {
            return GetTransactionAmount() + GetTransactionOtherCharges();
        }

        public int GetTransactionSpaceItemId()
        {
            return _transaction.SpaceItemId;
        }

        public int GetItemId()
        {
            return _transaction.SpaceItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.SpaceItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.SpaceItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.SpaceItemId);
            return _item.IsOrder();
        }

        public bool IsItemAllowDeposit()
        {
            _item.SetItem(_transaction.SpaceItemId);
            return _item.AllowDeposit();
        }

        public int GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public int? GetTransactionDeceasedId()
        {
            return _transaction.DeceasedId;
        }

        public IEnumerable<Core.Domain.SpaceTransaction> GetTransactionsByItemId(int itemId, string filter)
        {
            return _unitOfWork.SpaceTransactions.GetByItem(itemId, filter);
        }

        public IEnumerable<SpaceTransactionDto> GetTransactionDtosByItemId(int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceTransaction>, IEnumerable<SpaceTransactionDto>>(GetTransactionsByItemId(itemId, filter));
        }

        public IEnumerable<Core.Domain.SpaceTransaction> GetTransactionsByItemIdAndApplicantId(int applicantId, int itemId)
        {
            return _unitOfWork.SpaceTransactions.GetByItemAndApplicant(itemId, applicantId);
        }

        public IEnumerable<SpaceTransactionDto> GetTransactionDtosByItemIdAndApplicantId(int applicantId, int itemId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceTransaction>, IEnumerable<SpaceTransactionDto>>(GetTransactionsByItemIdAndApplicantId(itemId, applicantId));
        }

        public IEnumerable<Core.Domain.SpaceTransaction> GetTransactionByItemIdAndDeceasedId(int deceasedId, int itemId)
        {
            return _unitOfWork.SpaceTransactions.GetByItemAndDeceased(itemId, deceasedId);
        }

        public IEnumerable<Core.Domain.SpaceBooked> GetBookedTransaction(DateTime from, DateTime to, byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceTransaction>, IEnumerable<Core.Domain.SpaceBooked>>(_unitOfWork.SpaceTransactions.GetBooked(from, to, siteId));
        }
        

        protected bool CreateNewTransaction(SpaceTransactionDto spaceTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.SpaceTransaction();

            Mapper.Map(spaceTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.SpaceTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(SpaceTransactionDto spaceTransactionDto)
        {
            var spaceTransactionInDb = GetTransaction(spaceTransactionDto.AF);

            Mapper.Map(spaceTransactionDto, spaceTransactionInDb);

            spaceTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }
    }
}