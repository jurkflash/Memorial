using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IPlot _plot;

        public Config(
            IUnitOfWork unitOfWork,
            IArea area,
            IItem item,
            IPlot plot
            )
        {
            _unitOfWork = unitOfWork;
            _area = area;
            _item = item;
            _plot = plot;
        }

        public PlotAreaDto GetPlotAreaDto(int id)
        {
            return _area.GetAreaDto(id);
        }

        public IEnumerable<PlotAreaDto> GetPlotAreaDtos()
        {
            return _area.GetAreaDtos();
        }

        public PlotDto GetPlotDto(int id)
        {
            return _plot.GetPlotDto(id);
        }

        public IEnumerable<PlotDto> GetPlotDtosByArea(int areaId, string filter)
        {
            return _plot.GetPlotDtosByAreaId(areaId, filter);
        }

        public PlotItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId)
        {
            return _item.GetItemDtosByPlot(plotId);
        }

        public IEnumerable<PlotNumber> GetNumbers()
        {
            return _unitOfWork.PlotNumbers.GetAll();
        }


        public bool CreatePlot(PlotDto plotDto)
        {
            if (_plot.Create(plotDto))
            {
                _item.AutoCreateItem(plotDto.PlotTypeDtoId, plotDto.Id);

                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdatePlot(PlotDto plotDto)
        {
            var plotInDB = _plot.GetPlot(plotDto.Id);

            if ((plotInDB.PlotTypeId != plotDto.PlotTypeDtoId
                || plotInDB.PlotAreaId != plotDto.PlotAreaId)
                && _unitOfWork.CemeteryTransactions.Find(ct => ct.PlotId == plotDto.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(plotDto, plotInDB);

            if (_plot.Update(plotInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeletePlot(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(ct => ct.PlotId == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_plot.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool CreateItem(PlotItemDto plotItemDto)
        {
            if (_item.Create(plotItemDto))
            {
                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdateItem(PlotItemDto plotItemDto)
        {
            var plotItemInDB = _item.GetItem(plotItemDto.Id);

            if ((plotItemInDB.isOrder != plotItemDto.isOrder)
                && _unitOfWork.CemeteryTransactions.Find(pt => pt.PlotItemId == plotItemDto.Id && pt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(plotItemDto, plotItemInDB);

            if (_item.Update(plotItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(pt => pt.PlotItemId == id && pt.DeleteDate == null).Any())
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


        public bool CreateArea(PlotAreaDto plotAreaDto)
        {
            if (_area.Create(plotAreaDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateArea(PlotAreaDto plotAreaDto)
        {
            var plotAreaInDB = _area.GetArea(plotAreaDto.Id);

            if (plotAreaInDB.SiteId != plotAreaDto.SiteId
                && _unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.PlotAreaId == plotAreaInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(plotAreaDto, plotAreaInDB);

            if (_area.Update(plotAreaInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteArea(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.PlotAreaId == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            if (_area.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }




    }
}