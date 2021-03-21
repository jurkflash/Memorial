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

    }
}