using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class CremationTransactionRepository : Repository<CremationTransaction>, ICremationTransactionRepository
    {
        public CremationTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public CremationTransaction GetActive(string AF)
        {
            return MemorialContext.CremationTransactions
                .Where(ct => ct.AF == AF && ct.DeleteDate == null)                                            
                .Include(ct => ct.FuneralCompany).SingleOrDefault();
        }

        public IEnumerable<CremationTransaction> GetByApplicant(int id)
        {
            return MemorialContext.CremationTransactions.Where(ct => ct.ApplicantId == id
                                            && ct.DeleteDate == null).ToList();
        }

        public IEnumerable<CremationTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.CremationTransactions.Where(ct => ct.ApplicantId == applicantId
                                            && ct.CremationItemId == itemId
                                            && ct.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}