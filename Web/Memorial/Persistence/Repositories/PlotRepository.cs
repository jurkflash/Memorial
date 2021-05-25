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
                .Include(p => p.CemeteryArea)
                .Include(p => p.CemeteryArea.Site)
                .Where(p => p.Id == id && p.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Plot> GetByArea(int cemeteryAreaId, string filter = null)
        {
            var plots = MemorialContext.Plots
                    .Include(p => p.PlotType)
                    .Include(p => p.CemeteryArea.Site)
                    .Where(p => p.CemeteryAreaId == cemeteryAreaId);

            if (string.IsNullOrEmpty(filter))
            {
                return plots.ToList();
            }
            else
            {
                return plots.Where(p => p.Name.Contains(filter)).ToList();
            }
        }

        public IEnumerable<PlotType> GetTypesByArea(int cemeteryAreaId)
        {
            var plotTypes = MemorialContext.Plots
                    .Include(p => p.PlotType)
                    .Include(p => p.CemeteryArea.Site)
                    .Where(p => p.CemeteryAreaId == cemeteryAreaId)
                    .Select(p => p.PlotType);

            return plotTypes.ToList();
        }

        public IEnumerable<Plot> GetByTypeAndArea(int cemeteryAreaId, int plotTypeId, string filter = null)
        {
            var plots = MemorialContext.Plots
                .Include(p => p.PlotType)
                .Include(p => p.CemeteryArea.Site)
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.CemeteryAreaId == cemeteryAreaId);

            if (string.IsNullOrEmpty(filter))
            {
                return plots.ToList();
            }
            else
            {
                return plots.Where(p => p.Name.Contains(filter)).ToList();
            }
        }

        public IEnumerable<Plot> GetAvailableByTypeAndArea(int plotTypeId, int cemeteryAreaId)
        {
            return MemorialContext.Plots
                .Include(p => p.PlotType)
                .Include(p => p.CemeteryArea.Site)
                .Where(p => p.PlotTypeId == plotTypeId && 
                        p.CemeteryAreaId == cemeteryAreaId && 
                        p.ApplicantId == null).ToList();
        }

        public IEnumerable<Plot> GetBuriedByWithTypeAndArea(int plotTypeId, int cemeteryAreaId)
        {
            return MemorialContext.Plots
                .Include(p => p.PlotType)
                .Include(p => p.CemeteryArea.Site)
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.CemeteryAreaId == cemeteryAreaId &&
                        p.Deceaseds.Count > 0 ).ToList();
        }

        public IEnumerable<Plot> GetSecondBurialByWithTypeAndArea(int plotTypeId, int cemeteryAreaId)
        {
            return MemorialContext.Plots
                .Include(p => p.PlotType)
                .Include(p => p.CemeteryArea.Site)
                .Where(p => p.PlotTypeId == plotTypeId &&
                        p.CemeteryAreaId == cemeteryAreaId &&
                        p.Deceaseds.Count < p.PlotType.NumberOfPlacement &&
                        p.PlotType.NumberOfPlacement > 1).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}