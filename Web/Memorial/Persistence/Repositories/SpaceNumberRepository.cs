using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class SpaceNumberRepository : Repository<SpaceNumber>, ISpaceNumberRepository
    {
        public SpaceNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int SpaceItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetSpaceNewAF(SpaceItemId, year);
            var spaceItem = MemorialContext.SpaceItems
                                .Include(s => s.Space)
                                .Include(s => s.Space.Site)
                                .Where(s => s.Id == SpaceItemId &&
                                s.DeleteDate == null).SingleOrDefault();

            if (number == -1 || spaceItem == null)
                return "";
            else
            {
                return spaceItem.Space.Site.Code + "/" + spaceItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int SpaceItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetSpaceNewIV(SpaceItemId, year);
            var spaceItem = MemorialContext.SpaceItems
                                .Include(s => s.Space)
                                .Include(s => s.Space.Site)
                                .Where(s => s.Id == SpaceItemId &&
                                s.DeleteDate == null).SingleOrDefault();

            if (number == -1 || spaceItem == null)
                return "";
            else
            {
                return spaceItem.Space.Site.Code + "/" + spaceItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int SpaceItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetSpaceNewRE(SpaceItemId, year);
            var spaceItem = MemorialContext.SpaceItems
                                .Include(s => s.Space)
                                .Include(s => s.Space.Site)
                                .Where(s => s.Id == SpaceItemId &&
                                s.DeleteDate == null).SingleOrDefault();

            if (number == -1 || spaceItem == null)
                return "";
            else
            {
                return spaceItem.Space.Site.Code + "/" + spaceItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}