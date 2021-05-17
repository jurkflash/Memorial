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

        public Plot GetActive(int id)
        {
            return MemorialContext.Plots
                .Include(p => p.Applicant)
                .Include(p => p.PlotType)
                .Include(p => p.PlotArea)
                .Where(p => p.Id == id && p.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Plot> GetByArea(int plotAreaId, string filter = null)
        {
            var plots = MemorialContext.Plots
                    .Include(p => p.PlotType)
                    .Where(p => p.PlotAreaId == plotAreaId);

            if (string.IsNullOrEmpty(filter))
            {
                return plots.ToList();
            }
            else
            {
                return plots.Where(p => p.Name.Contains(filter)).ToList();
            }
        }

        public IEnumerable<PlotType> GetTypesByArea(int plotAreaId)
        {
            var plotTypes = MemorialContext.Plots
                    .Include(p => p.PlotType)
                    .Where(p => p.PlotAreaId == plotAreaId)
                    .Select(p => p.PlotType);

            return plotTypes.ToList();
        }

        public IEnumerable<Plot> GetByTypeAndArea(int plotAreaId, int plotTypeId, string filter = null)
        {
            var plots = MemorialContext.Plots
                .Include(p => p.PlotType)
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.PlotAreaId == plotAreaId);

            if (string.IsNullOrEmpty(filter))
            {
                return plots.ToList();
            }
            else
            {
                return plots.Where(p => p.Name.Contains(filter)).ToList();
            }
        }

        public IEnumerable<Plot> GetAvailableByTypeAndArea(int plotTypeId, int plotAreaId)
        {
            return MemorialContext.Plots
                .Include(p => p.PlotType)
                .Where(p => p.PlotTypeId == plotTypeId && 
                        p.PlotAreaId == plotAreaId && 
                        p.ApplicantId == null).ToList();
        }

        public IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeId, int plotAreaId)
        {
            return MemorialContext.Plots
                .Include(p => p.PlotType)
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.PlotAreaId == plotAreaId &&
                        p.Deceaseds.Count > 0 ).ToList();
        }

        public IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeId, int plotAreaId)
        {
            return MemorialContext.Plots
                .Include(p => p.PlotType)
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