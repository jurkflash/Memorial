using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Data;

namespace Memorial.Persistence.Repositories
{
    public class MiscellaneousNumberRepository : Repository<MiscellaneousNumber>, IMiscellaneousNumberRepository
    {
        public MiscellaneousNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int MiscellaneousItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetMiscellaneousNewAF(MiscellaneousItemId, year);
            var miscellaneousItem = MemorialContext.MiscellaneousItems
                                .Include(m => m.Miscellaneous)
                                .Include(m => m.Miscellaneous.Site)
                                .Where(m => m.Id == MiscellaneousItemId &&
                                m.DeleteDate == null).SingleOrDefault();

            if (number == -1 || miscellaneousItem == null)
                return "";
            else
            {
                return miscellaneousItem.Miscellaneous.Site.Code + "/" + miscellaneousItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int MiscellaneousItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetMiscellaneousNewIV(MiscellaneousItemId, year);
            var miscellaneousItem = MemorialContext.MiscellaneousItems
                                .Include(m => m.Miscellaneous)
                                .Include(m => m.Miscellaneous.Site)
                                .Where(m => m.Id == MiscellaneousItemId &&
                                m.DeleteDate == null).SingleOrDefault();

            if (number == -1 || miscellaneousItem == null)
                return "";
            else
            {
                return miscellaneousItem.Miscellaneous.Site.Code + "/" + miscellaneousItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int MiscellaneousItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            var number = numberRepository.GetMiscellaneousNewRE(MiscellaneousItemId, year);
            var miscellaneousItem = MemorialContext.MiscellaneousItems
                                .Include(m => m.Miscellaneous)
                                .Include(m => m.Miscellaneous.Site)
                                .Where(m => m.Id == MiscellaneousItemId &&
                                m.DeleteDate == null).SingleOrDefault();

            if (number == -1 || miscellaneousItem == null)
                return "";
            else
            {
                return miscellaneousItem.Miscellaneous.Site.Code + "/" + miscellaneousItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}