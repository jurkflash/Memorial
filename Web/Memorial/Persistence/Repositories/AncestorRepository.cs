using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorRepository : Repository<Ancestor>, IAncestorRepository
    {
        public AncestorRepository(MemorialContext context) : base(context)
        {
        }

        public Ancestor GetActive(int id)
        {
            return MemorialContext.Ancestors
                .Include(a => a.Applicant)
                .Include(a => a.AncestorArea)
                .Where(a => a.Id == id && a.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Ancestor> GetByArea(int areaId)
        {
            return MemorialContext.Ancestors
                .Where(a => a.AncestorAreaId == areaId).ToList();
        }

        public IEnumerable<Ancestor> GetAvailableByArea(int ancestorAreaId)
        {
            return MemorialContext.Ancestors
                .Where(a => a.AncestorAreaId == ancestorAreaId && a.ApplicantId == null).ToList();
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int ancestorAreaId)
        {
            var d = new Dictionary<byte, IEnumerable<byte>>();
            var t = MemorialContext.Ancestors
                .Where(a => a.AncestorAreaId == ancestorAreaId)
                .Select(a => new { a.PositionY, a.PositionX })
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