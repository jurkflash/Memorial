using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class PlotRepository : Repository<Plot>, IPlotRepository
    {
        public PlotRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Plot> GetByTypeAndArea(int plotTypeId, int plotAreaId)
        {
            return MemorialContext.Plots
                .Where(p => p.PlotTypeId == plotTypeId && 
                        p.PlotAreaId == plotAreaId).ToList();
        }

        public IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeId, int plotAreaId)
        {
            return MemorialContext.Plots
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.PlotAreaId == plotAreaId &&
                        p.Deceaseds.Count > 0 ).ToList();
        }

        public IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeId, int plotAreaId)
        {
            return MemorialContext.Plots
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.PlotAreaId == plotAreaId &&
                        p.Deceaseds.Count < p.PlotType.NumberOfPlacement &&
                        p.PlotType.NumberOfPlacement > 1).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}