using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Receipt;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Ancestor
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IAncestor _ancestor;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;
        protected Core.Domain.AncestorTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item,
            IAncestor ancestor, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestor = ancestor;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.AncestorTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.AncestorTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.AncestorTransaction GetTransaction()
        {
            return _transaction;
        }

        public AncestorTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.AncestorTransaction, AncestorTransactionDto>(GetTransaction());
        }

        public Core.Domain.AncestorTransaction GetTransaction(string AF)
        {
            return _unitOfWork.AncestorTransactions.GetActive(AF);
        }

        public AncestorTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.AncestorTransaction, AncestorTransactionDto>(GetTransaction(AF));
        }

        public string GetTransactionAF()
        {
            return _transaction.AF;
        }

        public float GetTransactionAmount()
        {
            return _transaction.Price +
                (_transaction.Maintenance == null ? 0 : (float)_transaction.Maintenance);
        }

        public int GetTransactionAncestorId()
        {
            return _transaction.AncestorId;
        }

        public int GetItemId()
        {
            return _transaction.AncestorItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.AncestorItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.AncestorItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.AncestorItemId);
            return _item.IsOrder();
        }

        public int GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public int? GetTransactionDeceasedId()
        {
            return _transaction.DeceasedId;
        }

        public IEnumerable<Core.Domain.AncestorTransaction> GetTransactionsByAncestorIdAndItemId(int ancestorId, int itemId)
        {
            return _unitOfWork.AncestorTransactions.GetByAncestorIdAndItem(ancestorId, itemId);
        }

        public IEnumerable<AncestorTransactionDto> GetTransactionDtosByAncestorIdAndItemId(int ancestorId, int itemId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorTransaction>, IEnumerable<AncestorTransactionDto>>(GetTransactionsByAncestorIdAndItemId(ancestorId, itemId));
        }

        public IEnumerable<Core.Domain.AncestorTransaction> GetTransactionsByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId)
        {
            return _unitOfWork.AncestorTransactions.GetByAncestorIdAndItemAndApplicant(ancestorId, itemId, applicantId);
        }

        public IEnumerable<AncestorTransactionDto> GetTransactionDtosByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorTransaction>, IEnumerable<AncestorTransactionDto>>(GetTransactionsByAncestorIdAndItemIdAndApplicantId(ancestorId, itemId, applicantId));
        }

        public Core.Domain.AncestorTransaction GetLastAncestorTransactionByAncestorId(int ancestorId)
        {
            return _unitOfWork.AncestorTransactions.GetLastAncestorTransactionByAncestorId(ancestorId);
        }

        public Core.Domain.AncestorTransaction GetLastAncestorTransactionByShiftedAncestorId(int ancestorId)
        {
            return _unitOfWork.AncestorTransactions.GetLastAncestorTransactionByShiftedAncestorId(ancestorId);
        }

        protected bool CreateNewTransaction(AncestorTransactionDto ancestorTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.AncestorTransaction();

            Mapper.Map(ancestorTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.AncestorTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(AncestorTransactionDto ancestorTransactionDto)
        {
            var ancestorTransactionInDb = GetTransaction(ancestorTransactionDto.AF);

            Mapper.Map(ancestorTransactionDto, ancestorTransactionInDb);

            ancestorTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }

        protected bool SetDeceasedIdBasedOnAncestorLastTransaction(AncestorTransactionDto ancestorTransactionDto)
        {
            if (_ancestor.HasDeceased())
            {
                var lastTransactionOfAncestor = GetLastAncestorTransactionByAncestorId(_ancestor.GetAncestor().Id);

                if (lastTransactionOfAncestor != null)
                {
                    SetDeceasedIdBasedOnAncestorLastTransaction(lastTransactionOfAncestor, ancestorTransactionDto);
                }
                else
                {
                    var lastTransactionOfShiftedAncestor = GetLastAncestorTransactionByShiftedAncestorId(_ancestor.GetAncestor().Id);

                    SetDeceasedIdBasedOnAncestorLastTransaction(lastTransactionOfShiftedAncestor, ancestorTransactionDto);
                }
            }

            return true;
        }

        private bool SetDeceasedIdBasedOnAncestorLastTransaction(Core.Domain.AncestorTransaction lastAncestorTransaction, AncestorTransactionDto ancestorTransactionDto)
        {
            if (lastAncestorTransaction != null)
            {
                if (lastAncestorTransaction.DeceasedId != null &&
                    _applicantDeceased.GetApplicantDeceased(ancestorTransactionDto.ApplicantId, (int)lastAncestorTransaction.DeceasedId) == null)
                {
                    return false;
                }

                ancestorTransactionDto.DeceasedId = lastAncestorTransaction.DeceasedId;
            }

            return true;
        }




    }
}