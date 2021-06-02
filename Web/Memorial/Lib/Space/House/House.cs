using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public class House : Transaction, IHouse
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.ISpace _invoice;
        private readonly IPayment _payment;

        public House(
            IUnitOfWork unitOfWork,
            IItem item,
            ISpace space,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.ISpace invoice,
            IPayment payment
            ) : 
            base(
                unitOfWork, 
                item,
                space, 
                applicant, 
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _space = space;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
        }

        public void SetHouse(string AF)
        {
            SetTransaction(AF);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(SpaceTransactionDto spaceTransactionDto)
        {
            NewNumber(spaceTransactionDto.SpaceItemDtoId);

            SummaryItem(spaceTransactionDto);

            if (CreateNewTransaction(spaceTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(SpaceTransactionDto spaceTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(spaceTransactionDto.AF).Any() && spaceTransactionDto.Amount < 
                _invoice.GetInvoicesByAF(spaceTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var spaceTransactionInDb = GetTransaction(spaceTransactionDto.AF);

            SummaryItem(spaceTransactionDto);

            if (UpdateTransaction(spaceTransactionDto))
            {
                _unitOfWork.Complete();
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

        private void SummaryItem(SpaceTransactionDto trx)
        {
            trx.SummaryItem = "AF: " + (string.IsNullOrEmpty(trx.AF) ? _AFnumber : trx.AF) + "<BR/>" +
                Resources.Mix.Remark + ": " + trx.Remark;
        }

    }
}