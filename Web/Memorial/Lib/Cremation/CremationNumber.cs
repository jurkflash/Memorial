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
    public class CremationNumber : ICremationNumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public CremationNumber(IUnitOfWork unitOfWork)
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

    }
}