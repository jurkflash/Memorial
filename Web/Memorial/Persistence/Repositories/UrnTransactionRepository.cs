using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class UrnTransactionRepository : Repository<UrnTransaction>, IUrnTransactionRepository
    {
        public UrnTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public UrnTransaction GetActive(string AF)
        {
            return MemorialContext.UrnTransactions
                .Include(ut => ut.UrnItem)
                .Include(ut => ut.Applicant)
                .Where(ut => ut.AF == AF && ut.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<UrnTransaction> GetByItem(int itemId)
        {
            return MemorialContext.UrnTransactions
                .Include(ut => ut.UrnItem)
                .Include(ut => ut.Applicant)
                .Where(ut => ut.UrnItemId == itemId && ut.DeleteDate == null).ToList();
        }

        public IEnumerable<UrnTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.UrnTransactions
                .Include(ut => ut.UrnItem)
                .Include(ut => ut.Applicant)
                .Where(ut => ut.ApplicantId == applicantId
                                            && ut.UrnItemId == itemId
                                            && ut.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}