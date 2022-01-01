using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletRepository : Repository<AncestralTablet>, IAncestralTabletRepository
    {
        public AncestralTabletRepository(MemorialContext context) : base(context)
        {
        }

        public AncestralTablet GetActive(int id)
        {
            return MemorialContext.AncestralTablets
                .Include(a => a.Applicant)
                .Include(a => a.AncestralTabletArea)
                .Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<AncestralTablet> GetByArea(int areaId)
        {
            return MemorialContext.AncestralTablets
                .Where(a => a.AncestralTabletAreaId == areaId).ToList();
        }

        public AncestralTablet GetByAreaAndPositions(int areaId, int positionX, int positionY)
        {
            return MemorialContext.AncestralTablets
                .Where(a => a.AncestralTabletAreaId == areaId
                && a.PositionX == positionX
                && a.PositionY == positionY).SingleOrDefault();
        }

        public IEnumerable<AncestralTablet> GetAvailableByArea(int ancestralTabletAreaId)
        {
            return MemorialContext.AncestralTablets
                .Where(a => a.AncestralTabletAreaId == ancestralTabletAreaId && a.ApplicantId == null).ToList();
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByArea(int ancestralTabletAreaId)
        {
            var d = new Dictionary<byte, IEnumerable<byte>>();
            var t = MemorialContext.AncestralTablets
                .Where(a => a.AncestralTabletAreaId == ancestralTabletAreaId)
                .Select(a => new { a.PositionY, a.PositionX })
                .Distinct()
                .OrderBy(a => a.PositionY).ThenBy(a => a.PositionX).ToList();

            if (t.Any())
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