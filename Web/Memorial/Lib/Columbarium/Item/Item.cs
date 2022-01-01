using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;
        private Core.Domain.ColumbariumItem _item;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.ColumbariumItems.GetActive(id);
        }

        public Core.Domain.ColumbariumItem GetItem()
        {
            return _item;
        }

        public ColumbariumItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.ColumbariumItem, ColumbariumItemDto>(GetItem());
        }

        public Core.Domain.ColumbariumItem GetItem(int id)
        {
            return _unitOfWork.ColumbariumItems.GetActive(id);
        }

        public ColumbariumItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.ColumbariumItem, ColumbariumItemDto>(GetItem(id));
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

        public float GetAmountWithDateRange(int itemId, DateTime from, DateTime to)
        {
            SetItem(itemId);

            if (from > to)
                return -1;

            var total = (((to.Year - from.Year) * 12) + to.Month - from.Month) * GetPrice();

            return total;
        }

        public IEnumerable<Core.Domain.ColumbariumItem> GetItemByCentre(int centreId)
        {
            return _unitOfWork.ColumbariumItems.GetByCentre(centreId);
        }

        public IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumItem>, IEnumerable<ColumbariumItemDto>>(GetItemByCentre(centreId));
        }

        public IEnumerable<SubProductServiceDto> GetAvailableItemDtosByCentre(int centreId)
        {
            if (centreId == 0)
                return new HashSet<SubProductServiceDto>();

            var t = GetItemByCentre(centreId);
            var sp = _subProductService.GetSubProductServicesByProduct(_product.GetColumbariumProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id && y.DeletedDate == null));

            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(f);
        }


        public int Create(ColumbariumItemDto columbariumItemDto)
        {
            _item = new Core.Domain.ColumbariumItem();
            Mapper.Map(columbariumItemDto, _item);

            _unitOfWork.ColumbariumItems.Add(_item);

            _unitOfWork.Complete();

            return _item.Id;
        }

        public bool Update(ColumbariumItemDto columbariumItemDto)
        {
            var columbariumItemInDB = GetItem(columbariumItemDto.Id);

            if ((columbariumItemInDB.isOrder != columbariumItemDto.isOrder
                || columbariumItemInDB.ColumbariumCentreId != columbariumItemDto.ColumbariumCentreDtoId)
                && _unitOfWork.ColumbariumTransactions.Find(qi => qi.ColumbariumItemId == columbariumItemDto.Id && qi.DeletedDate == null).Any())
            {
                return false;
            }

            Mapper.Map(columbariumItemDto, columbariumItemInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItemId == id && qt.DeletedDate == null).Any())
            {
                return false;
            }

            SetItem(id);

            if(_item == null)
            {
                return false;
            }

            _unitOfWork.ColumbariumItems.Remove(_item);

            _unitOfWork.Complete();

            return true;
        }

    }
}