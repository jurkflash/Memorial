using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int columbariumItemId, int year)
        {
            return _unitOfWork.ColumbariumNumbers.GetNewAF(columbariumItemId, year);
        }

        public string GetNewIV(int columbariumItemId, int year)
        {
            return _unitOfWork.ColumbariumNumbers.GetNewIV(columbariumItemId, year);
        }

    }
}