using Memorial.Core;

namespace Memorial.Lib.Urn
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int urnItemId, int year)
        {
            return _unitOfWork.UrnNumbers.GetNewAF(urnItemId, year);
        }

        public string GetNewIV(int urnItemId, int year)
        {
            return _unitOfWork.UrnNumbers.GetNewIV(urnItemId, year);
        }

        public string GetNewRE(int urnItemId, int year)
        {
            return _unitOfWork.UrnNumbers.GetNewRE(urnItemId, year);
        }

    }
}