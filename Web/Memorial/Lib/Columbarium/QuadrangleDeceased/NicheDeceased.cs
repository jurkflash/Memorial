using System.Linq;
using Memorial.Lib.Deceased;
using Memorial.Core;

namespace Memorial.Lib.Columbarium
{
    public class NicheDeceased : INicheDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeceased _deceased;
        private readonly INiche _niche;
        private readonly ITransaction _transaction;
        private readonly ITracking _tracking;

        public NicheDeceased(
            IUnitOfWork unitOfWork,
            IDeceased deceased,
            INiche niche,
            ITransaction transaction,
            ITracking tracking)
        {
            _unitOfWork = unitOfWork;
            _deceased = deceased;
            _niche = niche;
            _transaction = transaction;
            _tracking = tracking;

        }

        private bool Add(int id, int deceasedId, byte side)
        {
            _deceased.SetDeceased(deceasedId);
            if (_deceased.GetDeceased() == null)
                return false;

            _niche.SetNiche(id);
            if (_niche.GetNiche() == null)
                return false;

            _niche.SetHasDeceased(true);

            _deceased.SetNiche(id);

            var tracking = _tracking.GetLatestFirstTransactionByNicheId(id);

            var transaction = _transaction.GetTransaction(tracking.ColumbariumTransactionAF);

            if (side == 1)
            {
                transaction.Deceased1Id = deceasedId;
                tracking.Deceased1Id = deceasedId;
            }

            if (side == 2)
            {
                transaction.Deceased2Id = deceasedId;
                tracking.Deceased2Id = deceasedId;
            }

            return true;
        }

        public bool Add1(int id, int deceasedId)
        {
            if (!Add(id, deceasedId, 1))
                return false;

            _unitOfWork.Complete();

            return true;
        }

        public bool Add2(int id, int deceasedId)
        {
            if (!Add(id, deceasedId, 2))
                return false;

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id, int deceasedId)
        {
            if (_deceased.GetDeceasedsByNicheId(id).Count() == 1)
            {
                _niche.SetNiche(id);
                _niche.SetHasDeceased(false);
            }

            _deceased.SetDeceased(deceasedId);
            _deceased.RemoveNiche();

            var tracking = _tracking.GetLatestFirstTransactionByNicheId(id);

            var transaction = _transaction.GetTransaction(tracking.ColumbariumTransactionAF);

            if (transaction.Deceased1Id == deceasedId)
            {
                transaction.Deceased1Id = null;
                tracking.Deceased1Id = null;
            }

            if (transaction.Deceased2Id == deceasedId)
            {
                transaction.Deceased2Id = null;
                tracking.Deceased2Id = null;
            }

            _unitOfWork.Complete();

            return true;
        }
    }
}