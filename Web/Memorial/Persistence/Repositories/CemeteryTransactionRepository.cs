﻿using Memorial.Core.Domain;
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
                .Include(pt => pt.CemeteryItem)
                .Include(pt => pt.FengShuiMaster)
                .Where(pt => pt.AF == AF && pt.DeleteDate == null)
                .SingleOrDefault();
        }

        public CemeteryTransaction GetExclusive(string AF)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Include(pt => pt.FengShuiMaster)
                .Where(pt => pt.AF == AF)
                .SingleOrDefault();
        }

        public IEnumerable<CemeteryTransaction> GetByApplicant(int id)
        {
            return MemorialContext.CemeteryTransactions.Where(pt => pt.ApplicantId == id
                                            && pt.DeleteDate == null).ToList();
        }

        public IEnumerable<CemeteryTransaction> GetByPlotIdAndItem(int plotId, int itemId, string filter)
        {
            var transactions = MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Include(pt => pt.FengShuiMaster)
                .Include(pt => pt.Deceased1)
                .Where(pt => pt.CemeteryItemId == itemId
                                            && pt.PlotId == plotId
                                            && pt.DeleteDate == null);

            if(string.IsNullOrEmpty(filter))
            {
                return transactions.ToList();
            }
            else
            {
                return transactions.Where(t=>t.AF.Contains(filter) || t.Applicant.Name.Contains(filter) || t.Applicant.Name2.Contains(filter)).ToList();
            }
        }

        public IEnumerable<CemeteryTransaction> GetByPlotIdAndItemAndApplicant(int plotId, int itemId, int applicantId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.ApplicantId == applicantId
                                            && pt.CemeteryItemId == itemId
                                            && pt.PlotId == plotId
                                            && pt.DeleteDate == null).ToList();
        }

        public CemeteryTransaction GetByPlotIdAndDeceased(int plotId, int deceased1Id)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.Deceased1Id == deceased1Id
                                            && pt.PlotId == plotId
                                            && pt.DeleteDate == null).SingleOrDefault();
        }

        public CemeteryTransaction GetLastCemeteryTransactionByPlotId(int plotId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.PlotId == plotId
                                            && pt.DeleteDate == null).OrderByDescending(pt => pt.CreateDate).FirstOrDefault();
        }

        public CemeteryTransaction GetLastCemeteryTransactionByShiftedPlotId(int plotId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Applicant)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.DeleteDate == null).OrderByDescending(pt => pt.CreateDate).FirstOrDefault();
        }

        public IEnumerable<CemeteryTransaction> GetByPlotId(int plotId)
        {
            return MemorialContext.CemeteryTransactions
                .Include(pt => pt.Plot)
                .Include(pt => pt.Plot)
                .Include(pt => pt.CemeteryItem)
                .Where(pt => pt.PlotId == plotId && pt.DeleteDate == null)
                .ToList();
        }
        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}