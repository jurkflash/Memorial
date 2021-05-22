using Memorial.Core;

namespace Memorial.Lib.AncestralTablet
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int ancestralTabletItemId, int year)
        {
            return _unitOfWork.AncestralTabletNumbers.GetNewAF(ancestralTabletItemId, year);
        }

        public string GetNewIV(int ancestralTabletItemId, int year)
        {
            return _unitOfWork.AncestralTabletNumbers.GetNewIV(ancestralTabletItemId, year);
        }

    }
}