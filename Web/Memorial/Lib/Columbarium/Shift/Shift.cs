using Memorial.Core;
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
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IManage _manage;
        private readonly IPhoto _photo;

        public Shift(
            IUnitOfWork unitOfWork,
            IItem item,
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IQuadrangle invoice,
            IPayment payment,
            ITracking tracking,
            IManage manage,
            IPhoto photo
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
            _manage = manage;
            _photo = photo;
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

        private bool ShiftQuadrangleApplicantDeceaseds(int oldQuadranlgeId, int newQuadrangleId, int newApplicantId)
        {
            _quadrangle.SetQuadrangle(oldQuadranlgeId);

            var deceaseds = _deceased.GetDeceasedsByQuadrangleId(oldQuadranlgeId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.SetQuadrangle(newQuadrangleId);
            }

            if (deceaseds.Any())
            {
                _quadrangle.SetHasDeceased(false);
            }

            _quadrangle.RemoveApplicant();

            _quadrangle.SetQuadrangle(newQuadrangleId);
            _quadrangle.SetApplicant(newApplicantId);
            _quadrangle.SetHasDeceased(deceaseds.Any());

            return true;
        }

        public bool Create(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            _quadrangle.SetQuadrangle(quadrangleTransactionDto.NicheId);
            if (_quadrangle.HasApplicant())
                return false;

            if(!SetTransactionDeceasedIdBasedOnQuadrangle(quadrangleTransactionDto, (int)quadrangleTransactionDto.ShiftedNicheId))
                return false;

            quadrangleTransactionDto.ShiftedColumbariumTransactionAF = _tracking.GetLatestFirstTransactionByQuadrangleId((int)quadrangleTransactionDto.ShiftedNicheId).ColumbariumTransactionAF;

            GetTransaction(quadrangleTransactionDto.ShiftedColumbariumTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove((int)quadrangleTransactionDto.ShiftedNicheId, quadrangleTransactionDto.ShiftedColumbariumTransactionAF);

            NewNumber(quadrangleTransactionDto.ColumbariumItemId);

            if (CreateNewTransaction(quadrangleTransactionDto))
            {
                ShiftQuadrangleApplicantDeceaseds((int)quadrangleTransactionDto.ShiftedNicheId, quadrangleTransactionDto.NicheId, quadrangleTransactionDto.ApplicantId);

                _manage.ChangeQuadrangle((int)quadrangleTransactionDto.ShiftedNicheId, quadrangleTransactionDto.NicheId);

                _photo.ChangeQuadrangle((int)quadrangleTransactionDto.ShiftedNicheId, quadrangleTransactionDto.NicheId);
                
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

            var quadrangleTransactionInDb = GetTransaction(quadrangleTransactionDto.AF);

            if (quadrangleTransactionInDb.NicheId != quadrangleTransactionDto.NicheId)
            {
                if (!SetTransactionDeceasedIdBasedOnQuadrangle(quadrangleTransactionDto, quadrangleTransactionInDb.NicheId))
                    return false;

                _tracking.Remove(quadrangleTransactionInDb.NicheId, quadrangleTransactionDto.AF);

                _tracking.Add(quadrangleTransactionDto.NicheId, quadrangleTransactionDto.AF, quadrangleTransactionDto.ApplicantId, quadrangleTransactionDto.Deceased1Id, quadrangleTransactionDto.Deceased2Id);

                ShiftQuadrangleApplicantDeceaseds(quadrangleTransactionInDb.NicheId, quadrangleTransactionDto.NicheId, quadrangleTransactionDto.ApplicantId);

                _manage.ChangeQuadrangle(quadrangleTransactionInDb.NicheId, quadrangleTransactionDto.NicheId);

                _photo.ChangeQuadrangle(quadrangleTransactionInDb.NicheId, quadrangleTransactionDto.NicheId);

                UpdateTransaction(quadrangleTransactionDto);

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if(GetTransactionsByShiftedQuadrangleTransactionAF(_transaction.AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction(_transaction.NicheId, _transaction.AF))
                return false;

            _quadrangle.SetQuadrangle((int)_transaction.ShiftedNicheId);
            if (_quadrangle.HasApplicant())
                return false;

            DeleteTransaction();


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


            var previousTransaction = GetTransactionExclusive(_transaction.ShiftedColumbariumTransactionAF);

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

            previousTransaction.DeleteDate = null;

            _tracking.Add(previousTransaction.NicheId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id);

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _manage.ChangeQuadrangle(_transaction.NicheId, previousTransaction.NicheId);

            _photo.ChangeQuadrangle(_transaction.NicheId, previousTransaction.NicheId);

            _unitOfWork.Complete();
            
            return true;
        }

    }
}