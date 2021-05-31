using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INiche _niche;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;
        protected Core.Domain.ColumbariumTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            INiche niche, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _niche = niche;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.ColumbariumTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.ColumbariumTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.ColumbariumTransaction GetTransaction()
        {
            return _transaction;
        }

        public ColumbariumTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.ColumbariumTransaction, ColumbariumTransactionDto>(GetTransaction());
        }

        public Core.Domain.ColumbariumTransaction GetTransaction(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetActive(AF);
        }

        public Core.Domain.ColumbariumTransaction GetTransactionExclusive(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetExclusive(AF);
        }

        public ColumbariumTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.ColumbariumTransaction, ColumbariumTransactionDto>(GetTransaction(AF));
        }

        public string GetTransactionAF()
        {
            return _transaction.AF;
        }

        public float GetTransactionTotalAmount()
        {
            return _transaction.Price + 
                (_transaction.Maintenance == null ? 0 : (float)_transaction.Maintenance) + 
                (_transaction.LifeTimeMaintenance == null ? 0 : (float)_transaction.LifeTimeMaintenance);
        }

        public string GetTransactionSummaryItem()
        {
            return _transaction.SummaryItem;
        }

        public int GetTransactionNicheId()
        {
            return _transaction.NicheId;
        }

        public int GetItemId()
        {
            return _transaction.ColumbariumItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.ColumbariumItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.ColumbariumItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.ColumbariumItemId);
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

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByNicheIdAndItemId(int nicheId, int itemId, string filter)
        {
            return _unitOfWork.ColumbariumTransactions.GetByNicheIdAndItem(nicheId, itemId, filter);
        }

        public IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByNicheIdAndItemId(int nicheId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumTransaction>, IEnumerable<ColumbariumTransactionDto>>(GetTransactionsByNicheIdAndItemId(nicheId, itemId, filter));
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByNicheIdAndItemIdAndApplicantId(int nicheId, int itemId, int applicantId)
        {
            return _unitOfWork.ColumbariumTransactions.GetByNicheIdAndItemAndApplicant(nicheId, itemId, applicantId);
        }

        public IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByNicheIdAndItemIdAndApplicantId(int nicheId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumTransaction>, IEnumerable<ColumbariumTransactionDto>>(GetTransactionsByNicheIdAndItemIdAndApplicantId(nicheId, itemId, applicantId));
        }

        public Core.Domain.ColumbariumTransaction GetTransactionsByShiftedColumbariumTransactionAF(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetByShiftedColumbariumTransactionAF(AF);
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByNicheId (int nicheId)
        {
            return _unitOfWork.ColumbariumTransactions.GetByNicheId(nicheId);
        }

        protected bool CreateNewTransaction(ColumbariumTransactionDto columbariumTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.ColumbariumTransaction();

            Mapper.Map(columbariumTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.ColumbariumTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(ColumbariumTransactionDto columbariumTransactionDto)
        {
            var columbariumTransactionInDb = GetTransaction(columbariumTransactionDto.AF);

            Mapper.Map(columbariumTransactionDto, columbariumTransactionInDb);

            columbariumTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteAllTransactionWithSameNicheId()
        {
            var datetimeNow = System.DateTime.Now;

            var transactions = GetTransactionsByNicheId(_transaction.NicheId);

            foreach(var transaction in transactions)
            {
                transaction.DeleteDate = datetimeNow;
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnNiche(ColumbariumTransactionDto columbariumTransactionDto, int nicheId)
        {
            _niche.SetNiche(nicheId);

            if (_niche.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByNicheId(nicheId);

                if (_niche.GetNumberOfPlacement() < deceaseds.Count())
                    return false;

                if (deceaseds.Count() > 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(columbariumTransactionDto.ApplicantDtoId, deceaseds.ElementAt(1).Id) == null)
                    {
                        return false;
                    }

                    columbariumTransactionDto.DeceasedDto2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(columbariumTransactionDto.ApplicantDtoId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    columbariumTransactionDto.DeceasedDto1Id = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool ChangeNiche(string systemCode, int oldNicheId, int newNicheId)
        {
            var centreId = _niche.GetNiche(oldNicheId).ColumbariumArea.ColumbariumCentreId;

            var itemId = _item.GetItemByCentre(centreId).Where(i => i.SystemCode == systemCode).FirstOrDefault();

            if (itemId == null)
                return false;

            var transactions = GetTransactionsByNicheIdAndItemId(oldNicheId, itemId.Id, null);

            foreach (var transaction in transactions)
            {
                transaction.NicheId = newNicheId;
            }

            return true;
        }
    }
}