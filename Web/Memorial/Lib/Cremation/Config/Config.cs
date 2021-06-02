using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private readonly ICremation _cremation;

        public Config(
            IUnitOfWork unitOfWork,
            IItem item,
            ICremation cremation
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _cremation = cremation;
        }

        public CremationDto GetCremationDto(int id)
        {
            return _cremation.GetCremationDto(id);
        }

        public IEnumerable<CremationDto> GetCremationDtos()
        {
            return _cremation.GetCremationDtos();
        }

        public CremationItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<CremationItemDto> GetItemDtos()
        {
            return _item.GetItemDtos();
        }

        public IEnumerable<CremationNumber> GetNumbers()
        {
            return _unitOfWork.CremationNumbers.GetAll();
        }


        public bool CreateCremation(CremationDto cremationDto)
        {
            if (_cremation.Create(cremationDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateCremation(CremationDto cremationDto)
        {
            var cremationInDB = _cremation.GetCremation(cremationDto.Id);

            if (cremationInDB.SiteId != cremationDto.SiteDtoId
                && _unitOfWork.CremationTransactions.Find(ct => ct.CremationItem.Cremation.SiteId == cremationInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cremationDto, cremationInDB);

            if (_cremation.Update(cremationInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteCremation(int id)
        {
            if (_unitOfWork.CremationTransactions.Find(ct => ct.CremationItem.Cremation.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_cremation.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


        public bool CreateItem(CremationItemDto cremationItemDto)
        {
            if (_item.Create(cremationItemDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateItem(CremationItemDto cremationItemDto)
        {
            var cremationItemInDB = _item.GetItem(cremationItemDto.Id);

            if ((cremationItemInDB.CremationId != cremationItemDto.CremationDtoId
                || cremationItemInDB.isOrder != cremationItemDto.isOrder)
                && _unitOfWork.CremationTransactions.Find(ct => ct.CremationItemId == cremationItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cremationItemDto, cremationItemInDB);

            if (_item.Update(cremationItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.CremationTransactions.Find(ct => ct.CremationItemId == id && ct.DeleteDate == null).Any())
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