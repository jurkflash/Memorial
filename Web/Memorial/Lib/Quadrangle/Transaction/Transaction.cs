using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Quadrangle
{
    public abstract class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IQuadrangle _quadrangle;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected Core.Domain.QuadrangleTransaction _transaction;
        protected string _number;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            IQuadrangle quadrangle, 
            IApplicant applicant,
            IDeceased deceased)
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.QuadrangleTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.QuadrangleTransaction transaction)
        {
            _transaction = transaction;
        }

        private void SetQuadrangle()
        {
            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            SetQuadrangle();
            return _quadrangle.GetQuadrangle();
        }

        public QuadrangleDto DtoGetQuadrangle()
        {
            return _quadrangle.DtoGetQuadrangle();
        }

        public float GetAmount()
        {
            return _transaction.Price;
        }

        public Core.Domain.QuadrangleTransaction GetTransaction()
        {
            return _transaction;
        }

        public QuadrangleTransactionDto DtoGetTransaction()
        {
            return Mapper.Map<Core.Domain.QuadrangleTransaction, QuadrangleTransactionDto>(GetTransaction());
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
            return _item.GetName();
        }

        public int GetApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public Core.Domain.Applicant GetApplicant()
        {
            _applicant.SetApplicant(_transaction.ApplicantId);
            return _applicant.GetApplicant();
        }

        public ApplicantDto DtoGetApplicant()
        {
            return _applicant.DtosGetApplicant();
        }

        public int? GetDeceasedId()
        {
            return _transaction.DeceasedId;
        }

        public Core.Domain.Applicant GetDeceased()
        {
            _deceased.SetDeceased(_transaction.ApplicantId);
            return _applicant.GetApplicant();
        }

        public DeceasedDto DtoGetDeceased()
        {
            return _deceased.DtoGetDeceased();
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetByQuadrangleIdAndItem(int quadrangleId, int itemId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItem(quadrangleId, itemId);
        }

        public IEnumerable<QuadrangleTransactionDto> DtosGetByQuadrangleIdAndItem(int quadrangleId, int itemId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetByQuadrangleIdAndItem(quadrangleId, itemId));
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId);
        }

        public IEnumerable<QuadrangleTransactionDto> DtosGetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId));
        }

        protected void NewNumber()
        {
            INumber quadrangleNumber = new Number(_unitOfWork);
            _number = quadrangleNumber.GetNewAF(_transaction.QuadrangleItemId, System.DateTime.Now.Year);
        }

        protected bool CreateNewTransaction()
        {
            NewNumber();

            if (_number == "")
                return false;

            _transaction.AF = _number;
            _transaction.CreateDate = System.DateTime.Now;
            _unitOfWork.QuadrangleTransactions.Add(_transaction);
            return true;
        }

        public float GetUnpaidNonOrderAmount()
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return GetAmount() -
                receipt.GetDtosByAF(_transaction.AF, Core.Domain.MasterCatalog.Quadrangle).Sum(r => r.Amount);
        }

        public bool Delete()
        {
            IItem quadrangleItem = new Item(_unitOfWork);
            quadrangleItem.SetItem(_transaction.QuadrangleItemId);

            ICommon common = new Lib.Common(_unitOfWork);
            if (common.DeleteForm(_transaction.AF, Core.Domain.MasterCatalog.Quadrangle))
            {
                if (quadrangleItem.GetSystemCode() == "Order")
                {
                    _deceased.SetById((int)_transaction.DeceasedId);
                    _deceased.RemoveQuadrangle();
                    _unitOfWork.Complete();

                    _quadrangle.SetQuadrangle(_transaction.QuadrangleId);
                    _quadrangle.SetHasDeceased(_deceased.GetByQuadrangle(_transaction.QuadrangleId).Any());
                    _quadrangle.RemoveApplicant();
                    _unitOfWork.Complete();
                }
                return true;
            }
            else
                return false;
        }
    }
}