using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Urn
{
    public class Purchase : Transaction, IPurchase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IUrn _invoice;
        private readonly IUrnPayment _payment;

        public Purchase(
            IUnitOfWork unitOfWork,
            IItem item,
            IUrn urn,
            IApplicant applicant,
            INumber number,
            Invoice.IUrn invoice,
            IUrnPayment payment
            ) : 
            base(
                unitOfWork, 
                item, 
                urn, 
                applicant, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _urn = urn;
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

        public bool Create(UrnTransactionDto urnTransactionDto)
        {
            NewNumber(urnTransactionDto.UrnItemId);

            if (CreateNewTransaction(urnTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(UrnTransactionDto urnTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(urnTransactionDto.AF).Any() && urnTransactionDto.Price < 
                _invoice.GetInvoicesByAF(urnTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            if (UpdateTransaction(urnTransactionDto))
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

    }
}