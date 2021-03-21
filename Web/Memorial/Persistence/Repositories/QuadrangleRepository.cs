using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleRepository : Repository<Quadrangle>, IQuadrangleRepository
    {
        public QuadrangleRepository(MemorialContext context) : base(context)
        {
        }

        public Quadrangle GetActive(int id)
        {
            return MemorialContext.Quadrangles
                .Include(q => q.Applicant)
                .Include(q => q.QuadrangleType)
                .Include(q => q.QuadrangleArea)
                //.Include(q => q.QuadrangleArea.QuadrangleCentre)
                .Where(q => q.Id == id && q.DeleteDate == null)
                .SingleOrDefault();
        }

        public IEnumerable<Quadrangle> GetByArea(int quadrangleAreaId)
        {
            return MemorialContext.Quadrangles
                .Where(q => q.QuadrangleAreaId == quadrangleAreaId).ToList();
        }

        public IEnumerable<Quadrangle> GetAvailableByArea(int quadrangleAreaId)
        {
            return MemorialContext.Quadrangles
                .Where(q => q.QuadrangleAreaId == quadrangleAreaId && q.ApplicantId == null).ToList();
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int quadrangleAreaId)
        {
            var d = new Dictionary<byte, IEnumerable<byte>>();
            var t = MemorialContext.Quadrangles
                .Where(q => q.QuadrangleAreaId == quadrangleAreaId)
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