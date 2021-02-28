using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Receipt;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
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
        protected INumber _number;
        protected Invoice.IQuadrangle _quadrangleInvoice;
        protected Receipt.IQuadrangle _quadrangleReceipt;
        protected Core.Domain.QuadrangleTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            IQuadrangle quadrangle, 
            IApplicant applicant,
            IDeceased deceased,
            INumber number,
            Invoice.IQuadrangle quadrangleInvoice,
            Receipt.IQuadrangle quadrangleReceipt)
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _number = number;
            _quadrangleInvoice = quadrangleInvoice;
            _quadrangleReceipt = quadrangleReceipt;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.QuadrangleTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.QuadrangleTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.QuadrangleTransaction GetTransaction()
        {
            return _transaction;
        }

        public QuadrangleTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.QuadrangleTransaction, QuadrangleTransactionDto>(GetTransaction());
        }

        public Core.Domain.QuadrangleTransaction GetTransaction(string AF)
        {
            return _unitOfWork.QuadrangleTransactions.GetActive(AF);
        }

        public QuadrangleTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.QuadrangleTransaction, QuadrangleTransactionDto>(GetTransaction(AF));
        }

        public string GetAF()
        {
            return _transaction.AF;
        }

        public float GetAmount()
        {
            return _transaction.Price;
        }

        public int GetQuadrangleId()
        {
            return _transaction.QuadrangleId;
        }

        public int GetItemId()
        {
            return _transaction.QuadrangleItemId;
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

        public int? GetDeceasedId()
        {
            return _transaction.DeceasedId;
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemId(int quadrangleId, int itemId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItem(quadrangleId, itemId);
        }

        public IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemId(int quadrangleId, int itemId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetTransactionsByQuadrangleIdAndItemId(quadrangleId, itemId));
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId);
        }

        public IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(quadrangleId, itemId, applicantId));
        }

        protected void NewNumber()
        {
            _AFnumber = _number.GetNewAF(_transaction.QuadrangleItemId, System.DateTime.Now.Year);
        }

        protected bool CreateNewTransaction()
        {
            NewNumber();

            if (_AFnumber == "")
                return false;

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;
            _unitOfWork.QuadrangleTransactions.Add(_transaction);
            return true;
        }

        public float GetUnpaidNonOrderAmount()
        {
            return GetAmount() - _quadrangleReceipt.GetTotalIssuedNonOrderReceiptAmount();
        }

        abstract
        public bool Delete();
            //if (IsOrder())
            //{
            //    _quadrangleInvoice.DeleteByApplication(GetAF());
            //}
            //else
            //{
            //    _quadrangleReceipt.SetTransaction(_transaction.AF);
            //    var receipts = _quadrangleReceipt.GetNonOrderReceipts();
            //    foreach (var receipt in receipts)
            //    {
            //        _quadrangleReceipt.SetReceipt(receipt.RE);
            //        _quadrangleReceipt.Delete();
            //    }
            //}










        //public bool Delete()
        //{
        //    IItem quadrangleItem = new Item(_unitOfWork);
        //    quadrangleItem.SetItem(_transaction.QuadrangleItemId);

        //    ICommon common = new Lib.Common(_unitOfWork);
        //    if (common.DeleteForm(_transaction.AF, Core.Domain.MasterCatalog.Quadrangle))
        //    {
        //        if (quadrangleItem.GetSystemCode() == "Order")
        //        {
        //            _deceased.SetById((int)_transaction.DeceasedId);
        //            _deceased.RemoveQuadrangle();
        //            _unitOfWork.Complete();

        //            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);
        //            _quadrangle.SetHasDeceased(_deceased.GetByQuadrangle(_transaction.QuadrangleId).Any());
        //            _quadrangle.RemoveApplicant();
        //            _unitOfWork.Complete();
        //        }
        //        return true;
        //    }
        //    else
        //        return false;
        //}
    }
}