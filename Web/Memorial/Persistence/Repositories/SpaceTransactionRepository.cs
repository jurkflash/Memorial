using System;
using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class SpaceTransactionRepository : Repository<SpaceTransaction>, ISpaceTransactionRepository
    {
        public SpaceTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public SpaceTransaction GetActive(string AF)
        {
            return MemorialContext.SpaceTransactions
                .Include(st => st.SpaceItem)
                .Include(st => st.SpaceItem.Space)
                .Include(st => st.SpaceItem.Space.Site)
                .Where(st => st.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<SpaceTransaction> GetByApplicant(int id)
        {
            return MemorialContext.SpaceTransactions.Where(st => st.ApplicantId == id).ToList();
        }

        public IEnumerable<SpaceTransaction> GetByItem(int itemId, string filter)
        {
            var transactions = MemorialContext.SpaceTransactions
                .Where(st => st.SpaceItemId == itemId)
                .Include(st => st.Applicant).ToList();

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t => t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || (t.Applicant.Name2 != null && t.Applicant.Name2.Contains(filter))).ToList();
            }
        }

        public IEnumerable<SpaceTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.SpaceTransactions.Where(st => st.ApplicantId == applicantId
                                            && st.SpaceItemId == itemId).ToList();
        }

        public IEnumerable<SpaceTransaction> GetByItemAndDeceased(int itemId, int deceasedId)
        {
            return MemorialContext.SpaceTransactions.Where(st => st.DeceasedId == deceasedId
                                            && st.SpaceItemId == itemId).ToList();
        }


        public bool GetAvailability(DateTime from, DateTime to, int spaceItemId)
        {
            var spaceItem = MemorialContext.SpaceItems.Where(si => si.Id == spaceItemId).SingleOrDefault();

            if (spaceItem == null)
                return false;

            return !MemorialContext.SpaceTransactions
                .Include(st => st.SpaceItem)
                .Where(st => st.SpaceItem.SpaceId == spaceItem.SpaceId
                && st.SpaceItem.AllowDoubleBook == false
                && (
                (st.FromDate <= from && from <= st.ToDate)
                || (st.FromDate <= to && to <= st.ToDate)
                || (from <= st.FromDate && st.FromDate <= to)
                || (from <= st.ToDate && st.ToDate <= to))).Any();
        }

        public bool GetAvailability(DateTime from, DateTime to, string AF)
        {
            var transaction = MemorialContext.SpaceTransactions.Where(st => st.AF == AF).FirstOrDefault();

            if (transaction == null)
                return false;

            return !MemorialContext.SpaceTransactions
                .Include(st => st.SpaceItem)
                .Where(st => st.SpaceItemId == transaction.SpaceItemId
                && st.SpaceItem.AllowDoubleBook == false
                && (
                (st.FromDate <= from && from <= st.ToDate)
                || (st.FromDate <= to && to <= st.ToDate)
                || (from <= st.FromDate && st.FromDate <= to)
                || (from <= st.ToDate && st.ToDate <= to))
                && st.AF != AF).Any();
        }

        public IEnumerable<SpaceTransaction> GetBooked(DateTime from, DateTime to, int siteId)
        {
            return MemorialContext.SpaceTransactions
                .Include(st => st.SpaceItem)
                .Include(st => st.SpaceItem.Space)
                .Where(st => st.SpaceItem.AllowDoubleBook == false
                && (
                (st.FromDate <= from && from <= st.ToDate)
                || (st.FromDate <= to && to <= st.ToDate)
                || (from <= st.FromDate && st.FromDate <= to)
                || (from <= st.ToDate && st.ToDate <= to))
                && st.SpaceItem.Space.SiteId == siteId);
        }

        public IEnumerable<SpaceTransaction> GetRecent(int? number, int siteId, int? applicantId)
        {
            var result = MemorialContext.SpaceTransactions
                .Where(t => t.SpaceItem.Space.SiteId == siteId)
                .Include(t => t.Applicant)
                .Include(t => t.SpaceItem)
                .Include(t => t.SpaceItem.Space)
                .Include(t => t.SpaceItem.SubProductService)
                .Include(t => t.SpaceItem.SubProductService.Product);

            if (applicantId != null)
                result = result.Where(t => t.ApplicantId == applicantId);

            if (number != null)
                result = result.Take((int)number);

            return result.OrderByDescending(t => t.CreatedDate).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}