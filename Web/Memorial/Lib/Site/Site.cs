using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Site
{
    public class Site : ISite
    {
        private readonly IUnitOfWork _unitOfWork;

        public Site(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Site Get(int id)
        {
            return _unitOfWork.Sites.GetActive(id);
        }

        public IEnumerable<Core.Domain.Site> GetAll()
        {
            return _unitOfWork.Sites.GetAllActive();
        }

        public int Add(Core.Domain.Site site)
        {
            _unitOfWork.Sites.Add(site);

            _unitOfWork.Complete();

            return site.Id;
        }

        public bool Change(int id, Core.Domain.Site site)
        {
            var siteInDb = _unitOfWork.Sites.Get(id);

            Mapper.Map(site, siteInDb);

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (
                _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == id).Any() ||
                _unitOfWork.CremationTransactions.Find(at => at.CremationItem.Cremation.SiteId == id).Any() ||
                _unitOfWork.MiscellaneousTransactions.Find(at => at.MiscellaneousItem.Miscellaneous.SiteId == id).Any() ||
                _unitOfWork.CemeteryTransactions.Find(at => at.Plot.CemeteryArea.SiteId == id).Any() ||
                _unitOfWork.ColumbariumTransactions.Find(at => at.ColumbariumItem.ColumbariumCentre.SiteId == id).Any() ||
                _unitOfWork.SpaceTransactions.Find(at => at.SpaceItem.Space.SiteId == id).Any() ||
                _unitOfWork.UrnTransactions.Find(at => at.UrnItem.Urn.SiteId == id).Any())
            {
                return false;
            }

            var siteInDb = _unitOfWork.Sites.Get(id);
            if (siteInDb != null)
            {
                _unitOfWork.Sites.Remove(siteInDb);
                _unitOfWork.Complete();
            }

            return true;
        }
    }
}