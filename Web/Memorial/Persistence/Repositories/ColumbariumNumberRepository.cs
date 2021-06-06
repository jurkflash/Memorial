using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class ColumbariumNumberRepository : Repository<ColumbariumNumber>, IColumbariumNumberRepository
    {
        public ColumbariumNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int columbariumItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var columbariumItem = MemorialContext.ColumbariumItems
                                .Include(q => q.ColumbariumCentre)
                                .Include(q => q.ColumbariumCentre.Site)
                                .Include(a => a.SubProductService)
                                .Where(q => q.Id == columbariumItemId &&
                                q.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetColumbariumNewAF(
                (string.IsNullOrWhiteSpace(columbariumItem.Code) ? columbariumItem.SubProductService.Code : columbariumItem.Code), 
                year);

            if (number == -1 || columbariumItem == null)
                return "";
            else
            {
                return columbariumItem.ColumbariumCentre.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(columbariumItem.Code) ? columbariumItem.SubProductService.Code : columbariumItem.Code) + 
                    "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int columbariumItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var columbariumItem = MemorialContext.ColumbariumItems
                                .Include(q => q.ColumbariumCentre)
                                .Include(q => q.ColumbariumCentre.Site)
                                .Include(a => a.SubProductService)
                                .Where(q => q.Id == columbariumItemId &&
                                q.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetColumbariumNewIV(
                (string.IsNullOrWhiteSpace(columbariumItem.Code) ? columbariumItem.SubProductService.Code : columbariumItem.Code), 
                year);

            if (number == -1 || columbariumItem == null)
                return "";
            else
            {
                return columbariumItem.ColumbariumCentre.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(columbariumItem.Code) ? columbariumItem.SubProductService.Code : columbariumItem.Code) + 
                    "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int columbariumItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var columbariumItem = MemorialContext.ColumbariumItems
                                .Include(q => q.ColumbariumCentre)
                                .Include(q => q.ColumbariumCentre.Site)
                                .Include(a => a.SubProductService)
                                .Where(q => q.Id == columbariumItemId &&
                                q.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetColumbariumNewRE(
                (string.IsNullOrWhiteSpace(columbariumItem.Code) ? columbariumItem.SubProductService.Code : columbariumItem.Code), 
                year);

            if (number == -1 || columbariumItem == null)
                return "";
            else
            {
                return columbariumItem.ColumbariumCentre.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(columbariumItem.Code) ? columbariumItem.SubProductService.Code : columbariumItem.Code) + 
                    "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}