using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IAncestralTablet _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IWithdraw _withdraw;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IAncestralTablet ancestralTablet,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IAncestralTablet invoice,
            IPayment payment,
            ITracking tracking,
            IWithdraw withdraw
            ) : 
            base(
                unitOfWork, 
                item, 
                ancestralTablet, 
                applicant, 
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _ancestralTablet = ancestralTablet;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
            _tracking = tracking;
            _withdraw = withdraw;
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

        public bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            if (ancestralTabletTransactionDto.DeceasedId != null)
            {
                SetDeceased((int)ancestralTabletTransactionDto.DeceasedId);
                if (_deceased.GetAncestralTablet() != null)
                    return false;
            }

            NewNumber(ancestralTabletTransactionDto.AncestralTabletItemId);

            if (CreateNewTransaction(ancestralTabletTransactionDto))
            {
                _ancestralTablet.SetAncestralTablet(ancestralTabletTransactionDto.AncestralTabletId);
                _ancestralTablet.SetApplicant(ancestralTabletTransactionDto.ApplicantId);

                if (ancestralTabletTransactionDto.DeceasedId != null)
                {
                    SetDeceased((int)ancestralTabletTransactionDto.DeceasedId);
                    if (_deceased.SetAncestralTablet(ancestralTabletTransactionDto.AncestralTabletId))
                    {
                        _ancestralTablet.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _tracking.Add(ancestralTabletTransactionDto.AncestralTabletId, _AFnumber, ancestralTabletTransactionDto.ApplicantId, ancestralTabletTransactionDto.DeceasedId);

                _withdraw.RemoveWithdrew(ancestralTabletTransactionDto.AncestralTabletId);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(ancestralTabletTransactionDto.AF).Any() && ancestralTabletTransactionDto.Price + (float)ancestralTabletTransactionDto.Maintenance < 
                _invoice.GetInvoicesByAF(ancestralTabletTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var ancestralTabletTransactionInDb = GetTransaction(ancestralTabletTransactionDto.AF);

            var deceased1InDb = ancestralTabletTransactionInDb.DeceasedId;

            if (UpdateTransaction(ancestralTabletTransactionDto))
            {
                _ancestralTablet.SetAncestralTablet(ancestralTabletTransactionDto.AncestralTabletId);

                AncestralTabletApplicantDeceaseds(ancestralTabletTransactionDto.DeceasedId, deceased1InDb);

                if (ancestralTabletTransactionDto.DeceasedId == null)
                    _ancestralTablet.SetHasDeceased(false);

                _tracking.Change(ancestralTabletTransactionDto.AncestralTabletId, ancestralTabletTransactionDto.AF, ancestralTabletTransactionDto.ApplicantId, ancestralTabletTransactionDto.DeceasedId);

                _unitOfWork.Complete();
            }

            return true;
        }

        private bool AncestralTabletApplicantDeceaseds(int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    _deceased.SetDeceased((int)dbDeceasedId);
                    _deceased.RemoveAncestralTabletDeceased();
                }
                else
                {
                    _deceased.SetDeceased((int)deceasedId);
                    if (_deceased.GetAncestralTablet() != null && _deceased.GetAncestralTablet().Id != _ancestralTablet.GetAncestralTablet().Id)
                    {
                        return false;
                    }
                    else
                    {
                        _deceased.SetAncestralTablet(_ancestralTablet.GetAncestralTablet().Id);
                        _ancestralTablet.SetHasDeceased(true);
                    }
                }
            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.AncestralTabletId, _transaction.AF))
                return false;

            DeleteAllTransactionWithSameAncestralTabletId();

            _ancestralTablet.SetAncestralTablet(_transaction.AncestralTabletId);

            var deceaseds = _deceased.GetDeceasedsByAncestralTabletId(_transaction.AncestralTabletId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveAncestralTabletDeceased();
            }

            _ancestralTablet.SetHasDeceased(false);

            _ancestralTablet.RemoveApplicant();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _tracking.Delete(_transaction.AF);

            _unitOfWork.Complete();

            return true;
        }

    }
}