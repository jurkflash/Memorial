using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IQuadranglePayment _payment;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased,
            INumber number,
            Invoice.IQuadrangle invoice,
            IQuadranglePayment payment
            ) : 
            base(
                unitOfWork, 
                item, 
                quadrangle, 
                applicant, 
                deceased, 
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
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

        public bool Create(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            if (quadrangleTransactionDto.DeceasedId != null)
            {
                SetDeceased((int)quadrangleTransactionDto.DeceasedId);
                if (_deceased.GetQuadrangle() != null)
                    return false;
            }

            NewNumber(quadrangleTransactionDto.QuadrangleItemId);

            if (CreateNewTransaction(quadrangleTransactionDto))
            {
                _quadrangle.SetQuadrangle(quadrangleTransactionDto.QuadrangleId);
                _quadrangle.SetApplicant(quadrangleTransactionDto.ApplicantId);

                if (quadrangleTransactionDto.DeceasedId != null)
                {
                    SetDeceased((int)quadrangleTransactionDto.DeceasedId);
                    if (_deceased.SetQuadrangle(quadrangleTransactionDto.QuadrangleId))
                    {
                        _quadrangle.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(quadrangleTransactionDto.AF).Any() && quadrangleTransactionDto.Price + (float)quadrangleTransactionDto.Maintenance + (float)quadrangleTransactionDto.LifeTimeMaintenance < 
                _invoice.GetInvoicesByAF(quadrangleTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            if(UpdateTransaction(quadrangleTransactionDto))
            {
                if (quadrangleTransactionDto.DeceasedId != null)
                {
                    SetDeceased((int)quadrangleTransactionDto.DeceasedId);
                    if (_deceased.SetQuadrangle(quadrangleTransactionDto.QuadrangleId))
                    {
                        _quadrangle.SetHasDeceased(true);
                    }
                    else
                        return false;
                }

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);
            if (_quadrangle.HasDeceased())
                return false;

            DeleteTransaction();

            if (_transaction.DeceasedId != null)
            {
                SetDeceased((int)_transaction.DeceasedId);
                _deceased.RemoveQuadrangle();
                _quadrangle.SetHasDeceased(false);
            }

            _quadrangle.RemoveApplicant();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

    }
}