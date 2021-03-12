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
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IQuadrangle _quadrangle;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected INumber _number;
        protected Core.Domain.QuadrangleTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            IQuadrangle quadrangle, 
            IApplicant applicant,
            IDeceased deceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _number = number;
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
            return _transaction.QuadrangleItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
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

        protected bool CreateNewTransaction(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.QuadrangleTransaction();

            Mapper.Map(quadrangleTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.QuadrangleTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(QuadrangleTransactionDto quadrangleTransactionDto)
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




            //if (IsOrder())
            //{
            //    _invoice.DeleteByApplication(GetAF());
            //}
            //else
            //{
            //    _receipt.SetTransaction(_transaction.AF);
            //    var receipts = _receipt.GetNonOrderReceipts();
            //    foreach (var receipt in receipts)
            //    {
            //        _receipt.SetReceipt(receipt.RE);
            //        _receipt.Delete();
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