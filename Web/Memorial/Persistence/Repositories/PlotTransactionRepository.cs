using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class PlotTransactionRepository : Repository<PlotTransaction>, IPlotTransactionRepository
    {
        public PlotTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public PlotTransaction GetActive(string AF)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Include(pt => pt.FengShuiMaster)
                .Where(pt => pt.AF == AF && pt.DeleteDate == null)
                .SingleOrDefault();
        }

        public PlotTransaction GetExclusive(string AF)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Include(pt => pt.FengShuiMaster)
                .Where(pt => pt.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<PlotTransaction> GetByApplicant(int id)
        {
            return MemorialContext.PlotTransactions.Where(pt => pt.ApplicantId == id
                                            && pt.DeleteDate == null).ToList();
        }

        public IEnumerable<PlotTransaction> GetByPlotIdAndItem(int plotId, int itemId)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.Deceased1)
                .Where(pt => pt.PlotItemId == itemId
                                            && pt.PlotId == plotId
                                            && pt.DeleteDate == null).ToList();
        }

        public IEnumerable<PlotTransaction> GetByPlotIdAndItemAndApplicant(int plotId, int itemId, int applicantId)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Where(pt => pt.ApplicantId == applicantId
                                            && pt.PlotItemId == itemId
                                            && pt.PlotId == plotId
                                            && pt.DeleteDate == null).ToList();
        }

        public PlotTransaction GetByPlotIdAndDeceased(int plotId, int deceased1Id)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Where(pt => pt.Deceased1Id == deceased1Id
                                            && pt.PlotId == plotId
                                            && pt.DeleteDate == null).SingleOrDefault();
        }

        public PlotTransaction GetLastPlotTransactionByPlotId(int plotId)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Where(pt => pt.PlotId == plotId
                                            && pt.DeleteDate == null).OrderByDescending(pt => pt.CreateDate).FirstOrDefault();
        }

        public PlotTransaction GetLastPlotTransactionByShiftedPlotId(int plotId)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Where(pt => pt.DeleteDate == null).OrderByDescending(pt => pt.CreateDate).FirstOrDefault();
        }

        public IEnumerable<PlotTransaction> GetByPlotId(int plotId)
        {
            return MemorialContext.PlotTransactions
                .Include(pt => pt.Plot)
                .Include(pt => pt.Plot)
                .Include(pt => pt.PlotItem)
                .Where(pt => pt.PlotId == plotId && pt.DeleteDate == null)
                .ToList();
        }
        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}