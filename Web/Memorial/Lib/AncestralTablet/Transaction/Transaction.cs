using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IAncestralTablet _ancestralTablet;
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
            IAncestralTablet ancestralTablet, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestralTablet = ancestralTablet;
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

        public int GetTransactionAncestralTabletId()
        {
            return _transaction.AncestralTabletId;
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

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestralTabletIdAndItemId(int ancestralTabletId, int itemId, string filter)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestralTabletIdAndItem(ancestralTabletId, itemId, filter);
        }

        public IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestralTabletIdAndItemId(int ancestralTabletId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletTransaction>, IEnumerable<AncestralTabletTransactionDto>>(GetTransactionsByAncestralTabletIdAndItemId(ancestralTabletId, itemId, filter));
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestralTabletIdAndItemIdAndApplicantId(int ancestralTabletId, int itemId, int applicantId)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestralTabletIdAndItemAndApplicant(ancestralTabletId, itemId, applicantId);
        }

        public IEnumerable<AncestralTabletTransactionDto> GetTransactionDtosByAncestralTabletIdAndItemIdAndApplicantId(int ancestralTabletId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletTransaction>, IEnumerable<AncestralTabletTransactionDto>>(GetTransactionsByAncestralTabletIdAndItemIdAndApplicantId(ancestralTabletId, itemId, applicantId));
        }

        public Core.Domain.AncestralTabletTransaction GetTransactionsByShiftedAncestralTabletTransactionAF(string AF)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByShiftedAncestralTabletTransactionAF(AF);
        }

        public IEnumerable<Core.Domain.AncestralTabletTransaction> GetTransactionsByAncestralTabletId(int nicheId)
        {
            return _unitOfWork.AncestralTabletTransactions.GetByAncestralTabletId(nicheId);
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

        protected bool DeleteAllTransactionWithSameAncestralTabletId()
        {
            var datetimeNow = System.DateTime.Now;

            var transactions = GetTransactionsByAncestralTabletId(_transaction.AncestralTabletId);

            foreach (var transaction in transactions)
            {
                transaction.DeleteDate = datetimeNow;
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnAncestralTablet(AncestralTabletTransactionDto ancestralTabletTransactionDto, int ancestralTabletId)
        {
            _ancestralTablet.SetAncestralTablet(ancestralTabletId);

            if (_ancestralTablet.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByAncestralTabletId(ancestralTabletId);

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

        protected bool ChangeAncestralTablet(string systemCode, int oldAncestralTabletId, int newAncestralTabletId)
        {
            var areaId = _ancestralTablet.GetAncestralTablet(oldAncestralTabletId).AncestralTabletAreaId;

            var itemId = _item.GetItemByArea(areaId).Where(i => i.SystemCode == systemCode).FirstOrDefault();

            if (itemId == null)
                return false;

            var transactions = GetTransactionsByAncestralTabletIdAndItemId(oldAncestralTabletId, itemId.Id, null);

            foreach (var transaction in transactions)
            {
                transaction.AncestralTabletId = newAncestralTabletId;
            }

            return true;
        }
    }
}