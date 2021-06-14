using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;
        private Core.Domain.MiscellaneousItem _item;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.MiscellaneousItems.GetActive(id);
        }

        public Core.Domain.MiscellaneousItem GetItem()
        {
            return _item;
        }

        public MiscellaneousItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.MiscellaneousItem, MiscellaneousItemDto>(GetItem());
        }

        public Core.Domain.MiscellaneousItem GetItem(int id)
        {
            return _unitOfWork.MiscellaneousItems.GetActive(id);
        }

        public MiscellaneousItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.MiscellaneousItem, MiscellaneousItemDto>(GetItem(id));
        }

        public IEnumerable<MiscellaneousItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.MiscellaneousItem>, IEnumerable<MiscellaneousItemDto>>(_unitOfWork.MiscellaneousItems.GetAllActive());
        }

        public int GetId()
        {
            return _item.Id;
        }

        public int GetMiscellaneousId()
        {
            return _item.MiscellaneousId;
        }

        public string GetName()
        {
            return _item.SubProductService.Name;
        }

        public string GetDescription()
        {
            return _item.SubProductService.Description;
        }

        public float GetPrice()
        {
            if (_item.Price.HasValue)
                return _item.Price.Value;
            else
                return _item.SubProductService.Price;
        }

        public string GetSystemCode()
        {
            return _item.SubProductService.SystemCode;
        }

        public bool IsOrder()
        {
            if (_item.isOrder.HasValue)
                return _item.isOrder.Value;
            else
                return _item.SubProductService.isOrder;
        }

        public IEnumerable<Core.Domain.MiscellaneousItem> GetItemByMiscellaneous(int miscellaneousId)
        {
            return _unitOfWork.MiscellaneousItems.GetByMiscellaneous(miscellaneousId);
        }

        public IEnumerable<MiscellaneousItemDto> GetItemDtosByMiscellaneous(int miscellaneousId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.MiscellaneousItem>, IEnumerable<MiscellaneousItemDto>>(GetItemByMiscellaneous(miscellaneousId));
        }

        public IEnumerable<SubProductServiceDto> GetAvailableItemDtosByMiscellaneous(int miscellaneousId)
        {
            if (miscellaneousId == 0)
                return new HashSet<SubProductServiceDto>();

            var t = GetItemByMiscellaneous(miscellaneousId);
            var sp = _subProductService.GetSubProductServicesByProduct(_product.GetMiscellaneousProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id && y.DeleteDate == null));

            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(f);
        }

        public int Create(MiscellaneousItemDto miscellaneousItemDto)
        {
            _item = new Core.Domain.MiscellaneousItem();
            Mapper.Map(miscellaneousItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.MiscellaneousItems.Add(_item);

            _unitOfWork.Complete();

            return _item.Id;
        }

        public bool Update(MiscellaneousItemDto miscellaneousItemDto)
        {
            var miscellaneousItemInDB = GetItem(miscellaneousItemDto.Id);

            if ((miscellaneousItemInDB.MiscellaneousId != miscellaneousItemDto.MiscellaneousDtoId
                || miscellaneousItemInDB.isOrder != miscellaneousItemDto.isOrder)
                && _unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItemId == miscellaneousItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(miscellaneousItemDto, miscellaneousItemInDB);

            miscellaneousItemInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItemId == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            SetItem(id);

            _item.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}