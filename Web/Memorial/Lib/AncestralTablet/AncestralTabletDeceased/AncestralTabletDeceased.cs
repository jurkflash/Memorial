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
            var deceased = _deceased.GetById(deceasedId);
            if (deceased == null)
                return false;

            var ancestralTablet = _ancestralTablet.GetById(id);
            if (ancestralTablet == null)
                return false;

            ancestralTablet.hasDeceased = true;

            deceased.AncestralTabletId = id;

            var tracking = _tracking.GetLatestFirstTransactionByAncestralTabletId(id);

            var transaction = _transaction.GetByAF(tracking.AncestralTabletTransactionAF);

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
            if (_deceased.GetByAncestralTabletId(id).Count() == 1)
            {
                var ancestralTablet = _ancestralTablet.GetById(id);
                ancestralTablet.hasDeceased = false;
            }

            var deceased = _deceased.GetById(deceasedId);
            deceased.AncestralTablet = null;
            deceased.AncestralTabletId = null;

            var tracking = _tracking.GetLatestFirstTransactionByAncestralTabletId(id);

            var transaction = _transaction.GetByAF(tracking.AncestralTabletTransactionAF);

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