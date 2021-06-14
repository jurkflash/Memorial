using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.Urn
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;
        private Core.Domain.UrnItem _item;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.UrnItems.GetActive(id);
        }

        public Core.Domain.UrnItem GetItem()
        {
            return _item;
        }

        public UrnItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.UrnItem, UrnItemDto>(GetItem());
        }

        public Core.Domain.UrnItem GetItem(int id)
        {
            return _unitOfWork.UrnItems.GetActive(id);
        }

        public UrnItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.UrnItem, UrnItemDto>(GetItem(id));
        }

        public IEnumerable<UrnItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(_unitOfWork.UrnItems.GetAllActive());
        }

        public int GetId()
        {
            return _item.Id;
        }

        public int GetUrnId()
        {
            return _item.UrnId;
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

        public IEnumerable<Core.Domain.UrnItem> GetItemByUrn(int urnId)
        {
            return _unitOfWork.UrnItems.GetByUrn(urnId);
        }

        public IEnumerable<UrnItemDto> GetItemDtosByUrn(int urnId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(GetItemByUrn(urnId));
        }

        public IEnumerable<SubProductServiceDto> GetAvailableItemDtosByUrn(int urnId)
        {
            if (urnId == 0)
                return new HashSet<SubProductServiceDto>();

            var t = GetItemByUrn(urnId);
            var sp = _subProductService.GetSubProductServicesByProduct(_product.GetUrnProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id && y.DeleteDate == null));

            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(f);
        }

        public int Create(UrnItemDto urnItemDto)
        {
            _item = new Core.Domain.UrnItem();
            Mapper.Map(urnItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.UrnItems.Add(_item);

            _unitOfWork.Complete();

            return _item.Id;
        }

        public bool Update(UrnItemDto urnItemDto)
        {
            var urnItemInDB = GetItem(urnItemDto.Id);

            if ((urnItemInDB.UrnId != urnItemDto.UrnDtoId
                || urnItemInDB.isOrder != urnItemDto.isOrder)
                && _unitOfWork.UrnTransactions.Find(ct => ct.UrnItemId == urnItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(urnItemDto, urnItemInDB);

            urnItemInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.UrnTransactions.Find(ct => ct.UrnItemId == id && ct.DeleteDate == null).Any())
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