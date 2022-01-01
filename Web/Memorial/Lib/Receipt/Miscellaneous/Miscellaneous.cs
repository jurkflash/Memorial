using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Lib.Miscellaneous;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public class Miscellaneous : Receipt, IMiscellaneous
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Miscellaneous(
            IUnitOfWork unitOfWork,
            INumber number
            ) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts(string AF)
        {
            return _unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(AF);
        }

        public IEnumerable<ReceiptDto> GetNonOrderReceiptDtos(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetNonOrderReceipts(AF));
        }

        public string GetApplicationAF()
        {
            return _receipt.MiscellaneousTransactionAF;
        }

        override
        public void NewNumber(int itemId)
        {
            _reNumber = _number.GetNewRE(itemId, System.DateTime.Now.Year);
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
                receipt.DeletedDate = System.DateTime.Now;
            }

            return true;
        }
    }
}