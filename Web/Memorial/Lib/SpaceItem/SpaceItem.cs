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
    public class SpaceItem : ISpaceItem
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceItem(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SpaceItemDto> DtosGetBySpace(int spaceId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceItem>, IEnumerable<SpaceItemDto>>(_unitOfWork.SpaceItems.GetBySpace(spaceId));
        }

        public bool IsOrderFlag(int spaceItemId)
        {
            return _unitOfWork.SpaceItems.Get(spaceItemId).isOrder;
        }

        public bool AllowDoubleBook(int spaceItemId)
        {
            return _unitOfWork.SpaceItems.Get(spaceItemId).AllowDoubleBook;
        }
    }
}