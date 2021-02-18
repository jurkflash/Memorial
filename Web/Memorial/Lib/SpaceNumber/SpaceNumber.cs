using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class SpaceNumber : ISpaceNumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceNumber(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int spaceItemId, int year)
        {
            return _unitOfWork.SpaceNumbers.GetNewAF(spaceItemId, year);
        }

    }
}