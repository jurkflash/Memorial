using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class MiscellaneousTransactionRepository : Repository<MiscellaneousTransaction>, IMiscellaneousTransactionRepository
    {
        public MiscellaneousTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public MiscellaneousTransaction GetActive(string AF)
        {
            return MemorialContext.MiscellaneousTransactions
                .Include(mt => mt.Applicant)
                .Include(mt => mt.MiscellaneousItem)
                .Include(mt => mt.MiscellaneousItem.Miscellaneous)
                .Include(mt => mt.MiscellaneousItem.Miscellaneous.Site)
                .Include(mt => mt.CemeteryLandscapeCompany)
                .Where(mt => mt.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<MiscellaneousTransaction> GetByItem(int itemId, string filter)
        {
            var miscellaneousTransactions = MemorialContext.MiscellaneousTransactions
                .Include(mt => mt.Applicant)
                .Include(mt => mt.MiscellaneousItem)
                .Include(mt => mt.MiscellaneousItem.Miscellaneous)
                .Include(mt => mt.MiscellaneousItem.Miscellaneous.Site)
                .Include(mt => mt.CemeteryLandscapeCompany)
                .Where(mt => mt.MiscellaneousItemId == itemId);

            if(string.IsNullOrEmpty(filter))
            {
                return miscellaneousTransactions.ToList();
            }
            else
            {
                return miscellaneousTransactions.Where(mt=>mt.AF.Contains(filter) || mt.Applicant.Name.Contains(filter) || (mt.Applicant.Name2 != null && mt.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public bool GetExistsByApplicant(int id)
        {
            return MemorialContext.MiscellaneousTransactions.Where(mt => mt.ApplicantId == id).Any();
        }

        public IEnumerable<MiscellaneousTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.MiscellaneousTransactions
                .Include(mt => mt.Applicant)
                .Include(mt => mt.MiscellaneousItem)
                .Include(mt => mt.MiscellaneousItem.Miscellaneous)
                .Include(mt => mt.CemeteryLandscapeCompany)
                .Where(mt => mt.ApplicantId == applicantId
                                            && mt.MiscellaneousItemId == itemId).ToList();
        }

        public IEnumerable<MiscellaneousTransaction> GetRecent(int? number, int siteId, int? applicantId)
        {
            var result = MemorialContext.MiscellaneousTransactions
                .Where(t => t.MiscellaneousItem.Miscellaneous.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.MiscellaneousItem)
                .Include(t => t.MiscellaneousItem.Miscellaneous)
                .Include(t => t.MiscellaneousItem.SubProductService)
                .Include(t => t.MiscellaneousItem.SubProductService.Product);

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