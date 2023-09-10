using Memorial.Core;
using System;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.Columbarium
{
    public class Withdraw : Transaction, IWithdraw
    {
        private const string _systemCode = "Withrdaw";
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;

        public Withdraw(
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

        public bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            columbariumTransaction.AF = _number.GetNewAF(columbariumTransaction.ColumbariumItemId, System.DateTime.Now.Year);

            var trns = GetByNicheId(columbariumTransaction.NicheId);

            if (trns.Count() == 0)
                return false;

            var trnsAF = string.Join(",", trns.Select(t => t.AF));
            foreach (var trn in trns)
            {
                trn.DeletedUtcTime = DateTime.UtcNow;
            }
            columbariumTransaction.WithdrewAFS = trnsAF;


            var trackingTrns = _tracking.GetTrackingByNicheId(columbariumTransaction.NicheId);
            foreach (var trackingTrn in trackingTrns)
            {
                trackingTrn.ToDeleteFlag = true;
            }
            

            var niche = _niche.GetById(columbariumTransaction.NicheId);

            if (columbariumTransaction.Deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)columbariumTransaction.Deceased1Id);
                deceased.AncestralTablet = null;
                deceased.AncestralTabletId = null;

                niche.hasDeceased = false;
            }

            if (columbariumTransaction.Deceased2Id != null)
            {
                var deceased = _deceased.GetById((int)columbariumTransaction.Deceased2Id);
                deceased.AncestralTablet = null;
                deceased.AncestralTabletId = null;
                niche.hasDeceased = false;
            }


            columbariumTransaction.WithdrewColumbariumApplicantId = (int)niche.ApplicantId;
            niche.Applicant = null;
            niche.ApplicantId = null;

            _unitOfWork.ColumbariumTransactions.Add(columbariumTransaction);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveColumbariumAF(columbariumTransaction.AF).ToList();

            if (invoices.Any() && GetTotalAmount(columbariumTransaction) < invoices.Max(i => i.Amount))
                return false;

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
            if (_unitOfWork.Invoices.GetByActiveColumbariumAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByColumbariumAF(AF).Any())
                return false;

            var transactionInDb = _unitOfWork.ColumbariumTransactions.GetByAF(AF);

            var AFs = transactionInDb.WithdrewAFS.Split(',');

            foreach(var AF1 in AFs)
            {
                GetByAF(AF1).DeletedUtcTime = null;
            }

            var niche = _niche.GetById(transactionInDb.NicheId);
            if (transactionInDb.Deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)transactionInDb.Deceased1Id);
                deceased.AncestralTabletId = transactionInDb.NicheId;
                niche.hasDeceased = true;
            }

            if (transactionInDb.Deceased2Id != null)
            {
                var deceased = _deceased.GetById((int)transactionInDb.Deceased2Id);
                deceased.AncestralTabletId = transactionInDb.NicheId;
                niche.hasDeceased = true;
            }

            niche.ApplicantId = (int)transactionInDb.WithdrewColumbariumApplicantId;        

            var trackings = _tracking.GetTrackingByNicheId(transactionInDb.NicheId, true);

            foreach(var tracking in trackings)
            {
                tracking.ToDeleteFlag = false;
            }

            _unitOfWork.ColumbariumTransactions.Remove(transactionInDb);

            _unitOfWork.Complete();

            return true;
        }

        public bool RemoveWithdrew(int nicheId)
        {
            var trans = GetByNicheId(nicheId);

            if(trans.Count() != 1)
            {
                return false;
            }

            trans.ElementAt(0).DeletedUtcTime = DateTime.UtcNow;

            var trackings = _tracking.GetTrackingByNicheId(nicheId, true);

            foreach (var tracking in trackings)
            {
                _tracking.Remove(tracking.NicheId, tracking.ColumbariumTransactionAF);
            }

            return true;
        }
    }
}