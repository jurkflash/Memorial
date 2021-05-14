﻿using Memorial.Core.Domain;
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
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.ShiftedAncestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AF == AF && at.DeleteDate == null)
                .SingleOrDefault();
        }

        public AncestorTransaction GetExclusive(string AF)
        {
            return MemorialContext.AncestorTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.ShiftedAncestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<AncestorTransaction> GetByApplicant(int id)
        {
            return MemorialContext.AncestorTransactions.Where(at => at.ApplicantId == id
                                            && at.DeleteDate == null).ToList();
        }

        public IEnumerable<AncestorTransaction> GetByAncestorIdAndItem(int ancestorId, int itemId)
        {
            return MemorialContext.AncestorTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.ShiftedAncestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AncestorItemId == itemId
                                            && (at.AncestorId == ancestorId || at.ShiftedAncestorId == ancestorId)
                                            && at.DeleteDate == null).ToList();
        }

        public IEnumerable<AncestorTransaction> GetByAncestorIdAndItemAndApplicant(int ancestorId, int itemId, int applicantId)
        {
            return MemorialContext.AncestorTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.ApplicantId == applicantId
                                            && at.AncestorItemId == itemId
                                            && at.AncestorId == ancestorId
                                            && at.DeleteDate == null).ToList();
        }

        public AncestorTransaction GetByShiftedAncestorTransactionAF(string AF)
        {
            return MemorialContext.AncestorTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.ShiftedAncestorTransactionAF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<AncestorTransaction> GetByAncestorId(int ancestorId)
        {
            return MemorialContext.AncestorTransactions
                .Include(at => at.Applicant)
                .Include(at => at.Ancestor)
                .Include(at => at.AncestorItem)
                .Where(at => at.AncestorId == ancestorId && at.DeleteDate == null)
                .ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}