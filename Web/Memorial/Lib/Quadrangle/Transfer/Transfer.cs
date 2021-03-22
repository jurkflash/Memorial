using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Invoice.IQuadrangle _invoice;
        private readonly IPayment _payment;
        private readonly ITracking _tracking;
        private readonly IQuadrangleApplicantDeceaseds _quadrangleApplicantDeceaseds;

        public Transfer(
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
            IQuadrangleApplicantDeceaseds quadrangleApplicantDeceaseds
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
            _quadrangleApplicantDeceaseds = quadrangleApplicantDeceaseds;
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

        public bool AllowQuadrangleDeceasePairing(IQuadrangle quadrangle, int applicantId)
        {
            if (quadrangle.HasDeceased())
            {
                var deceaseds = _deceased.GetDeceasedsByQuadrangleId(quadrangle.GetQuadrangle().Id);
                foreach (var deceased in deceaseds)
                {
                    var applicantDeceased = _applicantDeceased.GetApplicantDeceased(applicantId, deceased.Id);
                    if (applicantDeceased != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public bool Create(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            _quadrangle.SetQuadrangle(quadrangleTransactionDto.QuadrangleId);

            if (!AllowQuadrangleDeceasePairing(_quadrangle, quadrangleTransactionDto.ApplicantId))
                return false;


            if(!SetDeceasedIdBasedOnQuadrangleLastTransaction(quadrangleTransactionDto))
                return false;

            if (_quadrangle.GetApplicantId() == quadrangleTransactionDto.ApplicantId)
                return false;


            NewNumber(quadrangleTransactionDto.QuadrangleItemId);

            if (CreateNewTransaction(quadrangleTransactionDto))
            {
                _quadrangle.SetApplicant(quadrangleTransactionDto.ApplicantId);

                _tracking.Add(quadrangleTransactionDto.QuadrangleId, _AFnumber, quadrangleTransactionDto.ApplicantId, quadrangleTransactionDto.Deceased1Id, quadrangleTransactionDto.Deceased2Id);

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
            if (!_tracking.IsLatestTransaction(_transaction.QuadrangleId, _transaction.AF))
                return false;

            DeleteTransaction();

            _payment.SetTransaction(_transaction.AF);
            _payment.DeleteTransaction();

            _quadrangleApplicantDeceaseds.RollbackQuadrangleApplicantDeceaseds(_transaction.AF, _transaction.QuadrangleId);

            _unitOfWork.Complete();

            return true;
        }

    }
}