using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class PlotType : IPlotType
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.PlotType _plotType;

        public PlotType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetPlotType(int id)
        {
            _plotType = _unitOfWork.PlotTypes.Get(id);
        }

        public int GetId()
        {
            return _plotType.Id;
        }

        public string GetName()
        {
            return _plotType.Name;
        }

        public byte GetNumberOfPlacement()
        {
            return _plotType.NumberOfPlacement;
        }

        public bool isFengShuiPlot()
        {
            return _plotType.isFengShuiPlot;
        }

        public Core.Domain.PlotType GetPlotType()
        {
            return _plotType;
        }

        public PlotTypeDto GetPlotTypeDto()
        {
            return Mapper.Map<Core.Domain.PlotType, PlotTypeDto>(_plotType);
        }

        public Core.Domain.PlotType GetPlotType(int plotTypeId)
        {
            return _unitOfWork.PlotTypes.Get(plotTypeId);
        }

        public PlotTypeDto GetPlotTypeDto(int plotTypeId)
        {
            return Mapper.Map<Core.Domain.PlotType, PlotTypeDto>(GetPlotType(plotTypeId));
        }

        public IEnumerable<Core.Domain.PlotType> GetPlotTypes()
        {
            return _unitOfWork.PlotTypes.GetAll();
        }

        public IEnumerable<PlotTypeDto> GetPlotTypeDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotType>, IEnumerable<PlotTypeDto>>(GetPlotTypes());
        }

    }
}