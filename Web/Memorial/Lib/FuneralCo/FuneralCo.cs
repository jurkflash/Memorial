using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.FuneralCo
{
    public class FuneralCo : IFuneralCo
    {
        private readonly IUnitOfWork _unitOfWork;
        public FuneralCo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FuneralCompanyDto> GetFuneralCompanyDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.FuneralCompany>, IEnumerable<FuneralCompanyDto>>(_unitOfWork.FuneralCompanies.GetAllActive());
        }

    }
}