﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletTrackingRepository : Repository<AncestralTabletTracking>, IAncestralTabletTrackingRepository
    {
        public AncestralTabletTrackingRepository(MemorialContext context) : base(context)
        {
        }

        public AncestralTabletTracking GetLatestFirstTransactionByAncestralTabletId(int ancestralTabletId)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestralTabletId == ancestralTabletId).OrderByDescending(qt => qt.ActionDate).FirstOrDefault();
        }

        public IEnumerable<AncestralTabletTracking> GetTrackingByAncestralTabletId(int ancestralTabletId)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestralTabletId == ancestralTabletId).OrderByDescending(qt => qt.ActionDate).ToList();
        }

        public AncestralTabletTracking GetTrackingByTransactionAF(string ancestralTabletTransactionAF)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestralTabletTransactionAF == ancestralTabletTransactionAF).SingleOrDefault();
        }

        public AncestralTabletTracking GetTrackingByAncestralTabletIdAndTransactionAF(int ancestralTabletId, string ancestralTabletTransactionAF)
        {
            return MemorialContext.AncestralTabletTrackings.Where(qt => qt.AncestralTabletTransactionAF == ancestralTabletTransactionAF && qt.AncestralTabletId == ancestralTabletId).SingleOrDefault();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}