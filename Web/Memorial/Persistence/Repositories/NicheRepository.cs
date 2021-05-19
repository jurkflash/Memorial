using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class NicheRepository : Repository<Niche>, INicheRepository
    {
        public NicheRepository(MemorialContext context) : base(context)
        {
        }

        public Niche GetActive(int id)
        {
            return MemorialContext.Niches
                .Include(q => q.Applicant)
                .Include(q => q.NicheType)
                .Include(q => q.ColumbariumArea)
                .Where(q => q.Id == id && q.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Niche> GetByArea(int columbariumAreaId)
        {
            return MemorialContext.Niches
                .Where(q => q.ColumbariumAreaId == columbariumAreaId).ToList();
        }

        public IEnumerable<Niche> GetAvailableByArea(int columbariumAreaId)
        {
            return MemorialContext.Niches
                .Include(q => q.NicheType)
                .Where(q => q.ColumbariumAreaId == columbariumAreaId && q.ApplicantId == null).ToList();
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int columbariumAreaId)
        {
            var d = new Dictionary<byte, IEnumerable<byte>>();
            var t = MemorialContext.Niches
                .Where(q => q.ColumbariumAreaId == columbariumAreaId)
                .Select(q => new { q.PositionY, q.PositionX })
                .Distinct()
                .OrderBy(a => a.PositionY).ThenBy(a => a.PositionX).ToList();

            if (t != null)
            {
                byte y = t.Min(m => m.PositionY);
                var x = new List<byte>();
                foreach (var c in t)
                {
                    if (y != c.PositionY)
                    {
                        d.Add(y, x);

                        y = c.PositionY;
                        x = new List<byte>();
                        x.Add(c.PositionX);
                    }
                    else
                    {
                        x.Add(c.PositionX);
                    }
                }
                d.Add(y, x);
            }
            return d;
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}