﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class ReligionTypeRepository : Repository<ReligionType>, IReligionTypeRepository
    {
        public ReligionTypeRepository(MemorialContext context) : base(context)
        {
        }

        public ReligionType GetActive(int id)
        {
            return MemorialContext.ReligionTypes
                .Where(rt => rt.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<ReligionType> GetAllActive()
        {
            return MemorialContext.ReligionTypes.ToList();
        }
        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}