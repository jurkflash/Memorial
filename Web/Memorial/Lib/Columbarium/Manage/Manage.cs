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
    public class Manage : Transaction, IManage
    {
        private const string _systemCode = "Manage";
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IPayment _payment;

        public Manage(
            IUnitOfWork unitOfWork,
            IItem item,
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IQuadrangle invoice,
            IPayment payment
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
        }

        public void SetManage(string AF)
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

        public bool Create(ColumbariumTransactionDto quadrangleTransactionDto)
        {
            NewNumber(quadrangleTransactionDto.ColumbariumItemId);

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
            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _unitOfWork.Complete();

            return true;
        }

        public bool ChangeQuadrangle(int oldQuadrangleId, int newQuadrangleId)
        {
            if (!ChangeQuadrangle(_systemCode, oldQuadrangleId, newQuadrangleId))
                return false;

            return true;
        }

        public float GetAmount(int itemId, DateTime from, DateTime to)
        {
            _item.SetItem(itemId);
            
            if (from > to)
                return -1;

            var total = (((to.Year - from.Year) * 12) + to.Month - from.Month) * _item.GetPrice();

            return total;
        }
    }
}