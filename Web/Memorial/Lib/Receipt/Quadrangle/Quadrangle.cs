using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Lib.Quadrangle;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public class Quadrangle : Receipt, IQuadrangle
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Quadrangle(
            IUnitOfWork unitOfWork, 
            INumber number
            ) : base( unitOfWork )
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts(string AF)
        {
            return _unitOfWork.Receipts.GetByNonOrderActiveQuadrangleAF(AF);
        }

        public IEnumerable<ReceiptDto> GetNonOrderReceiptDtos(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetNonOrderReceipts(AF));
        }

        public string GetApplicationAF()
        {
            return _receipt.QuadrangleTransactionAF;
        }

        override
        public void NewNumber(int itemId)
        {
            _reNumber = _number.GetNewIV(itemId, System.DateTime.Now.Year);
        }

        public float GetTotalIssuedNonOrderReceiptAmount(string AF)
        {
            return GetNonOrderReceipts(AF).Sum(r => r.Amount);
        }

        public bool Create(int itemId, ReceiptDto receiptDto)
        {
            NewNumber(itemId);

            CreateNewReceipt(receiptDto);

            return true;
        }

        public bool Update(ReceiptDto receiptDto)
        {
            UpdateReceipt(receiptDto);

            return true;
        }

        public bool Delete()
        {
            DeleteReceipt();

            return true;
        }

        public bool DeleteNonOrderReceiptsByApplicationAF(string AF)
        {
            var receipts = GetNonOrderReceipts(AF);
            foreach (var receipt in receipts)
            {
                receipt.DeleteDate = System.DateTime.Now;
            }

            return true;
        }

    }
}