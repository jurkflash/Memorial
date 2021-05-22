using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Ancestor
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private readonly IArea _area;
        private readonly IAncestor _ancestor;

        public Config(
            IUnitOfWork unitOfWork,
            IItem item,
            IArea area,
            IAncestor ancestor
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _area = area;
            _ancestor = ancestor;
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

        public AncestorDto GetAncestorDto(int id)
        {
            return _ancestor.GetAncestorDto(id);
        }

        public IEnumerable<AncestorDto> GetAncestorsByAreaId(int id)
        {
            return _ancestor.GetAncestorDtosByAreaId(id);
        }

        public IEnumerable<AncestorNumber> GetNumbers()
        {
            return _unitOfWork.AncestorNumbers.GetAll();
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
            
            if ((ancestralTabletItemInDB.AncestralTabletAreaId != ancestralTabletItemDto.AncestralTabletAreaId
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

            if (ancestralTabletAreaInDB.SiteId != ancestralTabletAreaDto.SiteId
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


        public bool CreateAncestor(AncestorDto ancestorDto)
        {
            if (_unitOfWork.Ancestors.Find(a => a.PositionX == ancestorDto.PositionX
                 && a.PositionY == ancestorDto.PositionY
                 && a.AncestralTabletAreaId == ancestorDto.AncestralTabletAreaId).Any())
            {
                return false;
            }

            if (_ancestor.Create(ancestorDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateAncestor(AncestorDto ancestorDto)
        {
            var ancestorInDB = _ancestor.GetAncestor(ancestorDto.Id);
            Mapper.Map(ancestorDto, ancestorInDB);

            if (ancestorInDB.AncestralTabletAreaId != ancestorDto.AncestralTabletAreaId
                && (ancestorInDB.ApplicantId != null || ancestorInDB.hasDeceased))
            {
                return false;
            }

            if (_ancestor.Update(ancestorInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteAncestor(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => (at.AncestorId == id || at.ShiftedAncestorId == id) && at.DeleteDate == null).Any())
            {
                return false;
            }

            if (_ancestor.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }
    }
}