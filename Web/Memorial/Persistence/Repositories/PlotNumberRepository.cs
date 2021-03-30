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

        public string GetNewAF(int PlotItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetPlotNewAF(PlotItemId, year);
            var plotItem = MemorialContext.PlotItems
                                .Include(p => p.Plot)
                                .Include(p => p.Plot.PlotArea)
                                .Include(p => p.Plot.PlotArea.Site)
                                .Where(p => p.Id == PlotItemId &&
                                p.DeleteDate == null).SingleOrDefault();

            if (number == -1 || plotItem == null)
                return "";
            else
            {
                return plotItem.Plot.PlotArea.Site.Code + "/" + plotItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int PlotItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetPlotNewIV(PlotItemId, year);
            var plotItem = MemorialContext.PlotItems
                                .Include(p => p.Plot)
                                .Include(p => p.Plot.PlotArea)
                                .Include(p => p.Plot.PlotArea.Site)
                                .Where(p => p.Id == PlotItemId &&
                                p.DeleteDate == null).SingleOrDefault();

            if (number == -1 || plotItem == null)
                return "";
            else
            {
                return plotItem.Plot.PlotArea.Site.Code + "/" + plotItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int PlotItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetPlotNewRE(PlotItemId, year);
            var plotItem = MemorialContext.PlotItems
                                .Include(p => p.Plot)
                                .Include(p => p.Plot.PlotArea)
                                .Include(p => p.Plot.PlotArea.Site)
                                .Where(p => p.Id == PlotItemId &&
                                p.DeleteDate == null).SingleOrDefault();

            if (number == -1 || plotItem == null)
                return "";
            else
            {
                return plotItem.Plot.PlotArea.Site.Code + "/" + plotItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}