using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.Columbarium
{
    public class Transfer : Transaction, ITransfer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public Transfer(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking
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
            _tracking = tracking;
        }

        public bool AllowNicheDeceasePairing(int nicheId, int applicantId)
        {
            var niche = _niche.GetById(nicheId);

            if (niche.hasDeceased)
            {
                var deceaseds = _deceased.GetByNicheId(nicheId);
                foreach (var deceased in deceaseds)
                {
                    var applicantDeceased = _applicantDeceased.GetByApplicantDeceasedId(applicantId, deceased.Id);
                    if (applicantDeceased != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var niche = _niche.GetById(columbariumTransaction.NicheId);

            if (niche.ApplicantId == columbariumTransaction.ApplicantId)
                return false;

            if (!AllowNicheDeceasePairing(columbariumTransaction.NicheId, columbariumTransaction.ApplicantId))
                return false;

            if (!SetTransactionDeceasedIdBasedOnNiche(columbariumTransaction, columbariumTransaction.NicheId))
                return false;

            columbariumTransaction.TransferredColumbariumTransactionAF = _tracking.GetLatestFirstTransactionByNicheId(columbariumTransaction.NicheId).ColumbariumTransactionAF;

            GetByAF(columbariumTransaction.TransferredColumbariumTransactionAF).DeletedUtcTime = System.DateTime.UtcNow;

            _tracking.Remove(columbariumTransaction.NicheId, columbariumTransaction.TransferredColumbariumTransactionAF);

            columbariumTransaction.AF = _number.GetNewAF(columbariumTransaction.ColumbariumItemId, System.DateTime.Now.Year);

            SummaryItem(columbariumTransaction);

            _unitOfWork.ColumbariumTransactions.Add(columbariumTransaction);

            niche.ApplicantId = columbariumTransaction.ApplicantId;

            _tracking.Add(columbariumTransaction.NicheId, columbariumTransaction.AF, columbariumTransaction.ApplicantId, columbariumTransaction.Deceased1Id, columbariumTransaction.Deceased2Id);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveColumbariumAF(columbariumTransaction.AF).ToList();

            if (invoices.Any() && GetTotalAmount(columbariumTransaction) < invoices.Max(i => i.Amount))
                return false;

            SummaryItem(columbariumTransaction);

            var columbariumTransactionInDb = GetByAF(AF);
            columbariumTransactionInDb.Price = columbariumTransaction.Price;
            columbariumTransactionInDb.Maintenance = columbariumTransaction.Maintenance;
            columbariumTransactionInDb.LifeTimeMaintenance = columbariumTransaction.LifeTimeMaintenance;
            columbariumTransactionInDb.SummaryItem = columbariumTransaction.SummaryItem;
            columbariumTransactionInDb.Remark = columbariumTransaction.Remark;

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(string AF)
        {
            var transactionInDb = _unitOfWork.ColumbariumTransactions.GetByAF(AF);

            if (!_tracking.IsLatestTransaction(transactionInDb.NicheId, transactionInDb.AF))
                return false;

            if (_unitOfWork.Invoices.GetByActiveColumbariumAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByColumbariumAF(AF).Any())
                return false;

            _unitOfWork.ColumbariumTransactions.Remove(transactionInDb);

            var niche = _niche.GetById(transactionInDb.NicheId);
            niche.Applicant = null;
            niche.ApplicantId = null;
            niche.hasDeceased = false;

            var deceaseds = _deceased.GetByNicheId(transactionInDb.NicheId);

            foreach (var deceased in deceaseds)
            {
                deceased.Niche = null;
                deceased.NicheId = null;
            }

            _tracking.Remove(transactionInDb.NicheId, transactionInDb.AF);


            var previousTransaction = GetTransactionExclusive(transactionInDb.TransferredColumbariumTransactionAF);

            var previousNiche = _niche.GetById(previousTransaction.NicheId);
            previousNiche.ApplicantId = previousTransaction.ApplicantId;

            if (previousTransaction.Deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)previousTransaction.Deceased1Id);
                if (deceased.NicheId != null && deceased.NicheId != transactionInDb.NicheId)
                    return false;

                deceased.NicheId = previousTransaction.NicheId;

                previousNiche.hasDeceased = true;
            }

            if (previousTransaction.Deceased2Id != null)
            {
                var deceased = _deceased.GetById((int)previousTransaction.Deceased2Id);
                if (deceased.NicheId != null && deceased.NicheId != transactionInDb.NicheId)
                    return false;

                deceased.NicheId = previousTransaction.NicheId;

                previousNiche.hasDeceased = true;
            }

            _unitOfWork.ColumbariumTransactions.Remove(transactionInDb);

            previousTransaction.DeletedUtcTime = null;

            _tracking.Add(previousTransaction.NicheId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            columbariumTransaction.SummaryItem = "AF: " + columbariumTransaction.AF + "<BR/>" +
                Resources.Mix.Niche + ": " + _applicant.Get((int)columbariumTransaction.TransferredApplicantId).Name + "<BR/>" +
                Resources.Mix.TransferTo + "<BR/>" +
                Resources.Mix.Niche + ": " + columbariumTransaction.Applicant.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + columbariumTransaction.Remark;
        }
    }
}