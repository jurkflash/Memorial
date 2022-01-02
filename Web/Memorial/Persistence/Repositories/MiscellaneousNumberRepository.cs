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
            
            var miscellaneousItem = MemorialContext.MiscellaneousItems
                                .Include(m => m.Miscellaneous)
                                .Include(m => m.Miscellaneous.Site)
                                .Include(m => m.SubProductService)
                                .Where(m => m.Id == MiscellaneousItemId).SingleOrDefault();

            var number = numberRepository.GetMiscellaneousNewAF(
                (string.IsNullOrWhiteSpace(miscellaneousItem.Code) ? miscellaneousItem.SubProductService.Code : miscellaneousItem.Code),
                year);

            if (number == -1 || miscellaneousItem == null)
                return "";
            else
            {
                return miscellaneousItem.Miscellaneous.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(miscellaneousItem.Code) ? miscellaneousItem.SubProductService.Code : miscellaneousItem.Code) + 
                    "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int MiscellaneousItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var miscellaneousItem = MemorialContext.MiscellaneousItems
                                .Include(m => m.Miscellaneous)
                                .Include(m => m.Miscellaneous.Site)
                                .Include(m => m.SubProductService)
                                .Where(m => m.Id == MiscellaneousItemId).SingleOrDefault();

            var number = numberRepository.GetMiscellaneousNewIV(
                (string.IsNullOrWhiteSpace(miscellaneousItem.Code) ? miscellaneousItem.SubProductService.Code : miscellaneousItem.Code),
                year);

            if (number == -1 || miscellaneousItem == null)
                return "";
            else
            {
                return miscellaneousItem.Miscellaneous.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(miscellaneousItem.Code) ? miscellaneousItem.SubProductService.Code : miscellaneousItem.Code) + 
                    "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int MiscellaneousItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var miscellaneousItem = MemorialContext.MiscellaneousItems
                                .Include(m => m.Miscellaneous)
                                .Include(m => m.Miscellaneous.Site)
                                .Include(m => m.SubProductService)
                                .Where(m => m.Id == MiscellaneousItemId).SingleOrDefault();

            var number = numberRepository.GetMiscellaneousNewRE(
                (string.IsNullOrWhiteSpace(miscellaneousItem.Code) ? miscellaneousItem.SubProductService.Code : miscellaneousItem.Code),
                year);

            if (number == -1 || miscellaneousItem == null)
                return "";
            else
            {
                return miscellaneousItem.Miscellaneous.Site.Code + "/" +
                    (string.IsNullOrWhiteSpace(miscellaneousItem.Code) ? miscellaneousItem.SubProductService.Code : miscellaneousItem.Code) + 
                    "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }


        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}