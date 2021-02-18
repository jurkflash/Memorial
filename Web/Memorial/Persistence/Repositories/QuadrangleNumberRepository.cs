using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleNumberRepository : Repository<QuadrangleNumber>, IQuadrangleNumberRepository
    {
        public QuadrangleNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int QuadrangleItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetQuadrangleNewAF(QuadrangleItemId, year);
            var quadrangleItem = MemorialContext.QuadrangleItems
                                .Include(q => q.QuadrangleCentre)
                                .Include(q => q.QuadrangleCentre.Site)
                                .Where(q => q.Id == QuadrangleItemId &&
                                q.DeleteDate == null).SingleOrDefault();

            if (number == -1 || quadrangleItem == null)
                return "";
            else
            {
                return quadrangleItem.QuadrangleCentre.Site.Code + "/" + quadrangleItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int QuadrangleItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetQuadrangleNewIV(QuadrangleItemId, year);
            var quadrangleItem = MemorialContext.QuadrangleItems
                                .Include(q => q.QuadrangleCentre)
                                .Include(q => q.QuadrangleCentre.Site)
                                .Where(q => q.Id == QuadrangleItemId &&
                                q.DeleteDate == null).SingleOrDefault();

            if (number == -1 || quadrangleItem == null)
                return "";
            else
            {
                return quadrangleItem.QuadrangleCentre.Site.Code + "/" + quadrangleItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int QuadrangleItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetQuadrangleNewRE(QuadrangleItemId, year);
            var quadrangleItem = MemorialContext.QuadrangleItems
                                .Include(q => q.QuadrangleCentre)
                                .Include(q => q.QuadrangleCentre.Site)
                                .Where(q => q.Id == QuadrangleItemId &&
                                q.DeleteDate == null).SingleOrDefault();

            if (number == -1 || quadrangleItem == null)
                return "";
            else
            {
                return quadrangleItem.QuadrangleCentre.Site.Code + "/" + quadrangleItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}