using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Ancestor
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
            return _unitOfWork.AncestorNumbers.GetNewAF(ancestralTabletItemId, year);
        }

        public string GetNewIV(int ancestralTabletItemId, int year)
        {
            return _unitOfWork.AncestorNumbers.GetNewIV(ancestralTabletItemId, year);
        }

    }
}