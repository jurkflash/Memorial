using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

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

    }
}