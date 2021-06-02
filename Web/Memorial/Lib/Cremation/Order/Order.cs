using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cremation
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.ICremation _invoice;
        private readonly IPayment _payment;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            ICremation cremation,
            IApplicant applicant,
            INumber number,
            Invoice.ICremation invoice,
            IPayment payment
            ) : 
            base(
                unitOfWork, 
                item,
                cremation, 
                applicant, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _cremation = cremation;
            _applicant = applicant;
            _number = number;
            _invoice = invoice;
            _payment = payment;
        }

        public void SetOrder(string AF)
        {
            SetTransaction(AF);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(CremationTransactionDto cremationTransactionDto)
        {
            NewNumber(cremationTransactionDto.CremationItemDtoId);

            SummaryItem(cremationTransactionDto);

            if (CreateNewTransaction(cremationTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(CremationTransactionDto cremationTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(cremationTransactionDto.AF).Any() && cremationTransactionDto.Price < 
                _invoice.GetInvoicesByAF(cremationTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            SummaryItem(cremationTransactionDto);

            if (UpdateTransaction(cremationTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Delete()
        {
            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(CremationTransactionDto trx)
        {
            trx.SummaryItem = "AF: " + (string.IsNullOrEmpty(trx.AF) ? _AFnumber : trx.AF) + "<BR/>" +
                Resources.Mix.CremateDate + ": " + trx.CremateDate.ToString("yyyy-MMM-dd HH:mm") + "<BR/>" +
                Resources.Mix.Remark + ": " + trx.Remark;
        }

    }
}