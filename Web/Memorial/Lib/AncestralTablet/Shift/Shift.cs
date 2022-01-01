﻿using Memorial.Core;
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
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IAncestralTablet _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IMaintenance _maintenance;

        public Shift(
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
            IMaintenance maintenance
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
            _maintenance = maintenance;
        }

        public void SetShift(string AF)
        {
            SetTransaction(AF);
        }

        public float GetPrice(int itemId)
        {
            _item.SetItem(itemId);
            return _item.GetPrice();
        }

        public void NewNumber(int itemId)
        {
            _AFnumber = _number.GetNewAF(itemId, System.DateTime.Now.Year);
        }

        private bool ShiftAncestralTabletApplicantDeceaseds(int oldQuadranlgeId, int newAncestralTabletId, int newApplicantId)
        {
            _ancestralTablet.SetAncestralTablet(oldQuadranlgeId);

            var deceaseds = _deceased.GetDeceasedsByAncestralTabletId(oldQuadranlgeId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.SetAncestralTablet(newAncestralTabletId);
            }

            if (deceaseds.Any())
            {
                _ancestralTablet.SetHasDeceased(false);
            }

            _ancestralTablet.RemoveApplicant();

            _ancestralTablet.SetAncestralTablet(newAncestralTabletId);
            _ancestralTablet.SetApplicant(newApplicantId);
            _ancestralTablet.SetHasDeceased(deceaseds.Any());

            return true;
        }

        public bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto)
        {
            _ancestralTablet.SetAncestralTablet((int)ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId);
            if (_ancestralTablet.HasApplicant())
                return false;

            if (!SetTransactionDeceasedIdBasedOnAncestralTablet(ancestralTabletTransactionDto, (int)ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId))
                return false;

            ancestralTabletTransactionDto.ShiftedAncestralTabletTransactionDtoAF = _tracking.GetLatestFirstTransactionByAncestralTabletId((int)ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId).AncestralTabletTransactionAF;

            GetTransaction(ancestralTabletTransactionDto.ShiftedAncestralTabletTransactionDtoAF).DeletedDate = System.DateTime.Now;

            _tracking.Remove((int)ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId, ancestralTabletTransactionDto.ShiftedAncestralTabletTransactionDtoAF);

            NewNumber(ancestralTabletTransactionDto.AncestralTabletItemDtoId);

            SummaryItem(ancestralTabletTransactionDto);

            if (CreateNewTransaction(ancestralTabletTransactionDto))
            {
                ShiftAncestralTabletApplicantDeceaseds(ancestralTabletTransactionDto.AncestralTabletDtoId, (int)ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId, ancestralTabletTransactionDto.ApplicantDtoId);

                _maintenance.ChangeAncestralTablet((int)ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId, ancestralTabletTransactionDto.AncestralTabletDtoId);

                _tracking.Add(ancestralTabletTransactionDto.AncestralTabletDtoId, _AFnumber, ancestralTabletTransactionDto.ApplicantDtoId, ancestralTabletTransactionDto.DeceasedDtoId);

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
            if (_invoice.GetInvoicesByAF(ancestralTabletTransactionDto.AF).Any() && ancestralTabletTransactionDto.Price <
                _invoice.GetInvoicesByAF(ancestralTabletTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var ancestralTabletTransactionInDb = GetTransaction(ancestralTabletTransactionDto.AF);

            if(ancestralTabletTransactionInDb.ShiftedAncestralTabletId != ancestralTabletTransactionDto.ShiftedAncestralTabletDtoId)
            {
                if (!SetTransactionDeceasedIdBasedOnAncestralTablet(ancestralTabletTransactionDto, ancestralTabletTransactionInDb.AncestralTabletId))
                    return false;

                _tracking.Remove(ancestralTabletTransactionInDb.AncestralTabletId, ancestralTabletTransactionDto.AF);

                _tracking.Add(ancestralTabletTransactionDto.AncestralTabletDtoId, ancestralTabletTransactionDto.AF, ancestralTabletTransactionDto.ApplicantDtoId, ancestralTabletTransactionDto.DeceasedDtoId);

                ShiftAncestralTabletApplicantDeceaseds(ancestralTabletTransactionInDb.AncestralTabletId, ancestralTabletTransactionDto.AncestralTabletDtoId, ancestralTabletTransactionDto.ApplicantDtoId);

                _maintenance.ChangeAncestralTablet(ancestralTabletTransactionInDb.AncestralTabletId, ancestralTabletTransactionDto.AncestralTabletDtoId);

                SummaryItem(ancestralTabletTransactionDto);

                UpdateTransaction(ancestralTabletTransactionDto);

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if (GetTransactionsByShiftedAncestralTabletTransactionAF(_transaction.AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction((int)_transaction.ShiftedAncestralTabletId, _transaction.AF))
                return false;

            _ancestralTablet.SetAncestralTablet((int)_transaction.ShiftedAncestralTabletId);
            if (_ancestralTablet.HasApplicant())
                return false;

            DeleteTransaction();


            _ancestralTablet.SetAncestralTablet(_transaction.AncestralTabletId);

            _ancestralTablet.RemoveApplicant();

            _ancestralTablet.SetHasDeceased(false);

            var deceaseds = _deceased.GetDeceasedsByAncestralTabletId(_transaction.AncestralTabletId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveNiche();
            }

            _tracking.Remove(_transaction.AncestralTabletId, _transaction.AF);


            var previousTransaction = GetTransactionExclusive(_transaction.ShiftedAncestralTabletTransactionAF);

            _ancestralTablet.SetAncestralTablet(previousTransaction.AncestralTabletId);

            _ancestralTablet.SetApplicant(previousTransaction.ApplicantId);

            if (previousTransaction.DeceasedId != null)
            {
                _deceased.SetDeceased((int)previousTransaction.DeceasedId);

                if (_deceased.GetAncestralTablet() != null && _deceased.GetAncestralTablet().Id != _transaction.AncestralTabletId)
                    return false;

                _deceased.SetAncestralTablet(previousTransaction.AncestralTabletId);

                _ancestralTablet.SetHasDeceased(true);
            }

            previousTransaction.DeletedDate = null;

            _tracking.Add(previousTransaction.AncestralTabletId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.DeceasedId);

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _maintenance.ChangeAncestralTablet(_transaction.AncestralTabletId, previousTransaction.AncestralTabletId);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(AncestralTabletTransactionDto trx)
        {
            _ancestralTablet.SetAncestralTablet(trx.AncestralTabletDtoId);
            
            trx.SummaryItem = "AF: " + (string.IsNullOrEmpty(trx.AF) ? _AFnumber : trx.AF) + "<BR/>" +
                Resources.Mix.AncestralTablet + ": " + _ancestralTablet.GetAncestralTablet((int)trx.ShiftedAncestralTabletDtoId).Name + "<BR/>" +
                Resources.Mix.ShiftTo + "<BR/>" +
                Resources.Mix.AncestralTablet + ": " + _ancestralTablet.GetName() + "<BR/>" +
                Resources.Mix.Remark + ": " + trx.Remark;
        }
    }
}