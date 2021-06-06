using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Data;

namespace Memorial.Persistence.Repositories
{
    public class CremationNumberRepository : Repository<CremationNumber>, ICremationNumberRepository
    {
        public CremationNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int CremationItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var cremationItem = MemorialContext.CremationItems
                                .Include(ci => ci.Cremation)
                                .Include(ci => ci.Cremation.Site)
                                .Include(a => a.SubProductService)
                                .Where(ci => ci.Id == CremationItemId &&
                                ci.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetCremationNewAF(
                (string.IsNullOrWhiteSpace(cremationItem.Code) ? cremationItem.SubProductService.Code : cremationItem.Code), 
                year);

            if (number == -1 || cremationItem == null)
                return "";
            else
            {
                return cremationItem.Cremation.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(cremationItem.Code) ? cremationItem.SubProductService.Code : cremationItem.Code) + 
                    "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int CremationItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var cremationItem = MemorialContext.CremationItems
                                .Include(ci => ci.Cremation)
                                .Include(ci => ci.Cremation.Site)
                                .Include(a => a.SubProductService)
                                .Where(ci => ci.Id == CremationItemId &&
                                ci.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetCremationNewIV(
                (string.IsNullOrWhiteSpace(cremationItem.Code) ? cremationItem.SubProductService.Code : cremationItem.Code), 
                year);

            if (number == -1 || cremationItem == null)
                return "";
            else
            {
                return cremationItem.Cremation.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(cremationItem.Code) ? cremationItem.SubProductService.Code : cremationItem.Code) + 
                    "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int CremationItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var cremationItem = MemorialContext.CremationItems
                                .Include(ci => ci.Cremation)
                                .Include(ci => ci.Cremation.Site)
                                .Include(a => a.SubProductService)
                                .Where(ci => ci.Id == CremationItemId &&
                                ci.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetCremationNewRE(
                (string.IsNullOrWhiteSpace(cremationItem.Code) ? cremationItem.SubProductService.Code : cremationItem.Code), 
                year);

            if (number == -1 || cremationItem == null)
                return "";
            else
            {
                return cremationItem.Cremation.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(cremationItem.Code) ? cremationItem.SubProductService.Code : cremationItem.Code) + 
                    "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}