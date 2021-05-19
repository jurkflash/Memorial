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

namespace Memorial.Lib.Columbarium
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IQuadrangle _quadrangle;
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
            IQuadrangle quadrangle, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
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

        public float GetTransactionAmount()
        {
            return _transaction.Price + 
                (_transaction.Maintenance == null ? 0 : (float)_transaction.Maintenance) + 
                (_transaction.LifeTimeMaintenance == null ? 0 : (float)_transaction.LifeTimeMaintenance);
        }

        public int GetTransactionQuadrangleId()
        {
            return _transaction.QuadrangleId;
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

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByQuadrangleIdAndItemId(int quadrangleId, int itemId, string filter)
        {
            return _unitOfWork.ColumbariumTransactions.GetByQuadrangleIdAndItem(quadrangleId, itemId, filter);
        }

        public IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByQuadrangleIdAndItemId(int quadrangleId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumTransaction>, IEnumerable<ColumbariumTransactionDto>>(GetTransactionsByQuadrangleIdAndItemId(quadrangleId, itemId, filter));
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId)
        {
            return _unitOfWork.ColumbariumTransactions.GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId);
        }

        public IEnumerable<ColumbariumTransactionDto> GetTransactionDtosByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumTransaction>, IEnumerable<ColumbariumTransactionDto>>(GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(quadrangleId, itemId, applicantId));
        }

        public Core.Domain.ColumbariumTransaction GetTransactionsByShiftedQuadrangleTransactionAF(string AF)
        {
            return _unitOfWork.ColumbariumTransactions.GetByShiftedColumbariumTransactionAF(AF);
        }

        public IEnumerable<Core.Domain.ColumbariumTransaction> GetTransactionsByQuadrangleId (int quadrangleId)
        {
            return _unitOfWork.ColumbariumTransactions.GetByQuadrangleId(quadrangleId);
        }

        protected bool CreateNewTransaction(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.ColumbariumTransaction();

            Mapper.Map(quadrangleTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.ColumbariumTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            var quadrangleTransactionInDb = GetTransaction(quadrangleTransactionDto.AF);

            Mapper.Map(quadrangleTransactionDto, quadrangleTransactionInDb);

            quadrangleTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteAllTransactionWithSameQuadrangleId()
        {
            var datetimeNow = System.DateTime.Now;

            var transactions = GetTransactionsByQuadrangleId(_transaction.QuadrangleId);

            foreach(var transaction in transactions)
            {
                transaction.DeleteDate = datetimeNow;
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnQuadrangle(ColumbariumTransactionDto quadrangleTransactionDto, int quadrangleId)
        {
            _quadrangle.SetQuadrangle(quadrangleId);

            if (_quadrangle.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByQuadrangleId(quadrangleId);

                if (_quadrangle.GetNumberOfPlacement() < deceaseds.Count())
                    return false;

                if (deceaseds.Count() > 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(quadrangleTransactionDto.ApplicantId, deceaseds.ElementAt(1).Id) == null)
                    {
                        return false;
                    }

                    quadrangleTransactionDto.Deceased2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetApplicantDeceased(quadrangleTransactionDto.ApplicantId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    quadrangleTransactionDto.Deceased1Id = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }

        protected bool ChangeQuadrangle(string systemCode, int oldQuadrangleId, int newQuadrangleId)
        {
            var centreId = _quadrangle.GetQuadrangle(oldQuadrangleId).QuadrangleArea.QuadrangleCentreId;

            var itemId = _item.GetItemByCentre(centreId).Where(i => i.SystemCode == systemCode).FirstOrDefault();

            if (itemId == null)
                return false;

            var transactions = GetTransactionsByQuadrangleIdAndItemId(oldQuadrangleId, itemId.Id, null);

            foreach (var transaction in transactions)
            {
                transaction.QuadrangleId = newQuadrangleId;
            }

            return true;
        }
    }
}