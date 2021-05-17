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
                .Include(ct => ct.FuneralCompany)
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.CremationItem.Cremation)
                .Include(ct => ct.CremationItem.Cremation.Site)
                .SingleOrDefault();
        }

        public IEnumerable<CremationTransaction> GetByItem(int itemId, string filter)
        {
            var transactions = MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Include(ct => ct.FuneralCompany)
                .Where(ct => ct.CremationItemId == itemId && ct.DeleteDate == null);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(ct => ct.AF.Contains(filter) || ct.Applicant.Name.Contains(filter) || ct.Applicant.Name2.Contains(filter)).ToList();
            }
        }

        public IEnumerable<CremationTransaction> GetByItemAndDeceased(int itemId, int deceasedId)
        {
            return MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Include(ct => ct.FuneralCompany)
                .Where(ct => ct.CremationItemId == itemId 
                    && ct.DeceasedId == deceasedId
                    && ct.DeleteDate == null).ToList();
        }

        public IEnumerable<CremationTransaction> GetByApplicant(int id)
        {
            return MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Where(ct => ct.ApplicantId == id
                                            && ct.DeleteDate == null).ToList();
        }

        public IEnumerable<CremationTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return MemorialContext.CremationTransactions
                .Include(ct => ct.CremationItem)
                .Include(ct => ct.Applicant)
                .Include(ct => ct.Deceased)
                .Where(ct => ct.ApplicantId == applicantId
                                            && ct.CremationItemId == itemId
                                            && ct.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}