using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class Deceased : IDeceased
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.Deceased _deceased;

        public Deceased(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Deceased GetActive(int id)
        {
            _deceased = _unitOfWork.Deceaseds.GetActive(id);
            return _deceased;
        }

        public Core.Domain.Deceased GetByIC(string ic)
        {
            _deceased = _unitOfWork.Deceaseds.GetByIC(ic);
            return _deceased;
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            if (_deceased.QuadrangleId != null)
            {
                IQuadrangle quadrangle = new Lib.Quadrangle(_unitOfWork);
                quadrangle.SetById((int)_deceased.QuadrangleId);
                return quadrangle.GetQuadrangle();
            }
            return null;
        }

        public bool SetQuadrangle(int quadrangleId)
        {
            if (_deceased != null)
            {
                if (_deceased.QuadrangleId == null)
                {
                    _deceased.QuadrangleId = quadrangleId;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<DeceasedBriefDto> BriefDtosGetByApplicant(int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedBriefDto>>(_unitOfWork.Deceaseds.GetByApplicant(applicantId));
        }

    }
}