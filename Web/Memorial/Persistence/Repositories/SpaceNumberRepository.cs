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
            
            var spaceItem = MemorialContext.SpaceItems
                                .Include(s => s.Space)
                                .Include(s => s.Space.Site)
                                .Include(s => s.SubProductService)
                                .Where(s => s.Id == SpaceItemId &&
                                s.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetSpaceNewAF(
                (string.IsNullOrWhiteSpace(spaceItem.Code) ? spaceItem.SubProductService.Code : spaceItem.Code),
                year);

            if (number == -1 || spaceItem == null)
                return "";
            else
            {
                return spaceItem.Space.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(spaceItem.Code) ? spaceItem.SubProductService.Code : spaceItem.Code) + 
                    "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int SpaceItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var spaceItem = MemorialContext.SpaceItems
                                .Include(s => s.Space)
                                .Include(s => s.Space.Site)
                                .Include(s => s.SubProductService)
                                .Where(s => s.Id == SpaceItemId &&
                                s.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetSpaceNewIV(
                (string.IsNullOrWhiteSpace(spaceItem.Code) ? spaceItem.SubProductService.Code : spaceItem.Code),
                year);

            if (number == -1 || spaceItem == null)
                return "";
            else
            {
                return spaceItem.Space.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(spaceItem.Code) ? spaceItem.SubProductService.Code : spaceItem.Code) + 
                    "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int SpaceItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var spaceItem = MemorialContext.SpaceItems
                                .Include(s => s.Space)
                                .Include(s => s.Space.Site)
                                .Include(s => s.SubProductService)
                                .Where(s => s.Id == SpaceItemId &&
                                s.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetSpaceNewRE(
                (string.IsNullOrWhiteSpace(spaceItem.Code) ? spaceItem.SubProductService.Code : spaceItem.Code),
                year);

            if (number == -1 || spaceItem == null)
                return "";
            else
            {
                return spaceItem.Space.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(spaceItem.Code) ? spaceItem.SubProductService.Code : spaceItem.Code) + 
                    "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}