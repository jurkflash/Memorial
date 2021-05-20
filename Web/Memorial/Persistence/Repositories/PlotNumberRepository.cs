using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class PlotNumberRepository : Repository<PlotNumber>, IPlotNumberRepository
    {
        public PlotNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int CemeteryItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var cemeteryItem = MemorialContext.CemeteryItems
                                .Include(p => p.Plot)
                                .Include(p => p.Plot.CemeteryArea)
                                .Include(p => p.Plot.CemeteryArea.Site)
                                .Where(p => p.Id == CemeteryItemId &&
                                p.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetPlotNewAF(cemeteryItem.Code, year);

            if (number == -1 || cemeteryItem == null)
                return "";
            else
            {
                return cemeteryItem.Plot.CemeteryArea.Site.Code + "/" + cemeteryItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int CemeteryItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var cemeteryItem = MemorialContext.CemeteryItems
                                .Include(p => p.Plot)
                                .Include(p => p.Plot.CemeteryArea)
                                .Include(p => p.Plot.CemeteryArea.Site)
                                .Where(p => p.Id == CemeteryItemId &&
                                p.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetPlotNewIV(cemeteryItem.Code, year);

            if (number == -1 || cemeteryItem == null)
                return "";
            else
            {
                return cemeteryItem.Plot.CemeteryArea.Site.Code + "/" + cemeteryItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int CemeteryItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var cemeteryItem = MemorialContext.CemeteryItems
                                .Include(p => p.Plot)
                                .Include(p => p.Plot.CemeteryArea)
                                .Include(p => p.Plot.CemeteryArea.Site)
                                .Where(p => p.Id == CemeteryItemId &&
                                p.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetPlotNewRE(cemeteryItem.Code, year);

            if (number == -1 || cemeteryItem == null)
                return "";
            else
            {
                return cemeteryItem.Plot.CemeteryArea.Site.Code + "/" + cemeteryItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}