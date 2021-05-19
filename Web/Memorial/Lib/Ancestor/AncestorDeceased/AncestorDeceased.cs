using System.Linq;
using Memorial.Lib.Deceased;
using Memorial.Core;

namespace Memorial.Lib.Ancestor
{
    public class AncestorDeceased : IAncestorDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeceased _deceased;
        private readonly IAncestor _ancestor;
        private readonly ITransaction _transaction;
        private readonly ITracking _tracking;

        public AncestorDeceased(
            IUnitOfWork unitOfWork,
            IDeceased deceased,
            IAncestor ancestor,
            ITransaction transaction,
            ITracking tracking)
        {
            _unitOfWork = unitOfWork;
            _deceased = deceased;
            _ancestor = ancestor;
            _transaction = transaction;
            _tracking = tracking;

        }

        private bool Add(int id, int deceasedId, byte side)
        {
            _deceased.SetDeceased(deceasedId);
            if (_deceased.GetDeceased() == null)
                return false;

            _ancestor.SetAncestor(id);
            if (_ancestor.GetAncestor() == null)
                return false;

            _ancestor.SetHasDeceased(true);

            _deceased.SetAncestor(id);

            var tracking = _tracking.GetLatestFirstTransactionByAncestorId(id);

            var transaction = _transaction.GetTransaction(tracking.AncestorTransactionAF);

            if (side == 1)
            {
                transaction.DeceasedId = deceasedId;
                tracking.DeceasedId = deceasedId;
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


        public bool Remove(int id, int deceasedId)
        {
            _deceased.SetDeceased(deceasedId);
            _deceased.RemoveAncestorDeceased();

            if (_deceased.GetDeceasedsByAncestorId(id).Count() == 0)
            {
                _ancestor.SetAncestor(id);
                _ancestor.SetHasDeceased(false);
            }

            var tracking = _tracking.GetLatestFirstTransactionByAncestorId(id);

            var transaction = _transaction.GetTransaction(tracking.AncestorTransactionAF);

            if (transaction.DeceasedId == deceasedId)
            {
                transaction.DeceasedId = null;
                tracking.DeceasedId = null;
            }

            _unitOfWork.Complete();

            return true;
        }
    }
}