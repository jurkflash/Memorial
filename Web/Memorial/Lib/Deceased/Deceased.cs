using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using Memorial.Lib.Quadrangle;
using AutoMapper;

namespace Memorial.Lib.Deceased
{
    public class Deceased : IDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private IQuadrangle _quadrangle;

        private Core.Domain.Deceased _deceased;

        public Deceased(IUnitOfWork unitOfWork, IQuadrangle quadrangle)
        {
            _unitOfWork = unitOfWork;
            _quadrangle = quadrangle;
        }

        public void SetDeceased(int id)
        {
            _deceased = _unitOfWork.Deceaseds.GetActive(id);
        }

        public Core.Domain.Deceased GetDeceasedByIC(string ic)
        {
            return _unitOfWork.Deceaseds.GetByIC(ic);
        }

        public Core.Domain.Deceased GetDeceased()
        {
            return _deceased;
        }

        public DeceasedDto GetDeceasedDto()
        {
            return Mapper.Map<Core.Domain.Deceased, DeceasedDto>(_deceased);
        }

        public Core.Domain.Deceased GetDeceased(int id)
        {
            return _unitOfWork.Deceaseds.GetActive(id);
        }

        public DeceasedDto GetDeceasedDto(int id)
        {
            return Mapper.Map<Core.Domain.Deceased, DeceasedDto>(GetDeceased(id));
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByApplicantId(int applicantId)
        {
            return _unitOfWork.Deceaseds.GetByApplicant(applicantId);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.Deceaseds.GetByQuadrangle(quadrangleId);
        }

        public bool Create(Core.Domain.Deceased deceased)
        {
            deceased.CreateDate = System.DateTime.Now;
            _unitOfWork.Deceaseds.Add(deceased);
            _unitOfWork.Complete();
            return true;
        }

        public bool Update(Core.Domain.Deceased deceased)
        {
            SetDeceased(deceased.Id);
            Mapper.Map(deceased, GetDeceased());
            deceased.ModifyDate = System.DateTime.Now;
            _unitOfWork.Complete();
            return true;
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            if (_deceased.QuadrangleId != null)
            {
                _quadrangle.SetQuadrangle((int)_deceased.QuadrangleId);
                return _quadrangle.GetQuadrangle();
            }
            return null;
        }

        public bool SetQuadrangle(int quadrangleId)
        {
            if (_deceased != null)
            {
                if (_deceased.QuadrangleId == null)
                {
                    _quadrangle.SetQuadrangle(quadrangleId);
                    if (_quadrangle.GetQuadrangle() != null)
                    {
                        _deceased.Quadrangle = _quadrangle.GetQuadrangle();
                        _deceased.QuadrangleId = quadrangleId;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveQuadrangle()
        {
            if (_deceased != null)
            {
                _deceased.Quadrangle = null;
                _deceased.QuadrangleId = null;
                return true;
            }
            return false;
        }

        public IEnumerable<DeceasedBriefDto> GetDeceasedBriefDtosByApplicantId(int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedBriefDto>>(GetDeceasedsByApplicantId(applicantId));
        }

    }
}