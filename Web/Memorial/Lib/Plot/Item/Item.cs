using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Plot
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.PlotItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.PlotItems.GetActive(id);
        }

        public Core.Domain.PlotItem GetItem()
        {
            return _item;
        }

        public PlotItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.PlotItem, PlotItemDto>(GetItem());
        }

        public Core.Domain.PlotItem GetItem(int id)
        {
            return _unitOfWork.PlotItems.GetActive(id);
        }

        public PlotItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.PlotItem, PlotItemDto>(GetItem(id));
        }

        public int GetId()
        {
            return _item.Id;
        }

        public string GetName()
        {
            return _item.Name;
        }

        public string GetDescription()
        {
            return _item.Description;
        }

        public float GetPrice()
        {
            return _item.Price;
        }

        public string GetSystemCode()
        {
            return _item.SystemCode;
        }

        public bool IsOrder()
        {
            return _item.isOrder;
        }

        public IEnumerable<Core.Domain.PlotItem> GetItemByPlot(int plotId)
        {
            return _unitOfWork.PlotItems.GetByPlot(plotId);
        }

        public IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotItem>, IEnumerable<PlotItemDto>>(GetItemByPlot(plotId));
        }

    }
}