using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICentre _centre;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly IQuadrangle _quadrangle;

        public Config(
            IUnitOfWork unitOfWork,
            ICentre centre,
            IArea area,
            IItem item,
            IQuadrangle quadrangle
            )
        {
            _unitOfWork = unitOfWork;
            _centre = centre;
            _area = area;
            _item = item;
            _quadrangle = quadrangle;
        }

        public QuadrangleCentreDto GetQuadrangleCentreDto(int id)
        {
            return _centre.GetCentreDto(id);
        }

        public IEnumerable<QuadrangleCentreDto> GetQuadrangleCentreDtos()
        {
            return _centre.GetCentreDtos();
        }

        public QuadrangleAreaDto GetQuadrangleAreaDto(int id)
        {
            return _area.GetAreaDto(id);
        }

        public IEnumerable<QuadrangleAreaDto> GetAreaDtosByCentre(int centreId)
        {
            return _area.GetAreaDtosByCentre(centreId);
        }

        public QuadrangleDto GetQuadrangleDto(int id)
        {
            return _quadrangle.GetQuadrangleDto(id);
        }

        public IEnumerable<QuadrangleDto> GetQuadrangleDtosByAreaId(int areaId)
        {
            return _quadrangle.GetQuadrangleDtosByAreaId(areaId);
        }

        public ColumbariumItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId)
        {
            return _item.GetItemDtosByCentre(centreId);
        }

        public IEnumerable<QuadrangleNumber> GetNumbers()
        {
            return _unitOfWork.QuadrangleNumbers.GetAll();
        }


        public bool CreateQuadrangle(QuadrangleDto quadrangleDto)
        {
            if (_quadrangle.Create(quadrangleDto))
            {
                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdateQuadrangle(QuadrangleDto quadrangleDto)
        {
            var quadrangleInDB = _quadrangle.GetQuadrangle(quadrangleDto.Id);

            if ((quadrangleInDB.QuadrangleTypeId != quadrangleDto.QuadrangleTypeDtoId
                || quadrangleInDB.QuadrangleAreaId != quadrangleDto.QuadrangleAreaDtoId)
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.QuadrangleId == quadrangleDto.Id || qt.ShiftedQuadrangleId == quadrangleDto.Id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(quadrangleDto, quadrangleInDB);

            if (_quadrangle.Update(quadrangleInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteQuadrangle(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.QuadrangleId == id || qt.ShiftedQuadrangleId == id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            if (_quadrangle.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool CreateItem(ColumbariumItemDto quadrangleItemDto)
        {
            if (_item.Create(quadrangleItemDto))
            {
                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdateItem(ColumbariumItemDto quadrangleItemDto)
        {
            var quadrangleItemInDB = _item.GetItem(quadrangleItemDto.Id);

            if ((quadrangleItemInDB.isOrder != quadrangleItemDto.isOrder
                || quadrangleItemInDB.QuadrangleCentreId != quadrangleItemDto.QuadrangleCentreId)
                && _unitOfWork.ColumbariumTransactions.Find(qi => qi.ColumbariumItemId == quadrangleItemDto.Id && qi.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(quadrangleItemDto, quadrangleItemInDB);

            if (_item.Update(quadrangleItemInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteItem(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItemId == id && qt.DeleteDate == null).Any())
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

        public bool CreateCentre(QuadrangleCentreDto quadrangleCentreDto)
        {
            if (_centre.Create(quadrangleCentreDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateCentre(QuadrangleCentreDto quadrangleCentreDto)
        {
            var quadrangleCentreInDB = _centre.GetCentre(quadrangleCentreDto.Id);

            if (quadrangleCentreInDB.SiteId != quadrangleCentreDto.SiteId
                && _unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.QuadrangleCentreId == quadrangleCentreInDB.Id && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(quadrangleCentreDto, quadrangleCentreInDB);

            if (_centre.Update(quadrangleCentreInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteCentre(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.QuadrangleCentreId == id && qt.DeleteDate == null).Any())
            {
                return false;
            }

            if (_centre.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


        public bool CreateArea(QuadrangleAreaDto quadrangleAreaDto)
        {
            if (_area.Create(quadrangleAreaDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateArea(QuadrangleAreaDto quadrangleAreaDto)
        {
            var quadrangleAreaInDB = _area.GetArea(quadrangleAreaDto.Id);

            if (quadrangleAreaInDB.QuadrangleCentreId != quadrangleAreaDto.QuadrangleCentreDtoId
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.Quadrangle.QuadrangleAreaId == quadrangleAreaInDB.Id || qt.ShiftedQuadrangle.QuadrangleAreaId == quadrangleAreaInDB.Id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(quadrangleAreaDto, quadrangleAreaInDB);

            if (_area.Update(quadrangleAreaInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteArea(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.Quadrangle.QuadrangleAreaId == id || qt.ShiftedQuadrangle.QuadrangleAreaId == id) && qt.DeleteDate == null).Any())
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




    }
}