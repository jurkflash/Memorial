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

        public CemeteryAreaDto GetCemeteryAreaDto(int id)
        {
            return _area.GetAreaDto(id);
        }

        public IEnumerable<CemeteryAreaDto> GetCemeteryAreaDtos()
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

        public CemeteryItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<CemeteryItemDto> GetItemDtosByPlot(int plotId)
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
                || plotInDB.CemeteryAreaId != plotDto.CemeteryAreaId)
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

        public bool CreateItem(CemeteryItemDto cemeteryItemDto)
        {
            if (_item.Create(cemeteryItemDto))
            {
                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdateItem(CemeteryItemDto cemeteryItemDto)
        {
            var cemeteryItemInDB = _item.GetItem(cemeteryItemDto.Id);

            if ((cemeteryItemInDB.isOrder != cemeteryItemDto.isOrder)
                && _unitOfWork.CemeteryTransactions.Find(pt => pt.CemeteryItemId == cemeteryItemDto.Id && pt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cemeteryItemDto, cemeteryItemInDB);

            if (_item.Update(cemeteryItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(pt => pt.CemeteryItemId == id && pt.DeleteDate == null).Any())
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


        public bool CreateArea(CemeteryAreaDto cemeteryAreaDto)
        {
            if (_area.Create(cemeteryAreaDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateArea(CemeteryAreaDto cemeteryAreaDto)
        {
            var cemeteryAreaInDB = _area.GetArea(cemeteryAreaDto.Id);

            if (cemeteryAreaInDB.SiteId != cemeteryAreaDto.SiteId
                && _unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.CemeteryAreaId == cemeteryAreaInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cemeteryAreaDto, cemeteryAreaInDB);

            if (_area.Update(cemeteryAreaInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteArea(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.CemeteryAreaId == id && ct.DeleteDate == null).Any())
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