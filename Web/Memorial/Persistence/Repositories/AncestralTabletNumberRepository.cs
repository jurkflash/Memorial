﻿using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Memorial.Persistence.Repositories
{
    public class AncestralTabletNumberRepository : Repository<AncestralTabletNumber>, IAncestralTabletNumberRepository
    {
        public AncestralTabletNumberRepository(MemorialContext context) : base(context)
        {
        }

        public string GetNewAF(int AncestralTabletItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var ancestralTabletItem = MemorialContext.AncestralTabletItems
                                .Include(a => a.AncestralTabletArea)
                                .Include(a => a.AncestralTabletArea.Site)
                                .Where(a => a.Id == AncestralTabletItemId &&
                                a.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetAncestralTabletNewAF(ancestralTabletItem.Code, year);

            if (number == -1 || ancestralTabletItem == null)
                return "";
            else
            {
                return ancestralTabletItem.AncestralTabletArea.Site.Code + "/" + ancestralTabletItem.Code + "/" + "AF-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewIV(int AncestralTabletItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var ancestralTabletItem = MemorialContext.AncestralTabletItems
                                .Include(a => a.AncestralTabletArea)
                                .Include(a => a.AncestralTabletArea.Site)
                                .Where(a => a.Id == AncestralTabletItemId &&
                                a.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetAncestralTabletNewIV(ancestralTabletItem.Code, year);

            if (number == -1 || ancestralTabletItem == null)
                return "";
            else
            {
                return ancestralTabletItem.AncestralTabletArea.Site.Code + "/" + ancestralTabletItem.Code + "/" + "IV-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public string GetNewRE(int AncestralTabletItemId, int year)
        {
            INumberRepository numberRepository = new NumberRepository();
            
            var ancestralTabletItem = MemorialContext.AncestralTabletItems
                                .Include(a => a.AncestralTabletArea)
                                .Include(a => a.AncestralTabletArea.Site)
                                .Where(a => a.Id == AncestralTabletItemId &&
                                a.DeleteDate == null).SingleOrDefault();

            var number = numberRepository.GetAncestralTabletNewRE(ancestralTabletItem.Code, year);

            if (number == -1 || ancestralTabletItem == null)
                return "";
            else
            {
                return ancestralTabletItem.AncestralTabletArea.Site.Code + "/" + ancestralTabletItem.Code + "/" + "RE-" + number.ToString().PadLeft(5, '0') + "/" + year.ToString();
            }
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}