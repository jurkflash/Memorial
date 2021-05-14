﻿using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IAncestor _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestor ancestor,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IAncestor invoice,
            IPayment payment,
            ITracking tracking
            ) : 
            base(
                unitOfWork, 
                item, 
                ancestor, 
                applicant, 
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestor = ancestor;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
            _tracking = tracking;
        }

        public void SetOrder(string AF)
        {
            SetTransaction(AF);
        }

        private void SetDeceased(int id)
        {
            _deceased.SetDeceased(id);
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        public bool Create(AncestorTransactionDto ancestorTransactionDto)
        {
            if (ancestorTransactionDto.DeceasedId != null)
            {
                SetDeceased((int)ancestorTransactionDto.DeceasedId);
                if (_deceased.GetAncestor() != null)
                    return false;
            }

            NewNumber(ancestorTransactionDto.AncestorItemId);

            if (CreateNewTransaction(ancestorTransactionDto))
            {
                _ancestor.SetAncestor(ancestorTransactionDto.AncestorId);
                _ancestor.SetApplicant(ancestorTransactionDto.ApplicantId);

                if (ancestorTransactionDto.DeceasedId != null)
                {
                    SetDeceased((int)ancestorTransactionDto.DeceasedId);
                    if (_deceased.SetAncestor(ancestorTransactionDto.AncestorId))
                    {
                        _ancestor.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _tracking.Add(ancestorTransactionDto.AncestorId, _AFnumber, ancestorTransactionDto.ApplicantId, ancestorTransactionDto.DeceasedId);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(AncestorTransactionDto ancestorTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(ancestorTransactionDto.AF).Any() && ancestorTransactionDto.Price + (float)ancestorTransactionDto.Maintenance < 
                _invoice.GetInvoicesByAF(ancestorTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var ancestorTransactionInDb = GetTransaction(ancestorTransactionDto.AF);

            var deceased1InDb = ancestorTransactionInDb.DeceasedId;

            if (UpdateTransaction(ancestorTransactionDto))
            {
                _ancestor.SetAncestor(ancestorTransactionDto.AncestorId);

                AncestorApplicantDeceaseds(ancestorTransactionDto.DeceasedId, deceased1InDb);

                if (ancestorTransactionDto.DeceasedId == null)
                    _ancestor.SetHasDeceased(false);

                _tracking.Change(ancestorTransactionDto.AncestorId, ancestorTransactionDto.AF, ancestorTransactionDto.ApplicantId, ancestorTransactionDto.DeceasedId);

                _unitOfWork.Complete();
            }

            return true;
        }

        private bool AncestorApplicantDeceaseds(int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    _deceased.SetDeceased((int)dbDeceasedId);
                    _deceased.RemoveAncestor();
                }
                else
                {
                    _deceased.SetDeceased((int)deceasedId);
                    if (_deceased.GetAncestor() != null && _deceased.GetAncestor().Id != _ancestor.GetAncestor().Id)
                    {
                        return false;
                    }
                    else
                    {
                        _deceased.SetAncestor(_ancestor.GetAncestor().Id);
                        _ancestor.SetHasDeceased(true);
                    }
                }
            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.AncestorId, _transaction.AF))
                return false;

            DeleteAllTransactionWithSameAncestorId();

            _ancestor.SetAncestor(_transaction.AncestorId);

            var deceaseds = _deceased.GetDeceasedsByAncestorId(_transaction.AncestorId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveAncestor();
            }

            _ancestor.SetHasDeceased(false);

            _ancestor.RemoveApplicant();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _tracking.Delete(_transaction.AF);

            _unitOfWork.Complete();

            return true;
        }

    }
}