using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private readonly IMiscellaneous _miscellaneous;

        public Config(
            IUnitOfWork unitOfWork,
            IItem item,
            IMiscellaneous miscellaneous
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _miscellaneous = miscellaneous;
        }

        public MiscellaneousDto GetMiscellaneousDto(int id)
        {
            return _miscellaneous.GetMiscellaneousDto(id);
        }

        public IEnumerable<MiscellaneousDto> GetMiscellaneousDtos()
        {
            return _miscellaneous.GetMiscellaneousDtos();
        }

        public MiscellaneousItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<MiscellaneousItemDto> GetItemDtos()
        {
            return _item.GetItemDtos();
        }

        public IEnumerable<MiscellaneousNumber> GetNumbers()
        {
            return _unitOfWork.MiscellaneousNumbers.GetAll();
        }


        public bool CreateMiscellaneous(MiscellaneousDto miscellaneousDto)
        {
            if (_miscellaneous.Create(miscellaneousDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateMiscellaneous(MiscellaneousDto miscellaneousDto)
        {
            var miscellaneousInDB = _miscellaneous.GetMiscellaneous(miscellaneousDto.Id);

            if (miscellaneousInDB.SiteId != miscellaneousDto.SiteDtoId
                && _unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItem.Miscellaneous.SiteId == miscellaneousInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(miscellaneousDto, miscellaneousInDB);

            if (_miscellaneous.Update(miscellaneousInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteMiscellaneous(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItem.Miscellaneous.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_miscellaneous.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


        public bool CreateItem(MiscellaneousItemDto miscellaneousItemDto)
        {
            if (_item.Create(miscellaneousItemDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateItem(MiscellaneousItemDto miscellaneousItemDto)
        {
            var miscellaneousItemInDB = _item.GetItem(miscellaneousItemDto.Id);

            if ((miscellaneousItemInDB.MiscellaneousId != miscellaneousItemDto.MiscellaneousDtoId
                || miscellaneousItemInDB.isOrder != miscellaneousItemDto.isOrder)
                && _unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItemId == miscellaneousItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(miscellaneousItemDto, miscellaneousItemInDB);

            if (_item.Update(miscellaneousItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItemId == id && ct.DeleteDate == null).Any())
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