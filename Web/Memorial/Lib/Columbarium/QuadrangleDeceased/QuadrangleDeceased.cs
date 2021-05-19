using System.Linq;
using Memorial.Lib.Deceased;
using Memorial.Core;

namespace Memorial.Lib.Columbarium
{
    public class QuadrangleDeceased : IQuadrangleDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeceased _deceased;
        private readonly IQuadrangle _quadrangle;
        private readonly ITransaction _transaction;
        private readonly ITracking _tracking;

        public QuadrangleDeceased(
            IUnitOfWork unitOfWork,
            IDeceased deceased,
            IQuadrangle quadrangle,
            ITransaction transaction,
            ITracking tracking)
        {
            _unitOfWork = unitOfWork;
            _deceased = deceased;
            _quadrangle = quadrangle;
            _transaction = transaction;
            _tracking = tracking;

        }

        private bool Add(int id, int deceasedId, byte side)
        {
            _deceased.SetDeceased(deceasedId);
            if (_deceased.GetDeceased() == null)
                return false;

            _quadrangle.SetQuadrangle(id);
            if (_quadrangle.GetQuadrangle() == null)
                return false;

            _quadrangle.SetHasDeceased(true);

            _deceased.SetQuadrangle(id);

            var tracking = _tracking.GetLatestFirstTransactionByQuadrangleId(id);

            var transaction = _transaction.GetTransaction(tracking.QuadrangleTransactionAF);

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
            _deceased.SetDeceased(deceasedId);
            _deceased.RemoveQuadrangleDeceased();

            if (_deceased.GetDeceasedsByQuadrangleId(id).Count() == 0)
            {
                _quadrangle.SetQuadrangle(id);
                _quadrangle.SetHasDeceased(false);
            }

            var tracking = _tracking.GetLatestFirstTransactionByQuadrangleId(id);

            var transaction = _transaction.GetTransaction(tracking.QuadrangleTransactionAF);

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