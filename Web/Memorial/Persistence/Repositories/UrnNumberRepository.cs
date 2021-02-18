using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class UrnNumberRepository : Repository<UrnNumber>, IUrnNumberRepository
    {
        public UrnNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int UrnItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetUrnNewAF(UrnItemId, year);
            var urnItem = MemorialContext.UrnItems
                                .Include(u => u.Urn)
                                .Include(u => u.Urn.Site)
                                .Where(u => u.Id == UrnItemId &&
                                u.DeleteDate == null).SingleOrDefault();

            if (number == -1 || urnItem == null)
                return "";
            else
            {
                return urnItem.Urn.Site.Code + "/" + urnItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int UrnItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetUrnNewIV(UrnItemId, year);
            var urnItem = MemorialContext.UrnItems
                                .Include(u => u.Urn)
                                .Include(u => u.Urn.Site)
                                .Where(u => u.Id == UrnItemId &&
                                u.DeleteDate == null).SingleOrDefault();

            if (number == -1 || urnItem == null)
                return "";
            else
            {
                return urnItem.Urn.Site.Code + "/" + urnItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int UrnItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetUrnNewRE(UrnItemId, year);
            var urnItem = MemorialContext.UrnItems
                                .Include(u => u.Urn)
                                .Include(u => u.Urn.Site)
                                .Where(u => u.Id == UrnItemId &&
                                u.DeleteDate == null).SingleOrDefault();

            if (number == -1 || urnItem == null)
                return "";
            else
            {
                return urnItem.Urn.Site.Code + "/" + urnItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}