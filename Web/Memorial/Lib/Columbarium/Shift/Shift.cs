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
            _quadrangle.SetQuadrangle(quadrangleTransactionDto.QuadrangleId);
            if (_quadrangle.HasApplicant())
                return false;

            if(!SetTransactionDeceasedIdBasedOnQuadrangle(quadrangleTransactionDto, (int)quadrangleTransactionDto.ShiftedQuadrangleId))
                return false;

            quadrangleTransactionDto.ShiftedQuadrangleTransactionAF = _tracking.GetLatestFirstTransactionByQuadrangleId((int)quadrangleTransactionDto.ShiftedQuadrangleId).QuadrangleTransactionAF;

            GetTransaction(quadrangleTransactionDto.ShiftedQuadrangleTransactionAF).DeleteDate = System.DateTime.Now;

            _tracking.Remove((int)quadrangleTransactionDto.ShiftedQuadrangleId, quadrangleTransactionDto.ShiftedQuadrangleTransactionAF);

            NewNumber(quadrangleTransactionDto.QuadrangleItemId);

            if (CreateNewTransaction(quadrangleTransactionDto))
            {
                ShiftQuadrangleApplicantDeceaseds((int)quadrangleTransactionDto.ShiftedQuadrangleId, quadrangleTransactionDto.QuadrangleId, quadrangleTransactionDto.ApplicantId);

                _manage.ChangeQuadrangle((int)quadrangleTransactionDto.ShiftedQuadrangleId, quadrangleTransactionDto.QuadrangleId);

                _photo.ChangeQuadrangle((int)quadrangleTransactionDto.ShiftedQuadrangleId, quadrangleTransactionDto.QuadrangleId);
                
                _tracking.Add(quadrangleTransactionDto.QuadrangleId, _AFnumber, quadrangleTransactionDto.ApplicantId, quadrangleTransactionDto.Deceased1Id, quadrangleTransactionDto.Deceased2Id);

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

            if (quadrangleTransactionInDb.QuadrangleId != quadrangleTransactionDto.QuadrangleId)
            {
                if (!SetTransactionDeceasedIdBasedOnQuadrangle(quadrangleTransactionDto, quadrangleTransactionInDb.QuadrangleId))
                    return false;

                _tracking.Remove(quadrangleTransactionInDb.QuadrangleId, quadrangleTransactionDto.AF);

                _tracking.Add(quadrangleTransactionDto.QuadrangleId, quadrangleTransactionDto.AF, quadrangleTransactionDto.ApplicantId, quadrangleTransactionDto.Deceased1Id, quadrangleTransactionDto.Deceased2Id);

                ShiftQuadrangleApplicantDeceaseds(quadrangleTransactionInDb.QuadrangleId, quadrangleTransactionDto.QuadrangleId, quadrangleTransactionDto.ApplicantId);

                _manage.ChangeQuadrangle(quadrangleTransactionInDb.QuadrangleId, quadrangleTransactionDto.QuadrangleId);

                _photo.ChangeQuadrangle(quadrangleTransactionInDb.QuadrangleId, quadrangleTransactionDto.QuadrangleId);

                UpdateTransaction(quadrangleTransactionDto);

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            if(GetTransactionsByShiftedQuadrangleTransactionAF(_transaction.AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction(_transaction.QuadrangleId, _transaction.AF))
                return false;

            _quadrangle.SetQuadrangle((int)_transaction.ShiftedQuadrangleId);
            if (_quadrangle.HasApplicant())
                return false;

            DeleteTransaction();


            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);

            _quadrangle.RemoveApplicant();

            _quadrangle.SetHasDeceased(false);

            var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_transaction.QuadrangleId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveQuadrangle();
            }

            _tracking.Remove(_transaction.QuadrangleId, _transaction.AF);


            var previousTransaction = GetTransactionExclusive(_transaction.ShiftedColumbariumTransactionAF);

            _quadrangle.SetQuadrangle(previousTransaction.QuadrangleId);

            _quadrangle.SetApplicant(previousTransaction.ApplicantId);

            if (previousTransaction.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased1Id);

                if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != _transaction.QuadrangleId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.QuadrangleId);

                _quadrangle.SetHasDeceased(true);
            }

            if (previousTransaction.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)previousTransaction.Deceased2Id);

                if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != _transaction.QuadrangleId)
                    return false;

                _deceased.SetQuadrangle(previousTransaction.QuadrangleId);

                _quadrangle.SetHasDeceased(true);
            }

            previousTransaction.DeleteDate = null;

            _tracking.Add(previousTransaction.QuadrangleId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id);

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _manage.ChangeQuadrangle(_transaction.QuadrangleId, previousTransaction.QuadrangleId);

            _photo.ChangeQuadrangle(_transaction.QuadrangleId, previousTransaction.QuadrangleId);

            _unitOfWork.Complete();
            
            return true;
        }

    }
}