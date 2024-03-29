﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletItemRepository : Repository<AncestralTabletItem>, IAncestralTabletItemRepository
    {
        public AncestralTabletItemRepository(MemorialContext context) : base(context)
        {
        }

        public AncestralTabletItem GetActive(int id)
        {
            return MemorialContext.AncestralTabletItems
                .Include(ai => ai.SubProductService)
                .Include(ai => ai.AncestralTabletArea)
                .Where(ai => ai.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTabletItem> GetAllActive()
        {
            MemorialContext.Configuration.LazyLoadingEnabled = false;
            return MemorialContext.AncestralTabletItems
                .Include(ai => ai.SubProductService)
                .ToList();
        }

        public IEnumerable<AncestralTabletItem> GetByArea(int areaId)
        {
            return MemorialContext.AncestralTabletItems
                .Include(ai => ai.SubProductService)
                .Include(ai => ai.AncestralTabletArea)
                .Where(ai => ai.AncestralTabletAreaId == areaId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}