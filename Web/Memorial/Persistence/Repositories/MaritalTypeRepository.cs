﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class MaritalTypeRepository : Repository<MaritalType>, IMaritalTypeRepository
    {
        public MaritalTypeRepository(MemorialContext context) : base(context)
        {
        }

        public MaritalType GetActive(int id)
        {
            return MemorialContext.MaritalTypes
                .Where(mt => mt.Id == id && mt.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<MaritalType> GetAllActive()
        {
            return MemorialContext.MaritalTypes
                .Where(mt => mt.DeleteDate == null);
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}