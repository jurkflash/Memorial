using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.Cemetery
{
    public class PlotType : IPlotType
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlotType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Domain.PlotType> GetAll()
        {
            return _unitOfWork.PlotTypes.GetAll();
        }
    }
}