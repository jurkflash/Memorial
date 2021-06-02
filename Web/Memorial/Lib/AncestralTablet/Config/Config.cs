using Memorial.Core;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private readonly IArea _area;
        private readonly IAncestralTablet _ancestralTablet;

        public Config(
            IUnitOfWork unitOfWork,
            IItem item,
            IArea area,
            IAncestralTablet ancestralTablet
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _area = area;
            _ancestralTablet = ancestralTablet;
        }

        public AncestralTabletAreaDto GetAreaDto(int id)
        {
            return _area.GetAreaDto(id);
        }

        public IEnumerable<AncestralTabletAreaDto> GetAreaDtos()
        {
            return _area.GetAreaDtos();
        }

        public AncestralTabletItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<AncestralTabletItemDto> GetItemDtos()
        {
            return _item.GetItemDtos();
        }

        public AncestralTabletDto GetAncestralTabletDto(int id)
        {
            return _ancestralTablet.GetAncestralTabletDto(id);
        }

        public IEnumerable<AncestralTabletDto> GetAncestralTabletsByAreaId(int id)
        {
            return _ancestralTablet.GetAncestralTabletDtosByAreaId(id);
        }

        public IEnumerable<AncestralTabletNumber> GetNumbers()
        {
            return _unitOfWork.AncestralTabletNumbers.GetAll();
        }

        public bool CreateItem(AncestralTabletItemDto ancestralTabletItemDto)
        {
            if (_item.Create(ancestralTabletItemDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateItem(AncestralTabletItemDto ancestralTabletItemDto)
        {
            var ancestralTabletItemInDB = _item.GetItem(ancestralTabletItemDto.Id);
            
            if ((ancestralTabletItemInDB.AncestralTabletAreaId != ancestralTabletItemDto.AncestralTabletAreaDtoId
                || ancestralTabletItemInDB.isOrder != ancestralTabletItemDto.isOrder)
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItemId == ancestralTabletItemInDB.Id && at.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(ancestralTabletItemDto, ancestralTabletItemInDB);

            if (_item.Update(ancestralTabletItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItemId == id && at.DeleteDate == null).Any())
            {
                return false;
            }

            if (_item.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }



        public bool CreateArea(AncestralTabletAreaDto ancestralTabletAreaDto)
        {
            if (_area.Create(ancestralTabletAreaDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateArea(AncestralTabletAreaDto ancestralTabletAreaDto)
        {
            var ancestralTabletAreaInDB = _area.GetArea(ancestralTabletAreaDto.Id);
            Mapper.Map(ancestralTabletAreaDto, ancestralTabletAreaInDB);

            if (ancestralTabletAreaInDB.SiteId != ancestralTabletAreaDto.SiteDtoId
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == ancestralTabletAreaInDB.SiteId && at.DeleteDate == null).Any())
            {
                return false;
            }

            if (_area.Update(ancestralTabletAreaInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteArea(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == id && at.DeleteDate == null).Any())
            {
                return false;
            }

            if (_area.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


        public bool CreateAncestralTablet(AncestralTabletDto ancestralTabletDto)
        {
            if (_unitOfWork.AncestralTablets.Find(a => a.PositionX == ancestralTabletDto.PositionX
                 && a.PositionY == ancestralTabletDto.PositionY
                 && a.AncestralTabletAreaId == ancestralTabletDto.AncestralTabletAreaId).Any())
            {
                return false;
            }

            if (_ancestralTablet.Create(ancestralTabletDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateAncestralTablet(AncestralTabletDto ancestralTabletDto)
        {
            var ancestralTabletInDB = _ancestralTablet.GetAncestralTablet(ancestralTabletDto.Id);
            Mapper.Map(ancestralTabletDto, ancestralTabletInDB);

            if (ancestralTabletInDB.AncestralTabletAreaId != ancestralTabletDto.AncestralTabletAreaId
                && (ancestralTabletInDB.ApplicantId != null || ancestralTabletInDB.hasDeceased))
            {
                return false;
            }

            if (_ancestralTablet.Update(ancestralTabletInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteAncestralTablet(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => (at.AncestralTabletId == id || at.ShiftedAncestralTabletId == id) && at.DeleteDate == null).Any())
            {
                return false;
            }

            if (_ancestralTablet.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }
    }
}