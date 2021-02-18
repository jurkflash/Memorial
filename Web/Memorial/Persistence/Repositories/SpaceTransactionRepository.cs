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
                .Where(st => st.AF == AF && st.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<SpaceTransaction> GetByApplicant(int id)
        {
            return MemorialContext.SpaceTransactions.Where(st => st.ApplicantId == id
                                            && st.DeleteDate == null).ToList();
        }

        public IEnumerable<SpaceTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.SpaceTransactions.Where(st => st.ApplicantId == applicantId
                                            && st.SpaceItemId == itemId
                                            && st.DeleteDate == null).ToList();
        }


        public bool GetAvailability(DateTime from, DateTime to, int spaceItemId)
        {
            var spaceItem = MemorialContext.SpaceItems.Where(si => si.Id == spaceItemId).SingleOrDefault();

            if (spaceItem == null)
                return false;

            return !MemorialContext.SpaceTransactions
                .Include(st => st.SpaceItem)
                .Where(st => st.DeleteDate == null
                && st.SpaceItem.SpaceId == spaceItem.SpaceId
                && st.SpaceItem.AllowDoubleBook == false
                && (
                (st.FromDate <= from && from <= st.ToDate)
                || (st.FromDate <= to && to <= st.ToDate)
                || (from <= st.FromDate && st.FromDate <= to)
                || (from <= st.ToDate && st.ToDate <= to))).Any();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}