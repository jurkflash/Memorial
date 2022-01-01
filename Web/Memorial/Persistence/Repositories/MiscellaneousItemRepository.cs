﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class MiscellaneousItemRepository : Repository<MiscellaneousItem>, IMiscellaneousItemRepository
    {
        public MiscellaneousItemRepository(MemorialContext context) : base(context)
        {
        }

        public MiscellaneousItem GetActive(int id)
        {
            return MemorialContext.MiscellaneousItems
                .Include(mi => mi.SubProductService)
                .Where(mi => mi.Id == id && mi.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<MiscellaneousItem> GetAllActive()
        {
            return MemorialContext.MiscellaneousItems
                .Include(mi => mi.SubProductService)
                .Where(mi => mi.DeletedDate == null)
                .ToList();
        }

        public IEnumerable<MiscellaneousItem> GetByMiscellaneous(int miscellaneousId)
        {
            return MemorialContext.MiscellaneousItems
                .Include(mi => mi.SubProductService)
                .Where(mi => mi.Id == miscellaneousId && mi.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}