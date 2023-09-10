using Memorial.Core;

namespace Memorial.Lib.Cremation
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int cremationItemId, int year)
        {
            return _unitOfWork.CremationNumbers.GetNewAF(cremationItemId, year);
        }

        public string GetNewIV(int cremationItemId, int year)
        {
            return _unitOfWork.CremationNumbers.GetNewIV(cremationItemId, year);
        }

        public string GetNewRE(int cremationItemId, int year)
        {
            return _unitOfWork.CremationNumbers.GetNewRE(cremationItemId, year);
        }
    }
}