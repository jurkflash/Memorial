using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class PlotTrackingRepository : Repository<PlotTracking>, IPlotTrackingRepository
    {
        public PlotTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public PlotTracking GetLatestFirstTransactionByPlotId(int plotId)
        {
            return MemorialContext.PlotTrackings.Where(qt => qt.PlotId == plotId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<PlotTracking> GetTrackingByPlotId(int plotId)
        {
            return MemorialContext.PlotTrackings.Where(qt => qt.PlotId == plotId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public PlotTracking GetTrackingByTransactionAF(string cemeteryTransactionAF)
        {
            return MemorialContext.PlotTrackings.Where(qt => qt.CemeteryTransactionAF == cemeteryTransactionAF).OrderByDescending(qt => qt.ActionDate).SingleOrDefault();
        }

        public PlotTracking GetTrackingByPlotIdAndTransactionAF(int plotId, string cemeteryTransactionAF)
        {
            return MemorialContext.PlotTrackings.Where(qt => qt.CemeteryTransactionAF == cemeteryTransactionAF && qt.PlotId == plotId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}