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
            
            var urnItem = MemorialContext.UrnItems
                                .Include(u => u.Urn)
                                .Include(u => u.Urn.Site)
                                .Include(u => u.SubProductService)
                                .Where(u => u.Id == UrnItemId &&
                                u.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetUrnNewAF(
                (string.IsNullOrWhiteSpace(urnItem.Code) ? urnItem.SubProductService.Code : urnItem.Code), 
                year);

            if (number == -1 || urnItem == null)
                return "";
            else
            {
                return urnItem.Urn.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(urnItem.Code) ? urnItem.SubProductService.Code : urnItem.Code) + 
                    "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int UrnItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var urnItem = MemorialContext.UrnItems
                                .Include(u => u.Urn)
                                .Include(u => u.Urn.Site)
                                .Include(u => u.SubProductService)
                                .Where(u => u.Id == UrnItemId &&
                                u.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetUrnNewIV(
                (string.IsNullOrWhiteSpace(urnItem.Code) ? urnItem.SubProductService.Code : urnItem.Code), 
                year);

            if (number == -1 || urnItem == null)
                return "";
            else
            {
                return urnItem.Urn.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(urnItem.Code) ? urnItem.SubProductService.Code : urnItem.Code) + 
                    "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int UrnItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var urnItem = MemorialContext.UrnItems
                                .Include(u => u.Urn)
                                .Include(u => u.Urn.Site)
                                .Include(u => u.SubProductService)
                                .Where(u => u.Id == UrnItemId &&
                                u.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetUrnNewRE(
                (string.IsNullOrWhiteSpace(urnItem.Code) ? urnItem.SubProductService.Code : urnItem.Code), 
                year);

            if (number == -1 || urnItem == null)
                return "";
            else
            {
                return urnItem.Urn.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(urnItem.Code) ? urnItem.SubProductService.Code : urnItem.Code) + 
                    "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}