using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
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

        public bool Create(PlotTransactionDto plotTransactionDto)
        {
            NewNumber(plotTransactionDto.PlotItemId);

            _plot.SetPlot(plotTransactionDto.PlotDtoId);
            plotTransactionDto.ApplicantDtoId = (int)_plot.GetApplicantId();

            if (CreateNewTransaction(plotTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(PlotTransactionDto plotTransactionDto)
        {
            if (_receipt.GetNonOrderReceipts(plotTransactionDto.AF).Any() && plotTransactionDto.Price <
                _receipt.GetNonOrderReceipts(plotTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            UpdateTransaction(plotTransactionDto);

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