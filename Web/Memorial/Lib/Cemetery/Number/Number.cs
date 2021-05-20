using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Number : INumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public Number(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int cemeteryItemId, int year)
        {
            return _unitOfWork.PlotNumbers.GetNewAF(cemeteryItemId, year);
        }

        public string GetNewIV(int cemeteryItemId, int year)
        {
            return _unitOfWork.PlotNumbers.GetNewIV(cemeteryItemId, year);
        }

    }
}