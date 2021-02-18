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
    public class Miscellaneous : IMiscellaneous
    {
        private readonly IUnitOfWork _unitOfWork;

        public Miscellaneous(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MiscellaneousDto> DtosGetBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Miscellaneous>, IEnumerable<MiscellaneousDto>>(_unitOfWork.Miscellaneous.GetBySite(siteId));
        }


        

    }
}