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

        public AncestorAreaDto GetAreaDto(int id)
        {
            return _area.GetAreaDto(id);
        }

        public IEnumerable<AncestorAreaDto> GetAreaDtos()
        {
            return _area.GetAreaDtos();
        }

        public AncestorItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<AncestorItemDto> GetItemDtos()
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

        public bool CreateItem(AncestorItemDto ancestorItemDto)
        {
            if (_item.Create(ancestorItemDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateItem(AncestorItemDto ancestorItemDto)
        {
            var ancestorItemInDB = _item.GetItem(ancestorItemDto.Id);
            
            if ((ancestorItemInDB.AncestorAreaId != ancestorItemDto.AncestorAreaId
                || ancestorItemInDB.isOrder != ancestorItemDto.isOrder)
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestorItemId == ancestorItemInDB.Id && at.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(ancestorItemDto, ancestorItemInDB);

            if (_item.Update(ancestorItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestorItemId == id && at.DeleteDate == null).Any())
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



        public bool CreateArea(AncestorAreaDto ancestorAreaDto)
        {
            if (_area.Create(ancestorAreaDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateArea(AncestorAreaDto ancestorAreaDto)
        {
            var ancestorAreaInDB = _area.GetArea(ancestorAreaDto.Id);
            Mapper.Map(ancestorAreaDto, ancestorAreaInDB);

            if (ancestorAreaInDB.SiteId != ancestorAreaDto.SiteId
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestorItem.AncestorArea.SiteId == ancestorAreaInDB.SiteId && at.DeleteDate == null).Any())
            {
                return false;
            }

            if (_area.Update(ancestorAreaInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteArea(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestorItem.AncestorArea.SiteId == id && at.DeleteDate == null).Any())
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
                 && a.AncestorAreaId == ancestorDto.AncestorAreaId).Any())
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

            if (ancestorInDB.AncestorAreaId != ancestorDto.AncestorAreaId
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