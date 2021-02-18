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
                .Include(mt => mt.MiscellaneousItem)
                .Where(mt => mt.AF == AF && mt.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<MiscellaneousTransaction> GetByApplicant(int id)
        {
            return MemorialContext.MiscellaneousTransactions.Where(mt => mt.ApplicantId == id
                                            && mt.DeleteDate == null).ToList();
        }

        public IEnumerable<MiscellaneousTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.MiscellaneousTransactions.Where(mt => mt.ApplicantId == applicantId
                                            && mt.MiscellaneousItemId == itemId
                                            && mt.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}