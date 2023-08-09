using Memorial.Core;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using AutoMapper;

namespace Memorial.Lib.Receipt
{
    public class Columbarium : Receipt, IColumbarium
    {
        private readonly IUnitOfWork _unitOfWork;
        protected INumber _number;

        public Columbarium(
            IUnitOfWork unitOfWork, 
            INumber number
            ) : base( unitOfWork )
        {
            _unitOfWork = unitOfWork;
            _number = number;
        }

        public IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts(string AF)
        {
            return _unitOfWork.Receipts.GetByColumbariumAF(AF, false);
        }

        public IEnumerable<ReceiptDto> GetNonOrderReceiptDtos(string AF)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(GetNonOrderReceipts(AF));
        }

        public string GetApplicationAF()
        {
            return _receipt.ColumbariumTransactionAF;
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
                _unitOfWork.Receipts.Remove(receipt);
            }

            return true;
        }

    }
}