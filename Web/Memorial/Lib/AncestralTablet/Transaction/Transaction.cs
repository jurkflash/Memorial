﻿using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
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
        protected Core.Domain.AncestralTabletTransaction _transaction;
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
            _transaction = _unitOfWork.AncestralTabletTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.AncestralTabletTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.AncestralTabletTransaction GetTransaction()
        {
            return _transaction;
        }

        public AncestralTabletTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.AncestralTabletTransaction, AncestralTabletTransactionDto>(GetTransaction());
        }

        public Core.Domain.AncestralTabletTransaction GetTransaction(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetActive(AF);
        }

        public Core.Domain.AncestralTabletTransaction GetTransactionExclusive(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetExclusive(AF);
        }

        public AncestralTabletTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.AncestralTabletTransaction, AncestralTabletTransactionDto>(GetTransaction(AF));
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
            return _transaction.AncestralTabletItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.AncestralTabletItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.AncestralTabletItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.AncestralTabletItemId);
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

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestorIdAndItemId(int ancestorId, int itemId, string filter)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestorIdAndItem(ancestorId, itemId, filter);
        }

        public IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestorIdAndItemId(int ancestorId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletTransaction>, IEnumerable<AncestralTabletTransactionDto>>(GetTransactionsByAncestorIdAndItemId(ancestorId, itemId, filter));
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestorIdAndItemAndApplicant(ancestorId, itemId, applicantId);
        }

        public IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestorIdAndItemIdAndApplicantId(int ancestorId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletTransaction>, IEnumerable<AncestralTabletTransactionDto>>(GetTransactionsByAncestorIdAndItemIdAndApplicantId(ancestorId, itemId, applicantId));
        }

        public Core.Domain.AncestralTabletTransaction GetTransactionsByShiftedAncestralTabletTransactionAF(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByShiftedAncestralTabletTransactionAF(AF);
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestorId(int nicheId)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestorId(nicheId);
        }

        protected bool CreateNewTransaction(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.AncestralTabletTransaction();

            Mapper.Map(ancestralTabletTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.AncestralTabletTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            var ancestralTabletTransactionInDb = GetTransaction(ancestralTabletTransactionDto.AF);

            Mapper.Map(ancestralTabletTransactionDto, ancestralTabletTransactionInDb);

            ancestralTabletTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteAllTransactionWithSameAncestorId()
        {
            var datetimeNow = System.DateTime.Now;

            var transactions = GetTransactionsByAncestorId(_transaction.AncestorId);

            foreach (var transaction in transactions)
            {
                transaction.DeleteDate = datetimeNow;
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnAncestor(AncestralTabletTransactionDto ancestralTabletTransactionDto, int ancestorId)
        {
            _ancestor.SetAncestor(ancestorId);

            if (_ancestor.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByAncestorId(ancestorId);

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(ancestralTabletTransactionDto.ApplicantId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    ancestralTabletTransactionDto.DeceasedId = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool ChangeAncestor(string systemCode, int oldAncestorId, int newAncestorId)
        {
            var areaId = _ancestor.GetAncestor(oldAncestorId).AncestralTabletAreaId;

            var itemId = _item.GetItemByArea(areaId).Where(i => i.SystemCode == systemCode).FirstOrDefault();

            if (itemId == null)
                return false;

            var transactions = GetTransactionsByAncestorIdAndItemId(oldAncestorId, itemId.Id, null);

            foreach (var transaction in transactions)
            {
                transaction.AncestorId = newAncestorId;
            }

            return true;
        }
    }
}