﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Cremation : ICremation
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Cremation _cremation;

        public Cremation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetCremation(int id)
        {
            _cremation = _unitOfWork.Cremations.GetActive(id);
        }

        public Core.Domain.Cremation GetCremation()
        {
            return _cremation;
        }

        public CremationDto GetCremationDto()
        {
            return Mapper.Map<Core.Domain.Cremation, CremationDto>(GetCremation());
        }

        public Core.Domain.Cremation GetCremation(int id)
        {
            return _unitOfWork.Cremations.GetActive(id);
        }

        public CremationDto GetCremationDto(int id)
        {
            return Mapper.Map<Core.Domain.Cremation, CremationDto>(GetCremation(id));
        }

        public IEnumerable<Core.Domain.Cremation> GetCremationBySite(byte siteId)
        {
            return _unitOfWork.Cremations.GetBySite(siteId);
        }

        public IEnumerable<CremationDto> GetCremationDtosBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Cremation>, IEnumerable<CremationDto>>(_unitOfWork.Cremations.GetBySite(siteId));
        }

        public string GetName()
        {
            return _cremation.Name;
        }

        public string GetDescription()
        {
            return _cremation.Description;
        }

    }
}