using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Lib.Columbarium
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWithdraw _withdraw;
        private readonly ITracking _tracking;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IWithdraw withdraw,
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
            _withdraw = withdraw;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
            _tracking = tracking;
        }

        public bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            if (columbariumTransaction.Deceased1Id != null)
            {
                return false;
            }

            columbariumTransaction.AF = _number.GetNewAF(columbariumTransaction.ColumbariumItemId, System.DateTime.Now.Year);

            SummaryItem(columbariumTransaction);

            _unitOfWork.ColumbariumTransactions.Add(columbariumTransaction);

            var niche = _niche.GetById(columbariumTransaction.NicheId);
            niche.ApplicantId = columbariumTransaction.ApplicantId;

            if (columbariumTransaction.Deceased1Id != null)
            {
                var deceased = _deceased.GetById((int)columbariumTransaction.Deceased1Id);
                deceased.NicheId = columbariumTransaction.NicheId;
                niche.hasDeceased = true;
            }

            if (columbariumTransaction.Deceased2Id != null)
            {
                var deceased = _deceased.GetById((int)columbariumTransaction.Deceased2Id);
                deceased.NicheId = columbariumTransaction.NicheId;
                niche.hasDeceased = true;
            }

            _tracking.Add(columbariumTransaction.NicheId, columbariumTransaction.AF, columbariumTransaction.ApplicantId, columbariumTransaction.Deceased1Id, columbariumTransaction.Deceased2Id);

            _withdraw.RemoveWithdrew(columbariumTransaction.NicheId);

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

            var deceased1InDb = columbariumTransactionInDb.Deceased1Id;

            var deceased2InDb = columbariumTransactionInDb.Deceased2Id;

            var niche = _niche.GetById(columbariumTransaction.NicheId);

            NicheApplicantDeceaseds(niche, columbariumTransaction.Deceased1Id, deceased1InDb);

            NicheApplicantDeceaseds(niche, columbariumTransaction.Deceased2Id, deceased2InDb);

            if (columbariumTransaction.Deceased1Id == null && columbariumTransaction.Deceased2Id == null)
                niche.hasDeceased = false;

            _tracking.Change(columbariumTransaction.NicheId, columbariumTransaction.AF, columbariumTransaction.ApplicantId, columbariumTransaction.Deceased1Id, columbariumTransaction.Deceased2Id);

            _unitOfWork.Complete();

            return true;
        }

        private bool NicheApplicantDeceaseds(Core.Domain.Niche niche, int? deceasedId, int? dbDeceasedId)
        {
            if (deceasedId != dbDeceasedId)
            {
                if (deceasedId == null)
                {
                    var deceased = _deceased.GetById((int)dbDeceasedId);
                    deceased.Niche = null;
                    deceased.NicheId = null;
                }
                else
                {
                    var deceased = _deceased.GetById((int)deceasedId);
                    if (deceased.NicheId != null && deceased.NicheId != niche.Id)
                    {
                        return false;
                    }
                    else
                    {
                        deceased.NicheId = niche.Id;
                        niche.hasDeceased = true;
                    }
                }
            }

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

            DeleteAllTransactionWithSameNicheId(transactionInDb.NicheId);

            var niche = _niche.GetById(transactionInDb.NicheId);

            var deceaseds = _deceased.GetByNicheId(transactionInDb.NicheId);

            foreach (var deceased in deceaseds)
            {
                deceased.Niche = null;
                deceased.NicheId = null;
            }

            niche.hasDeceased = false;
            niche.Applicant = null;
            niche.ApplicantId = null;

            _tracking.Delete(transactionInDb.AF);

            _unitOfWork.Complete();

            return true;
        }

        private void SummaryItem(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var niche = _niche.GetById(columbariumTransaction.NicheId);

            columbariumTransaction.SummaryItem = "AF: " + columbariumTransaction.AF + "<BR/>" +
                Resources.Mix.Niche + ": " + niche.Name + "<BR/>" +
                Resources.Mix.Type + ": " + niche.NicheType.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + columbariumTransaction.Remark;
        }
    }
}