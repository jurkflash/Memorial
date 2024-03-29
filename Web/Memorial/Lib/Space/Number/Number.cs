﻿using Memorial.Core;

namespace Memorial.Lib.Space
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int spaceItemId, int year)
        {
            return _unitOfWork.SpaceNumbers.GetNewAF(spaceItemId, year);
        }

        public string GetNewIV(int spaceItemId, int year)
        {
            return _unitOfWork.SpaceNumbers.GetNewIV(spaceItemId, year);
        }

        public string GetNewRE(int spaceItemId, int year)
        {
            return _unitOfWork.SpaceNumbers.GetNewRE(spaceItemId, year);
        }
    }
}