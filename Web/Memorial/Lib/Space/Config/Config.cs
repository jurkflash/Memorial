using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private readonly ISpace _space;

        public Config(
            IUnitOfWork unitOfWork,
            IItem item,
            ISpace space
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _space = space;
        }

        public SpaceDto GetSpaceDto(int id)
        {
            return _space.GetSpaceDto(id);
        }

        public IEnumerable<SpaceDto> GetSpaceDtos()
        {
            return _space.GetSpaceDtos();
        }

        public SpaceItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<SpaceItemDto> GetItemDtos()
        {
            return _item.GetItemDtos();
        }

        public IEnumerable<SpaceNumber> GetNumbers()
        {
            return _unitOfWork.SpaceNumbers.GetAll();
        }


        public bool CreateSpace(SpaceDto spaceDto)
        {
            if (_space.Create(spaceDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateSpace(SpaceDto spaceDto)
        {
            var spaceInDB = _space.GetSpace(spaceDto.Id);

            if (spaceInDB.SiteId != spaceDto.SiteId
                && _unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItem.Space.SiteId == spaceInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(spaceDto, spaceInDB);

            if (_space.Update(spaceInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteSpace(int id)
        {
            if (_unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItem.Space.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_space.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


        public bool CreateItem(SpaceItemDto spaceItemDto)
        {
            if (_item.Create(spaceItemDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateItem(SpaceItemDto spaceItemDto)
        {
            var spaceItemInDB = _item.GetItem(spaceItemDto.Id);

            if ((spaceItemInDB.SpaceId != spaceItemDto.SpaceId
                || spaceItemInDB.isOrder != spaceItemDto.isOrder
                || spaceItemInDB.AllowDoubleBook != spaceItemDto.AllowDoubleBook
                || spaceItemInDB.AllowDeposit != spaceItemDto.AllowDeposit)
                && _unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItemId == spaceItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(spaceItemDto, spaceItemInDB);

            if (_item.Update(spaceItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItemId == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_item.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }




    }
}