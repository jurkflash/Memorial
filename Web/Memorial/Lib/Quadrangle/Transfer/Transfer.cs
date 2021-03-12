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
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IQuadranglePayment _payment;

        public Transfer(
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

        public bool Create(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            //_quadrangle.SetQuadrangle(quadrangleTransactionDto.QuadrangleId);
            //var deceased = _deceased.GetDeceasedsByQuadrangleId(quadrangleTransactionDto.QuadrangleId);
            //deceased.ApplicantId

            if (_quadrangle.HasDeceased())
            {

            }

            if (!_quadrangle.HasDeceased() && 
                _quadrangle.GetApplicantId() != quadrangleTransactionDto.ApplicantId)
            {

                NewNumber(quadrangleTransactionDto.QuadrangleItemId);

                if (CreateNewTransaction(quadrangleTransactionDto))
                {
                    _unitOfWork.Complete();
                }
                else
                {
                    return false;
                }
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















        //public bool Create()
        //{
        //    CreateNewTransaction();

        //    _quadrangle.SetApplicant(_transaction.ApplicantId);

        //    _unitOfWork.Complete();

        //    return true;
        //}

    }
}