using Memorial.Core;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;

namespace Memorial.Lib.Columbarium
{
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITracking _tracking;
        private readonly IManage _manage;
        private readonly IPhoto _photo;

        public Shift(
            IUnitOfWork unitOfWork,
            IItem item,
            INiche niche,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number,
            ITracking tracking,
            IManage manage,
            IPhoto photo
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
            _manage = manage;
            _photo = photo;
        }

        private bool ShiftNicheApplicantDeceaseds(int oldQuadranlgeId, int newNicheId, int newApplicantId)
        {
            var oldNiche = _niche.GetById(oldQuadranlgeId);

            var deceaseds = _deceased.GetByNicheId(oldQuadranlgeId);

            foreach (var deceased in deceaseds)
            {
                deceased.NicheId = newNicheId;
            }

            if (deceaseds.Any())
            {
                oldNiche.hasDeceased = false;
            }
            oldNiche.Applicant = null;
            oldNiche.ApplicantId = null;

            var newNiche = _niche.GetById(newNicheId);
            newNiche.ApplicantId = newApplicantId;
            newNiche.hasDeceased = deceaseds.Any();

            return true;
        }

        public bool Add(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var niche = _niche.GetById(columbariumTransaction.NicheId);
            if (niche.ApplicantId != null)
                return false;

            if(!SetTransactionDeceasedIdBasedOnNiche(columbariumTransaction, (int)columbariumTransaction.ShiftedNicheId))
                return false;

            columbariumTransaction.ShiftedColumbariumTransactionAF = _tracking.GetLatestFirstTransactionByNicheId((int)columbariumTransaction.ShiftedNicheId).ColumbariumTransactionAF;

            GetByAF(columbariumTransaction.ShiftedColumbariumTransactionAF).DeletedUtcTime = System.DateTime.UtcNow;

            _tracking.Remove((int)columbariumTransaction.ShiftedNicheId, columbariumTransaction.ShiftedColumbariumTransactionAF);

            columbariumTransaction.AF = _number.GetNewAF(columbariumTransaction.ColumbariumItemId, System.DateTime.Now.Year);

            SummaryItem(columbariumTransaction);

            _unitOfWork.ColumbariumTransactions.Add(columbariumTransaction);

            ShiftNicheApplicantDeceaseds((int)columbariumTransaction.ShiftedNicheId, columbariumTransaction.NicheId, columbariumTransaction.ApplicantId);

            _manage.ChangeNiche((int)columbariumTransaction.ShiftedNicheId, columbariumTransaction.NicheId);

            _photo.ChangeNiche((int)columbariumTransaction.ShiftedNicheId, columbariumTransaction.NicheId);
                
            _tracking.Add(columbariumTransaction.NicheId, columbariumTransaction.AF, columbariumTransaction.ApplicantId, columbariumTransaction.Deceased1Id, columbariumTransaction.Deceased2Id);

            _unitOfWork.Complete();

            return true;
        }

        public bool Change(string AF, Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var invoices = _unitOfWork.Invoices.GetByActiveColumbariumAF(columbariumTransaction.AF).ToList();

            if (invoices.Any() && GetTotalAmount(columbariumTransaction) < invoices.Max(i => i.Amount))
                return false;

            var columbariumTransactionInDb = GetByAF(AF);

            if (columbariumTransactionInDb.NicheId != columbariumTransaction.NicheId)
            {
                if (!SetTransactionDeceasedIdBasedOnNiche(columbariumTransaction, columbariumTransactionInDb.NicheId))
                    return false;

                _tracking.Remove(columbariumTransactionInDb.NicheId, columbariumTransaction.AF);

                _tracking.Add(columbariumTransaction.NicheId, columbariumTransaction.AF, columbariumTransaction.ApplicantId, columbariumTransaction.Deceased1Id, columbariumTransaction.Deceased2Id);

                ShiftNicheApplicantDeceaseds(columbariumTransactionInDb.NicheId, columbariumTransaction.NicheId, columbariumTransaction.ApplicantId);

                _manage.ChangeNiche(columbariumTransactionInDb.NicheId, columbariumTransaction.NicheId);

                _photo.ChangeNiche(columbariumTransactionInDb.NicheId, columbariumTransaction.NicheId);

                SummaryItem(columbariumTransaction);

                columbariumTransactionInDb.Price = columbariumTransaction.Price;
                columbariumTransactionInDb.Maintenance = columbariumTransaction.Maintenance;
                columbariumTransactionInDb.LifeTimeMaintenance = columbariumTransaction.LifeTimeMaintenance;
                columbariumTransactionInDb.SummaryItem = columbariumTransaction.SummaryItem;
                columbariumTransactionInDb.Remark = columbariumTransaction.Remark;

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Remove(string AF)
        {
            var transactionInDb = _unitOfWork.ColumbariumTransactions.GetByAF(AF);

            if (GetByShiftedColumbariumTransactionAF(AF) != null)
                return false;

            if (!_tracking.IsLatestTransaction(transactionInDb.NicheId, AF))
                return false;

            if (_unitOfWork.Invoices.GetByActiveColumbariumAF(AF).Any())
                return false;

            if (_unitOfWork.Receipts.GetByColumbariumAF(AF).Any())
                return false;


            var shiftedNiche = _niche.GetById((int)transactionInDb.ShiftedNicheId);
            if (shiftedNiche.ApplicantId != null)
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


            var previousTransaction = GetTransactionExclusive(transactionInDb.ShiftedColumbariumTransactionAF);

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

            previousTransaction.DeletedUtcTime = null;

            _tracking.Add(previousTransaction.NicheId, previousTransaction.AF, previousTransaction.ApplicantId, previousTransaction.Deceased1Id, previousTransaction.Deceased2Id);

            _manage.ChangeNiche(transactionInDb.NicheId, previousTransaction.NicheId);

            _photo.ChangeNiche(transactionInDb.NicheId, previousTransaction.NicheId);

            _unitOfWork.Complete();
            
            return true;
        }

        private void SummaryItem(Core.Domain.ColumbariumTransaction columbariumTransaction)
        {
            var niche = _niche.GetById(columbariumTransaction.NicheId);

            columbariumTransaction.SummaryItem = "AF: " + columbariumTransaction.AF + "<BR/>" +
                Resources.Mix.Niche + ": " + _niche.GetById((int)columbariumTransaction.ShiftedNicheId).Name + "<BR/>" +
                Resources.Mix.ShiftTo + "<BR/>" +
                Resources.Mix.Niche + ": " + niche.Name + "<BR/>" +
                Resources.Mix.Remark + ": " + columbariumTransaction.Remark;
        }

    }
}