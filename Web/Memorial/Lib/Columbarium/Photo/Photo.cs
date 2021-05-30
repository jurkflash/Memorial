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
    public class Photo : Transaction, IPhoto
    {
        private const string _systemCode = "Photo";
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IColumbarium _invoice;
        private readonly IPayment _payment;

        public Photo(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            Invoice.IColumbarium invoice,
            IPayment payment
            ) :
            base(
                unitOfWork,
                item,
                niche,
                applicant,
                deceased,
                applicantDeceased,
                number
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _niche = niche;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
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

        public bool Create(ColumbariumTransactionDto columbariumTransactionDto)
        {
            if (columbariumTransactionDto.DeceasedDto1Id != null)
            {
                SetDeceased((int)columbariumTransactionDto.DeceasedDto1Id);
                if (_deceased.GetNiche() != null)
                    return false;
            }

            NewNumber(columbariumTransactionDto.ColumbariumItemDtoId);

            if (CreateNewTransaction(columbariumTransactionDto))
            {
                _unitOfWork.Complete();
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Update(ColumbariumTransactionDto columbariumTransactionDto)
        {
            if (_invoice.GetInvoicesByAF(columbariumTransactionDto.AF).Any() && columbariumTransactionDto.Price <
                _invoice.GetInvoicesByAF(columbariumTransactionDto.AF).Max(i => i.Amount))
            {
                return false;
            }

            UpdateTransaction(columbariumTransactionDto);

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

        public bool ChangeNiche(int oldNicheId, int newNicheId)
        {
            if (!ChangeNiche(_systemCode, oldNicheId, newNicheId))
                return false;

            return true;
        }
    }
}