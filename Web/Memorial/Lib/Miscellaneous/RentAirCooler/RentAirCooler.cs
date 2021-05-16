using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public class RentAirCooler : Transaction, IRentAirCooler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IMiscellaneous _invoice;
        private readonly IPayment _payment;

        public RentAirCooler(
            IUnitOfWork unitOfWork,
            IItem item,
            IMiscellaneous miscellaneous,
            IApplicant applicant,
            INumber number,
            Invoice.IMiscellaneous invoice,
            IPayment payment
            ) : 
            base(
                unitOfWork, 
                item,
                miscellaneous, 
                applicant, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _miscellaneous = miscellaneous;
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

        public bool Create(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            NewNumber(miscellaneousTransactionDto.MiscellaneousItemDtoId);

            if (CreateNewTransaction(miscellaneousTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(miscellaneousTransactionDto.AF).Any() && miscellaneousTransactionDto.Amount < 
                _invoice.GetInvoicesByAF(miscellaneousTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            if (UpdateTransaction(miscellaneousTransactionDto))
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