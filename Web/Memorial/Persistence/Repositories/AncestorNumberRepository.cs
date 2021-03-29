using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class AncestorNumberRepository : Repository<AncestorNumber>, IAncestorNumberRepository
    {
        public AncestorNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int AncestorItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetAncestorNewAF(AncestorItemId, year);
            var ancestorItem = MemorialContext.AncestorItems
                                .Include(a => a.AncestorArea)
                                .Include(a => a.AncestorArea.Site)
                                .Where(a => a.Id == AncestorItemId &&
                                a.DeleteDate == null).SingleOrDefault();

            if (number == -1 || ancestorItem == null)
                return "";
            else
            {
                return ancestorItem.AncestorArea.Site.Code + "/" + ancestorItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int AncestorItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetAncestorNewIV(AncestorItemId, year);
            var ancestorItem = MemorialContext.AncestorItems
                                .Include(a => a.AncestorArea)
                                .Include(a => a.AncestorArea.Site)
                                .Where(a => a.Id == AncestorItemId &&
                                a.DeleteDate == null).SingleOrDefault();

            if (number == -1 || ancestorItem == null)
                return "";
            else
            {
                return ancestorItem.AncestorArea.Site.Code + "/" + ancestorItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int AncestorItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetAncestorNewRE(AncestorItemId, year);
            var ancestorItem = MemorialContext.AncestorItems
                                .Include(a => a.AncestorArea)
                                .Include(a => a.AncestorArea.Site)
                                .Where(a => a.Id == AncestorItemId &&
                                a.DeleteDate == null).SingleOrDefault();

            if (number == -1 || ancestorItem == null)
                return "";
            else
            {
                return ancestorItem.AncestorArea.Site.Code + "/" + ancestorItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}