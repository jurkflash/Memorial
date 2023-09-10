using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Plot : IPlot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;

        public Plot(IUnitOfWork unitOfWork, IItem item)
        {
            _unitOfWork = unitOfWork;
            _item = item;
        }

        public Core.Domain.Plot GetById(int id)
        {
            return _unitOfWork.Plots.GetActive(id);
        }

        public IEnumerable<Core.Domain.PlotType> GetPlotTypesByAreaId(int id)
        {
            return _unitOfWork.Plots.GetPlotTypesByArea(id);
        }

        public IEnumerable<Core.Domain.Plot> GetByAreaId(int id, string filter)
        {
            return _unitOfWork.Plots.GetByArea(id, filter);
        }

        public IEnumerable<Core.Domain.Plot> GetByAreaIdAndTypeId(int areaId, int typeId, string filter)
        {
            return _unitOfWork.Plots.GetByTypeAndArea(areaId, typeId, filter);
        }

        public IEnumerable<Core.Domain.Plot> GetAvailableByTypeAndArea(int typeId, int areaId)
        {
            return _unitOfWork.Plots.GetAvailableByTypeAndArea(typeId, areaId);
        }

        public int Add(Core.Domain.Plot plot)
        {
            _unitOfWork.Plots.Add(plot);

            _unitOfWork.Complete();

            _item.AutoAddItem(plot.PlotTypeId, plot.Id);

            _unitOfWork.Complete();

            return plot.Id;
        }

        public bool Change(int id, Core.Domain.Plot plot)
        {
            var plotInDB = GetById(id);

            if ((plotInDB.PlotTypeId != plot.PlotTypeId
                || plotInDB.CemeteryAreaId != plot.CemeteryAreaId)
                && _unitOfWork.CemeteryTransactions.Find(ct => ct.PlotId == plot.Id).Any())
            {
                return false;
            }

            plotInDB.Name = plot.Name;
            plotInDB.Description = plot.Description;
            plotInDB.Size = plot.Size;
            plotInDB.Price = plot.Price;
            plotInDB.Maintenance = plot.Maintenance;
            plotInDB.Wall = plot.Wall;
            plotInDB.Dig = plot.Dig;
            plotInDB.Brick = plot.Brick;
            plotInDB.Remark = plot.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(ct => ct.PlotId == id).Any())
            {
                return false;
            }

            var plotInDb = GetById(id);

            if(plotInDb == null)
            {
                return false;
            }

            _unitOfWork.Plots.Remove(plotInDb);

            _unitOfWork.Complete();

            return true;
        }

    }
}