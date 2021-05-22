using System.Linq;
using Memorial.Lib.Deceased;
using Memorial.Core;

namespace Memorial.Lib.AncestralTablet
{
    public class AncestralTabletDeceased : IAncestralTabletDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeceased _deceased;
        private readonly IAncestralTablet _ancestralTablet;
        private readonly ITransaction _transaction;
        private readonly ITracking _tracking;

        public AncestralTabletDeceased(
            IUnitOfWork unitOfWork,
            IDeceased deceased,
            IAncestralTablet ancestralTablet,
            ITransaction transaction,
            ITracking tracking)
        {
            _unitOfWork = unitOfWork;
            _deceased = deceased;
            _ancestralTablet = ancestralTablet;
            _transaction = transaction;
            _tracking = tracking;

        }

        private bool Add(int id, int deceasedId, byte side)
        {
            _deceased.SetDeceased(deceasedId);
            if (_deceased.GetDeceased() == null)
                return false;

            _ancestralTablet.SetAncestralTablet(id);
            if (_ancestralTablet.GetAncestralTablet() == null)
                return false;

            _ancestralTablet.SetHasDeceased(true);

            _deceased.SetAncestralTablet(id);

            var tracking = _tracking.GetLatestFirstTransactionByAncestralTabletId(id);

            var transaction = _transaction.GetTransaction(tracking.AncestralTabletTransactionAF);

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
            _deceased.RemoveAncestralTabletDeceased();

            if (_deceased.GetDeceasedsByAncestralTabletId(id).Count() == 0)
            {
                _ancestralTablet.SetAncestralTablet(id);
                _ancestralTablet.SetHasDeceased(false);
            }

            var tracking = _tracking.GetLatestFirstTransactionByAncestralTabletId(id);

            var transaction = _transaction.GetTransaction(tracking.AncestralTabletTransactionAF);

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