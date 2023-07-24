using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CemeteryTransactionRepository : Repository<CemeteryTransaction>, ICemeteryTransactionRepository
    {
        public CemeteryTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public CemeteryTransaction GetActive(string AF)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.Plot.CemeteryArea)
                .Include(pt => pt.Plot.CemeteryArea.Site)
                .Include(pt => pt.CemeteryItem)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Where(pt => pt.AF == AF)
                .SingleOrDefault();
        }

        public CemeteryTransaction GetExclusive(string AF)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.Plot.CemeteryArea)
                .Include(pt => pt.Plot.CemeteryArea.Site)
                .Include(pt => pt.CemeteryItem)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Where(pt => pt.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<CemeteryTransaction> GetByApplicant(int id)
        {
            return MemorialContext.CemeteryTransactions.Where(pt => pt.ApplicantId == id).ToList();
        }

        public IEnumerable<CemeteryTransaction> GetByPlotIdAndItem(int plotId, int itemId, string filter)
        {
            var transactions = MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Include(pt => pt.Deceased1)
                .Where(pt => pt.CemeteryItemId == itemId
                                            && pt.PlotId == plotId);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t=>t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || (t.Applicant.Name2 != null && t.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public IEnumerable<CemeteryTransaction> GetByPlotIdAndItemAndApplicant(int plotId, int itemId, int applicantId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.ApplicantId == applicantId
                                            && pt.CemeteryItemId == itemId
                                            && pt.PlotId == plotId).ToList();
        }

        public CemeteryTransaction GetByPlotIdAndDeceased(int plotId, int deceased1Id)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.Deceased1Id == deceased1Id
                                            && pt.PlotId == plotId).SingleOrDefault();
        }

        public CemeteryTransaction GetLastCemeteryTransactionByPlotId(int plotId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.PlotId == plotId).OrderByDescending(pt => pt.CreatedUtcTime).FirstOrDefault();
        }

        public CemeteryTransaction GetLastCemeteryTransactionByShiftedPlotId(int plotId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Include(pt => pt.CemeteryItem)
                .OrderByDescending(pt => pt.CreatedUtcTime).FirstOrDefault();
        }

        public IEnumerable<CemeteryTransaction> GetByPlotId(int plotId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.FuneralCompany)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.PlotId == plotId)
                .ToList();
        }

        public IEnumerable<CemeteryTransaction> GetRecent(int? number, int siteId, int? applicantId)
        {
            var result = MemorialContext.CemeteryTransactions
                .Where(t => t.CemeteryItem.Plot.CemeteryArea.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.CemeteryItem)
                .Include(t => t.Plot)
                .Include(t => t.Plot.CemeteryArea)
                .Include(t => t.CemeteryItem.SubProductService)
                .Include(t => t.CemeteryItem.SubProductService.Product);

            if (applicantId != null)
                result = result.Where(t => t.ApplicantId == applicantId);

            if (number != null)
                result = result.Take((int)number);

            return result.OrderByDescending(t => t.CreatedUtcTime).ToList();
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}