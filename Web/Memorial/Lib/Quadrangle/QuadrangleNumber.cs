﻿using System;
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
    public class QuadrangleNumber : IQuadrangleNumber
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuadrangleNumber(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNewAF(int quadrangleItemId, int year)
        {
            return _unitOfWork.QuadrangleNumbers.GetNewAF(quadrangleItemId, year);
        }

        public string GetNewIV(int quadrangleItemId, int year)
        {
            return _unitOfWork.QuadrangleNumbers.GetNewIV(quadrangleItemId, year);
        }

    }
}