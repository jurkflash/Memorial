﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class UrnItemRepository : Repository<UrnItem>, IUrnItemRepository
    {
        public UrnItemRepository(MemorialContext context) : base(context)
        {
        }

        public UrnItem GetActive(int id)
        {
            return MemorialContext.UrnItems
                .Include(ui => ui.Urn)
                .Include(ui => ui.SubProductService)
                .Where(ui => ui.Id == id && ui.DeletedDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<UrnItem> GetAllActive()
        {
            return MemorialContext.UrnItems
                .Include(ui => ui.Urn)
                .Include(ui => ui.SubProductService)
                .Where(ui => ui.DeletedDate == null)
                .ToList();
        }

        public IEnumerable<UrnItem> GetByUrn(int urnId)
        {
            return MemorialContext.UrnItems
                .Include(ui => ui.Urn)
                .Include(ui => ui.SubProductService)
                .Where(ui => ui.Id == urnId && ui.DeletedDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}