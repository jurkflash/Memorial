﻿using System;
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

        public string GetNewAF(int ancestorItemId, int year)
        {
            return _unitOfWork.AncestorNumbers.GetNewAF(ancestorItemId, year);
        }

        public string GetNewIV(int ancestorItemId, int year)
        {
            return _unitOfWork.AncestorNumbers.GetNewIV(ancestorItemId, year);
        }

    }
}