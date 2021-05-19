﻿using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Transfer(
            IUnitOfWork unitOfWork,
            IItem item,
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IQuadrangle invoice,
            IPayment payment,
            ITracking tracking
            ) :
            base(
                unitOfWork,
                item,
                quadrangle,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
            _tracking = tracking;
        }

        public void SetTransfer(string AF)
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

        public bool AllowQuadrangleDeceasePairing(int quadrangleId, int applicantId)
        {
            _quadrangle.SetQuadrangle(quadrangleId);

            if (_quadrangle.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByQuadrangleId(quadrangleId);
                foreach (var deceased in deceaseds)
                {
                    var applicantDeceased = _applicantDeceased.GetApplicantDeceased(applicantId, deceased.Id);
                    if (applicantDeceased != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public bool Create(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            _quadrangle.SetQuadrangle(quadrangleTransactionDto.NicheId);
            if (_quadrangle.GetApplicantId() == quadrangleTransactionDto.ApplicantId)
                return false;

            if (!AllowQuadrangleDeceasePairing(quadrangleTransactionDto.NicheId, quadrangleTransactionDto.ApplicantId))
                return false;

            if (!SetTransactionDeceasedIdBasedOnQuadrangle(quadrangleTransactionDto, quadrangleTransactionDto.NicheId))
                return false;

            quadrangleTransactionDto.TransferredColumbariumTransactionAF = _tracking.GetLatestFirstTransactionByQuadrangleId(quadrangleTransactionDto.NicheId).ColumbariumTransactionAF;

            GetTransaction(quadrangleTransactionDto.TransferredColumbariumTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove(quadrangleTransactionDto.NicheId, quadrangleTransactionDto.TransferredColumbariumTransactionAF);

            NewNumber(quadrangleTransactionDto.ColumbariumItemId);

            if (CreateNewTransaction(quadrangleTransactionDto))
            {
                _quadrangle.SetApplicant(quadrangleTransactionDto.ApplicantId);

                _tracking.Add(quadrangleTransactionDto.NicheId, _AFnumber, quadrangleTransactionDto.ApplicantId, quadrangleTransactionDto.Deceased1Id, quadrangleTransactionDto.Deceased2Id);

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }


            return true;
        }

        public bool Update(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(quadrangleTransactionDto.AF).Any() && quadrangleTransactionDto.Price <
                _invoice.GetInvoicesByAF(quadrangleTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            UpdateTransaction(quadrangleTransactionDto);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.NicheId, _transaction.AF))
                return false;

            _quadrangle.SetQuadrangle(_transaction.NicheId);

            _quadrangle.RemoveApplicant();

            _quadrangle.SetHasDeceased(false);

            var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_transaction.NicheId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveQuadrangle();
            }

            _tracking.Remove(_transaction.NicheId, _transaction.AF);


            var previousTransaction = GetTransactionExclusive(_transaction.TransferredColumbariumTransactionAF);

            _quadrangle.SetQuadrangle(previousTransaction.NicheId);

            _quadrangle.SetApplicant(previousTransaction.ApplicantId);

            if (previousTransaction.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased1Id);

                if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != _transaction.NicheId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.NicheId);

                _quadrangle.SetHasDeceased(true);
            }

            if (previousTransaction.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased2Id);

                if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != _transaction.NicheId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.NicheId);

                _quadrangle.SetHasDeceased(true);
            }

            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

    }
}