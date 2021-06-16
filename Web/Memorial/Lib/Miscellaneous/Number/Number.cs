using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
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

        public string GetNewRE(int miscellaneousItemId, int year)
        {
            return _unitOfWork.MiscellaneousNumbers.GetNewRE(miscellaneousItemId, year);
        }

    }
}