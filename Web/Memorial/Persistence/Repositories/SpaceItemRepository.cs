﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class SpaceItemRepository : Repository<SpaceItem>, ISpaceItemRepository
    {
        public SpaceItemRepository(MemorialContext context) : base(context)
        {
        }

        public SpaceItem GetActive(int id)
        {
            return MemorialContext.SpaceItems
                .Where(si => si.Id == id && si.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<SpaceItem> GetBySpace(int spaceId)
        {
            return MemorialContext.SpaceItems
                .Where(si => si.SpaceId == spaceId && si.DeleteDate == null).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}