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
    public class Photo : Transaction, IPhoto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IQuadranglePayment _payment;

        public Photo(
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

        public void SetPhoto(string AF)
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
            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }
    }
}