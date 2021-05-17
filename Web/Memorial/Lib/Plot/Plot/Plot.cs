using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Plot
{
    public class Plot : IPlot
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Plot _plot;

        public Plot(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetPlot(int id)
        {
            _plot = _unitOfWork.Plots.GetActive(id);
        }

        public Core.Domain.Plot GetPlot()
        {
            return _plot;
        }

        public PlotDto GetPlotDto()
        {
            return Mapper.Map<Core.Domain.Plot, PlotDto>(GetPlot());
        }

        public Core.Domain.Plot GetPlot(int id)
        {
            return _unitOfWork.Plots.GetActive(id);
        }

        public PlotDto GetPlotDto(int id)
        {
            return Mapper.Map<Core.Domain.Plot, PlotDto>(GetPlot(id));
        }

        public IEnumerable<Core.Domain.PlotType> GetPlotTypesByAreaId(int id)
        {
            return _unitOfWork.Plots.GetTypesByArea(id);
        }

        public IEnumerable<PlotTypeDto> GetPlotTypeDtosByAreaId(int id)
        {
            return Mapper.Map< IEnumerable<Core.Domain.PlotType>, IEnumerable<PlotTypeDto>>(GetPlotTypesByAreaId(id));
        }

        public IEnumerable<Core.Domain.Plot> GetPlotsByAreaId(int id, string filter)
        {
            return _unitOfWork.Plots.GetByArea(id, filter);
        }

        public IEnumerable<PlotDto> GetPlotDtosByAreaId(int id, string filter)
        {
            return Mapper.Map< IEnumerable<Core.Domain.Plot>, IEnumerable<PlotDto>>(GetPlotsByAreaId(id, filter));
        }

        public IEnumerable<Core.Domain.Plot> GetPlotsByAreaIdAndTypeId(int areaId, int typeId, string filter)
        {
            return _unitOfWork.Plots.GetByTypeAndArea(areaId, typeId, filter);
        }

        public IEnumerable<PlotDto> GetPlotDtosByAreaIdAndTypeId(int areaId, int typeId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Plot>, IEnumerable<PlotDto>>(GetPlotsByAreaIdAndTypeId(areaId, typeId, filter));
        }

        public IEnumerable<Core.Domain.Plot> GetAvailablePlotsByTypeIdAndAreaId(int typeId, int areaId)
        {
            return _unitOfWork.Plots.GetAvailableByTypeAndArea(typeId, areaId);
        }

        public IEnumerable<PlotDto> GetAvailablePlotDtosByAreaId(int typeId, int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Plot>, IEnumerable<PlotDto>>(GetAvailablePlotsByTypeIdAndAreaId(typeId, areaId));
        }

        public string GetName()
        {
            return _plot.Name;
        }

        public string GetDescription()
        {
            return _plot.Description;
        }

        public string GetSize()
        {
            return _plot.Size;
        }

        public float GetPrice()
        {
            return _plot.Price;
        }

        public float GetMaintenance()
        {
            return _plot.Maintenance;
        }

        public float GetWall()
        {
            return _plot.Wall;
        }

        public float GetDig()
        {
            return _plot.Dig;
        }

        public float GetBrick()
        {
            return _plot.Brick;
        }

        public string GetRemark()
        {
            return _plot.Remark;
        }

        public bool HasDeceased()
        {
            return _plot.hasDeceased;
        }

        public void SetHasDeceased(bool flag)
        {
            _plot.hasDeceased = flag;
        }

        public bool HasCleared()
        {
            return _plot.hasCleared;
        }

        public void SetHasCleared(bool flag)
        {
            _plot.hasCleared = flag;
        }

        public bool HasApplicant()
        {
            return _plot.ApplicantId == null ? false : true;
        }

        public int? GetApplicantId()
        {
            return _plot.ApplicantId;
        }

        public void SetApplicant(int applicantId)
        {
            _plot.ApplicantId = applicantId;
        }

        public void RemoveApplicant()
        {
            _plot.Applicant = null;
            _plot.ApplicantId = null;
        }

        public int GetAreaId()
        {
            return _plot.PlotAreaId;
        }

        public int GetNumberOfPlacement()
        {
            return _plot.PlotType.NumberOfPlacement;
        }

        public bool IsFengShuiPlot()
        {
            return _plot.PlotType.isFengShuiPlot;
        }

        public bool Create(PlotDto plotDto)
        {
            _plot = new Core.Domain.Plot();
            Mapper.Map(plotDto, _plot);

            _plot.CreateDate = DateTime.Now;

            _unitOfWork.Plots.Add(_plot);

            return true;
        }

        public bool Update(Core.Domain.Plot plot)
        {
            plot.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetPlot(id);

            _plot.DeleteDate = DateTime.Now;

            return true;
        }

    }
}