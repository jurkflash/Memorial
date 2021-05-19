﻿using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;

        public Order(
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

        public bool Create(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            if (quadrangleTransactionDto.Deceased1Id != null)
            {
                SetDeceased((int)quadrangleTransactionDto.Deceased1Id);
                if (_deceased.GetQuadrangle() != null)
                    return false;
            }

            NewNumber(quadrangleTransactionDto.ColumbariumItemId);

            if (CreateNewTransaction(quadrangleTransactionDto))
            {
                _quadrangle.SetQuadrangle(quadrangleTransactionDto.QuadrangleId);
                _quadrangle.SetApplicant(quadrangleTransactionDto.ApplicantId);

                if (quadrangleTransactionDto.Deceased1Id != null)
                {
                    SetDeceased((int)quadrangleTransactionDto.Deceased1Id);
                    if (_deceased.SetQuadrangle(quadrangleTransactionDto.QuadrangleId))
                    {
                        _quadrangle.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                if (quadrangleTransactionDto.Deceased2Id != null)
                {
                    SetDeceased((int)quadrangleTransactionDto.Deceased2Id);
                    if (_deceased.SetQuadrangle(quadrangleTransactionDto.QuadrangleId))
                    {
                        _quadrangle.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

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
            if (_invoice.GetInvoicesByAF(quadrangleTransactionDto.AF).Any() && quadrangleTransactionDto.Price + (float)quadrangleTransactionDto.Maintenance + (float)quadrangleTransactionDto.LifeTimeMaintenance < 
                _invoice.GetInvoicesByAF(quadrangleTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            var quadrangleTransactionInDb = GetTransaction(quadrangleTransactionDto.AF);

            var deceased1InDb = quadrangleTransactionInDb.Deceased1Id;

            var deceased2InDb = quadrangleTransactionInDb.Deceased2Id;

            if (UpdateTransaction(quadrangleTransactionDto))
            {
                _quadrangle.SetQuadrangle(quadrangleTransactionDto.QuadrangleId);

                QuadrangleApplicantDeceaseds(quadrangleTransactionDto.Deceased1Id, deceased1InDb);

                QuadrangleApplicantDeceaseds(quadrangleTransactionDto.Deceased2Id, deceased2InDb);

                if (quadrangleTransactionDto.Deceased1Id == null && quadrangleTransactionDto.Deceased2Id == null)
                    _quadrangle.SetHasDeceased(false);

                _tracking.Change(quadrangleTransactionDto.QuadrangleId, quadrangleTransactionDto.AF, quadrangleTransactionDto.ApplicantId, quadrangleTransactionDto.Deceased1Id, quadrangleTransactionDto.Deceased2Id);

                _unitOfWork.Complete();
            }

            return true;
        }

        private bool QuadrangleApplicantDeceaseds(int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    _deceased.SetDeceased((int)dbDeceasedId);
                    _deceased.RemoveQuadrangle();
                }
                else
                {
                    _deceased.SetDeceased((int)deceasedId);
                    if (_deceased.GetQuadrangle() != null && _deceased.GetQuadrangle().Id != _quadrangle.GetQuadrangle().Id)
                    {
                        return false;
                    }
                    else
                    {
                        _deceased.SetQuadrangle(_quadrangle.GetQuadrangle().Id);
                        _quadrangle.SetHasDeceased(true);
                    }
                }
            }

            return true;
        }

        public bool Delete()
        {
            if (!_tracking.IsLatestTransaction(_transaction.QuadrangleId, _transaction.AF))
                return false;

            DeleteAllTransactionWithSameQuadrangleId();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();


            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);

            var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_transaction.QuadrangleId);

            foreach (var deceased in deceaseds)
            {
                _deceased.SetDeceased(deceased.Id);
                _deceased.RemoveQuadrangle();
            }

            _quadrangle.SetHasDeceased(false);

            _quadrangle.RemoveApplicant();

            _tracking.Delete(_transaction.AF);

            _unitOfWork.Complete();

            return true;
        }

    }
}