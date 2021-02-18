using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorTransactionRepository : Repository<AncestorTransaction>, IAncestorTransactionRepository
    {
        public AncestorTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public AncestorTransaction GetActive(string AF)
        {
            return MemorialContext.AncestorTransactions
                .Where(at => at.AF == AF && at.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<AncestorTransaction> GetByApplicant(int id)
        {
            return MemorialContext.AncestorTransactions.Where(at => at.ApplicantId == id
                                            && at.DeleteDate == null).ToList();
        }

        public IEnumerable<AncestorTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.AncestorTransactions.Where(at => at.ApplicantId == applicantId
                                            && at.AncestorItemId == itemId
                                            && at.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}