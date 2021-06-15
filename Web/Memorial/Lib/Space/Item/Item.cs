using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;
        private Core.Domain.SpaceItem _item;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.SpaceItems.GetActive(id);
        }

        public Core.Domain.SpaceItem GetItem()
        {
            return _item;
        }

        public SpaceItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.SpaceItem, SpaceItemDto>(GetItem());
        }

        public Core.Domain.SpaceItem GetItem(int id)
        {
            return _unitOfWork.SpaceItems.GetActive(id);
        }

        public SpaceItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.SpaceItem, SpaceItemDto>(GetItem(id));
        }

        public IEnumerable<SpaceItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceItem>, IEnumerable<SpaceItemDto>>(_unitOfWork.SpaceItems.GetAllActive());
        }

        public int GetId()
        {
            return _item.Id;
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

        public bool AllowDoubleBook()
        {
            return _item.AllowDoubleBook;
        }

        public bool AllowDeposit()
        {
            return _item.AllowDeposit;
        }

        public IEnumerable<Core.Domain.SpaceItem> GetItemBySpace(int spaceId)
        {
            return _unitOfWork.SpaceItems.GetBySpace(spaceId);
        }

        public IEnumerable<SpaceItemDto> GetItemDtosBySpace(int spaceId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceItem>, IEnumerable<SpaceItemDto>>(GetItemBySpace(spaceId));
        }

        public IEnumerable<SubProductServiceDto> GetAvailableItemDtosBySpace(int spaceId)
        {
            if (spaceId == 0)
                return new HashSet<SubProductServiceDto>();

            var t = GetItemBySpace(spaceId);
            var sp = _subProductService.GetSubProductServicesByProduct(_product.GetSpaceProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id && y.DeleteDate == null));

            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(f);
        }

        public int Create(SpaceItemDto spaceItemDto)
        {
            _item = new Core.Domain.SpaceItem();
            Mapper.Map(spaceItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.SpaceItems.Add(_item);

            _unitOfWork.Complete();

            return _item.Id;
        }

        public bool Update(SpaceItemDto spaceItemDto)
        {
            var spaceItemInDB = GetItem(spaceItemDto.Id);

            if ((spaceItemInDB.SpaceId != spaceItemDto.SpaceDtoId
                || spaceItemInDB.isOrder != spaceItemDto.isOrder
                || spaceItemInDB.AllowDoubleBook != spaceItemDto.AllowDoubleBook
                || spaceItemInDB.AllowDeposit != spaceItemDto.AllowDeposit)
                && _unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItemId == spaceItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(spaceItemDto, spaceItemInDB);

            spaceItemInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItemId == id && ct.DeleteDate == null).Any())
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