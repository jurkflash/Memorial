using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IMiscellaneous _miscellaneous;
        protected IItem _item;
        protected IApplicant _applicant;
        protected INumber _number;
        protected Core.Domain.MiscellaneousTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            IMiscellaneous miscellaneous,
            IApplicant applicant,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _miscellaneous = miscellaneous;
            _applicant = applicant;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.MiscellaneousTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.MiscellaneousTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.MiscellaneousTransaction GetTransaction()
        {
            return _transaction;
        }

        public MiscellaneousTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.MiscellaneousTransaction, MiscellaneousTransactionDto>(GetTransaction());
        }

        public Core.Domain.MiscellaneousTransaction GetTransaction(string AF)
        {
            return _unitOfWork.MiscellaneousTransactions.GetActive(AF);
        }

        public MiscellaneousTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.MiscellaneousTransaction, MiscellaneousTransactionDto>(GetTransaction(AF));
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

        public int GetItemId()
        {
            return _transaction.MiscellaneousItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.MiscellaneousItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.MiscellaneousItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.MiscellaneousItemId);
            return _item.IsOrder();
        }

        public int? GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public IEnumerable<Core.Domain.MiscellaneousTransaction> GetTransactionsByItemId(int itemId, string filter)
        {
            return _unitOfWork.MiscellaneousTransactions.GetByItem(itemId, filter);
        }

        public IEnumerable<MiscellaneousTransactionDto> GetTransactionDtosByItemId(int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.MiscellaneousTransaction>, IEnumerable<MiscellaneousTransactionDto>>(GetTransactionsByItemId(itemId, filter));
        }

        protected bool CreateNewTransaction(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.MiscellaneousTransaction();

            Mapper.Map(miscellaneousTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.MiscellaneousTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            var miscellaneousTransactionInDb = GetTransaction(miscellaneousTransactionDto.AF);

            Mapper.Map(miscellaneousTransactionDto, miscellaneousTransactionInDb);

            miscellaneousTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }
    }
}