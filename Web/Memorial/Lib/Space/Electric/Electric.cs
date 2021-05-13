﻿using Memorial.Core;
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
    public class Electric : Transaction, IElectric
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.ISpace _invoice;
        private readonly IPayment _payment;

        public Electric(
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

        public void SetElectric(string AF)
        {
            SetTransaction(AF);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(SpaceTransactionDto spaceTransactionDto)
        {
            NewNumber(spaceTransactionDto.SpaceItemId);

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

    }
}