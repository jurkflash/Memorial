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
    public class MiscellaneousNumber : IMiscellaneousNumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public MiscellaneousNumber(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int miscellaneousItemId, int year)
        {
            return _unitOfWork.MiscellaneousNumbers.GetNewAF(miscellaneousItemId, year);
        }

        public string GetNewIV(int miscellaneousItemId, int year)
        {
            return _unitOfWork.MiscellaneousNumbers.GetNewIV(miscellaneousItemId, year);
        }

    }
}