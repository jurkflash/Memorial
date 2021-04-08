using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Urn
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private readonly IUrn _urn;

        public Config(
            IUnitOfWork unitOfWork,
            IItem item,
            IUrn urn
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _urn = urn;
        }

        public UrnDto GetUrnDto(int id)
        {
            return _urn.GetUrnDto(id);
        }

        public IEnumerable<UrnDto> GetUrnDtos()
        {
            return _urn.GetUrnDtos();
        }

        public UrnItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<UrnItemDto> GetItemDtos()
        {
            return _item.GetItemDtos();
        }

        public IEnumerable<UrnNumber> GetNumbers()
        {
            return _unitOfWork.UrnNumbers.GetAll();
        }


        public bool CreateUrn(UrnDto urnDto)
        {
            if (_urn.Create(urnDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateUrn(UrnDto urnDto)
        {
            var urnInDB = _urn.GetUrn(urnDto.Id);

            if (urnInDB.SiteId != urnDto.SiteId
                && _unitOfWork.UrnTransactions.Find(ct => ct.UrnItem.Urn.SiteId == urnInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(urnDto, urnInDB);

            if (_urn.Update(urnInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteUrn(int id)
        {
            if (_unitOfWork.UrnTransactions.Find(ct => ct.UrnItem.Urn.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_urn.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


        public bool CreateItem(UrnItemDto urnItemDto)
        {
            if (_item.Create(urnItemDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateItem(UrnItemDto urnItemDto)
        {
            var urnItemInDB = _item.GetItem(urnItemDto.Id);

            if ((urnItemInDB.UrnId != urnItemDto.UrnId
                || urnItemInDB.isOrder != urnItemDto.isOrder)
                && _unitOfWork.UrnTransactions.Find(ct => ct.UrnItemId == urnItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(urnItemDto, urnItemInDB);

            if (_item.Update(urnItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.UrnTransactions.Find(ct => ct.UrnItemId == id && ct.DeleteDate == null).Any())
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