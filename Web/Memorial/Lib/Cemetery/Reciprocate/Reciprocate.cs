using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public class Reciprocate : Transaction, IReciprocate
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Receipt.IPlot _receipt;
        private readonly IPayment _payment;

        public Reciprocate(
            IUnitOfWork unitOfWork,
            IItem item,
            IPlot plot,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Receipt.IPlot receipt,
            IPayment payment
            ) :
            base(
                unitOfWork,
                item,
                plot,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _plot = plot;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _receipt = receipt;
            _payment = payment;
        }

        public void SetReciprocate(string AF)
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

        public bool Create(CemeteryTransactionDto cemeteryTransactionDto)
        {
            NewNumber(cemeteryTransactionDto.CemeteryItemId);

            _plot.SetPlot(cemeteryTransactionDto.PlotDtoId);
            cemeteryTransactionDto.ApplicantDtoId = (int)_plot.GetApplicantId();

            if (CreateNewTransaction(cemeteryTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(CemeteryTransactionDto cemeteryTransactionDto)
        {
            if (_receipt.GetNonOrderReceipts(cemeteryTransactionDto.AF).Any() && cemeteryTransactionDto.Price <
                _receipt.GetNonOrderReceipts(cemeteryTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            UpdateTransaction(cemeteryTransactionDto);

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