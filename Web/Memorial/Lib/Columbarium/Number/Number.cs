using Memorial.Core;

namespace Memorial.Lib.Columbarium
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int columbariumItemId, int year)
        {
            return _unitOfWork.ColumbariumNumbers.GetNewAF(columbariumItemId, year);
        }

        public string GetNewIV(int columbariumItemId, int year)
        {
            return _unitOfWork.ColumbariumNumbers.GetNewIV(columbariumItemId, year);
        }

        public string GetNewRE(int columbariumItemId, int year)
        {
            return _unitOfWork.ColumbariumNumbers.GetNewRE(columbariumItemId, year);
        }

    }
}