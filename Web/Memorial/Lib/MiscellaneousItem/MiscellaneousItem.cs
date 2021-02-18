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
    public class MiscellaneousItem : IMiscellaneousItem
    {
        private readonly IUnitOfWork _unitOfWork;

        public MiscellaneousItem(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MiscellaneousItemDto> DtosGetByMiscellaneous(int miscellaneousId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.MiscellaneousItem>, IEnumerable<MiscellaneousItemDto>>(_unitOfWork.MiscellaneousItems.GetByMiscellaneous(miscellaneousId));
        }

        public bool IsOrderFlag(int miscellaneousItemId)
        {
            return _unitOfWork.MiscellaneousItems.Get(miscellaneousItemId).isOrder;
        }
        

    }
}