using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using Memorial.Lib.Quadrangle;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Deceased
{
    public class Deceased : IDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private IQuadrangle _quadrangle;
        private IApplicantDeceased _appplicantDeceased;

        private Core.Domain.Deceased _deceased;

        public Deceased(IUnitOfWork unitOfWork, IQuadrangle quadrangle, IApplicantDeceased appplicantDeceased)
        {
            _unitOfWork = unitOfWork;
            _quadrangle = quadrangle;
            _appplicantDeceased = appplicantDeceased;
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

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsExcludeFilter(int applicantId, string deceasedName)
        {
            return _unitOfWork.Deceaseds.GetAllExcludeFilter(applicantId, deceasedName);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.Deceaseds.GetByQuadrangle(quadrangleId);
        }

        public int Create(DeceasedDto deceasedDto)
        {
            _deceased = new Core.Domain.Deceased();

            Mapper.Map(deceasedDto, _deceased);

            _deceased.CreateDate = System.DateTime.Now;

            _unitOfWork.Deceaseds.Add(_deceased);

            _unitOfWork.Complete();

            return _deceased.Id;
        }

        public bool Update(DeceasedDto deceasedDto)
        {
            var deceasedInDb = GetDeceased(deceasedDto.Id);

            Mapper.Map(deceasedDto, deceasedInDb);

            deceasedInDb.ModifyDate = System.DateTime.Now;

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
                var quadrangle = _quadrangle.GetQuadrangle(quadrangleId);
                if (quadrangle != null)
                {
                    _deceased.Quadrangle = quadrangle;
                    _deceased.QuadrangleId = quadrangleId;
                    return true;
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