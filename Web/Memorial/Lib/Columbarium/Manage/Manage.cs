using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Columbarium
{
    public class Manage : Transaction, IManage
    {
        private const string _systemCode = "Manage";
        private readonly IUnitOfWork _unitOfWork;

        public Manage(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
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
        }

        public bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            columbariumTransaction.AF = _number.GetNewAF(columbariumTransaction.ColumbariumItemId, System.DateTime.Now.Year);

            SummaryItem(columbariumTransaction);

            _unitOfWork.ColumbariumTransactions.Add(columbariumTransaction);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveColumbariumAF(columbariumTransaction.AF).ToList();

            if (invoices.Any() && columbariumTransaction.Price < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(columbariumTransaction);

            var columbariumTransactionInDb = GetByAF(columbariumTransaction.AF);
            columbariumTransactionInDb.Price = columbariumTransaction.Price;
            columbariumTransactionInDb.SummaryItem = columbariumTransaction.SummaryItem;
            columbariumTransactionInDb.Remark = columbariumTransaction.Remark;

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            if (_unitOfWork.Invoices.GetByActiveColumbariumAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByColumbariumAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.ColumbariumTransactions.GetByAF(AF);
            _unitOfWork.ColumbariumTransactions.Remove(transactionInDb);
            _unitOfWork.Complete();

            return true;
        }

        public bool ChangeNiche(int oldNicheId, int newNicheId)
        {
            if (!ChangeNiche(_systemCode, oldNicheId, newNicheId))
                return false;

            return true;
        }

        private void SummaryItem(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var niche = _niche.GetById(columbariumTransaction.NicheId);

            columbariumTransaction.SummaryItem = "AF: " + columbariumTransaction.AF + "<BR/>" +
                Resources.Mix.Niche + ": " + niche.Name + "<BR/>" +
                Resources.Mix.From + ": " + columbariumTransaction.FromDate.Value.ToString("yyyy-MMM-dd HH:mm") + " " + Resources.Mix.To + ": " + columbariumTransaction.ToDate.Value.ToString("yyyy-MMM-dd HH:mm") + "<BR/>" +
                Resources.Mix.Remark + ": " + columbariumTransaction.Remark;
        }
    }
}