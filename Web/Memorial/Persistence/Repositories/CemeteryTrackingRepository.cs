using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CemeteryTrackingRepository : Repository<CemeteryTracking>, ICemeteryTrackingRepository
    {
        public CemeteryTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public CemeteryTracking GetLatestFirstTransactionByPlotId(int plotId)
        {
            return MemorialContext.CemeteryTrackings.Where(qt => qt.PlotId == plotId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<CemeteryTracking> GetTrackingByPlotId(int plotId)
        {
            return MemorialContext.CemeteryTrackings.Where(qt => qt.PlotId == plotId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public CemeteryTracking GetTrackingByTransactionAF(string cemeteryTransactionAF)
        {
            return MemorialContext.CemeteryTrackings.Where(qt => qt.CemeteryTransactionAF == cemeteryTransactionAF).OrderByDescending(qt => qt.ActionDate).SingleOrDefault();
        }

        public CemeteryTracking GetTrackingByPlotIdAndTransactionAF(int plotId, string cemeteryTransactionAF)
        {
            return MemorialContext.CemeteryTrackings.Where(qt => qt.CemeteryTransactionAF == cemeteryTransactionAF && qt.PlotId == plotId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}