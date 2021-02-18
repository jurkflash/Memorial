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
        public Deceased(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DeceasedBriefDto> BriefDtosGetByApplicant(int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedBriefDto>>(_unitOfWork.Deceaseds.GetByApplicant(applicantId));
        }

    }
}