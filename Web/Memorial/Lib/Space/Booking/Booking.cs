using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public class Booking : Transaction, IBooking
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.ISpace _invoice;
        private readonly IPayment _payment;

        public Booking(
            IUnitOfWork unitOfWork,
            IItem item,
            ISpace space,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.ISpace invoice,
            IPayment payment
            ) : 
            base(
                unitOfWork, 
                item,
                space, 
                applicant, 
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _space = space;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _invoice = invoice;
            _payment = payment;
        }

        public void SetBooking(string AF)
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

        public bool IsAvailable(int itemId, DateTime from, DateTime to)
        {
            if (_space.CheckAvailability(from, to, itemId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAvailable(string AF, DateTime from, DateTime to)
        {
            if (_space.CheckAvailability(from, to, AF))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(SpaceTransactionDto spaceTransactionDto)
        {
            if (!IsAvailable(spaceTransactionDto.SpaceItemId, (DateTime)spaceTransactionDto.FromDate, (DateTime)spaceTransactionDto.ToDate))
            {
                return false;
            }

            NewNumber(spaceTransactionDto.SpaceItemId);

            SummaryItem(spaceTransactionDto);

            if (CreateNewTransaction(spaceTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool Update(SpaceTransactionDto spaceTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(spaceTransactionDto.AF).Any() && spaceTransactionDto.Amount < 
                _invoice.GetInvoicesByAF(spaceTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            if (!IsAvailable(spaceTransactionDto.AF, (DateTime)spaceTransactionDto.FromDate, (DateTime)spaceTransactionDto.ToDate))
            {
                return false;
            }

            var spaceTransactionInDb = GetTransaction(spaceTransactionDto.AF);

            SummaryItem(spaceTransactionDto);

            if (UpdateTransaction(spaceTransactionDto))
            {
                _unitOfWork.Complete();
            }

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

        private void SummaryItem(SpaceTransactionDto trx)
        {
            trx.SummaryItem = "AF: " + trx.AF == null ? _AFnumber : trx.AF + "<BR/>" +
                Resources.Mix.Space + ": " + trx.SpaceItem.Space.Name +"<BR/>"+ 
                Resources.Mix.From + ": " + trx.FromDate.Value.ToString("yyyy-MMM-dd HH:mm") + " " + Resources.Mix.To + ": " + trx.ToDate.Value.ToString("yyyy-MMM-dd HH:mm") + "<BR/>"+
                Resources.Mix.Remark + ": " + trx.Remark;
        }

    }
}