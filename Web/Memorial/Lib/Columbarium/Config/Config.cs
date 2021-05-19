using Memorial.Core;
using System.Collections.Generic;
using System.Linq;
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
        private readonly INiche _niche;

        public Config(
            IUnitOfWork unitOfWork,
            ICentre centre,
            IArea area,
            IItem item,
            INiche niche
            )
        {
            _unitOfWork = unitOfWork;
            _centre = centre;
            _area = area;
            _item = item;
            _niche = niche;
        }

        public ColumbariumCentreDto GetColumbariumCentreDto(int id)
        {
            return _centre.GetCentreDto(id);
        }

        public IEnumerable<ColumbariumCentreDto> GetColumbariumCentreDtos()
        {
            return _centre.GetCentreDtos();
        }

        public ColumbariumAreaDto GetColumbariumAreaDto(int id)
        {
            return _area.GetAreaDto(id);
        }

        public IEnumerable<ColumbariumAreaDto> GetAreaDtosByCentre(int centreId)
        {
            return _area.GetAreaDtosByCentre(centreId);
        }

        public NicheDto GetNicheDto(int id)
        {
            return _niche.GetNicheDto(id);
        }

        public IEnumerable<NicheDto> GetNicheDtosByAreaId(int areaId)
        {
            return _niche.GetNicheDtosByAreaId(areaId);
        }

        public ColumbariumItemDto GetItemDto(int id)
        {
            return _item.GetItemDto(id);
        }

        public IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId)
        {
            return _item.GetItemDtosByCentre(centreId);
        }

        public IEnumerable<ColumbariumNumber> GetNumbers()
        {
            return _unitOfWork.ColumbariumNumbers.GetAll();
        }


        public bool CreateNiche(NicheDto nicheDto)
        {
            if (_niche.Create(nicheDto))
            {
                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdateNiche(NicheDto nicheDto)
        {
            var nicheInDB = _niche.GetNiche(nicheDto.Id);

            if ((nicheInDB.NicheTypeId != nicheDto.NicheTypeDtoId
                || nicheInDB.ColumbariumAreaId != nicheDto.ColumbariumAreaDtoId)
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.NicheId == nicheDto.Id || qt.ShiftedNicheId == nicheDto.Id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(nicheDto, nicheInDB);

            if (_niche.Update(nicheInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteNiche(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.NicheId == id || qt.ShiftedNicheId == id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            if (_niche.Delete(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool CreateItem(ColumbariumItemDto columbariumItemDto)
        {
            if (_item.Create(columbariumItemDto))
            {
                _unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public bool UpdateItem(ColumbariumItemDto columbariumItemDto)
        {
            var columbariumItemInDB = _item.GetItem(columbariumItemDto.Id);

            if ((columbariumItemInDB.isOrder != columbariumItemDto.isOrder
                || columbariumItemInDB.ColumbariumCentreId != columbariumItemDto.ColumbariumCentreId)
                && _unitOfWork.ColumbariumTransactions.Find(qi => qi.ColumbariumItemId == columbariumItemDto.Id && qi.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(columbariumItemDto, columbariumItemInDB);

            if (_item.Update(columbariumItemInDB))
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

        public bool CreateCentre(ColumbariumCentreDto columbariumCentreDto)
        {
            if (_centre.Create(columbariumCentreDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateCentre(ColumbariumCentreDto columbariumCentreDto)
        {
            var columbariumCentreInDB = _centre.GetCentre(columbariumCentreDto.Id);

            if (columbariumCentreInDB.SiteId != columbariumCentreDto.SiteId
                && _unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.ColumbariumCentreId == columbariumCentreInDB.Id && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(columbariumCentreDto, columbariumCentreInDB);

            if (_centre.Update(columbariumCentreInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteCentre(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.ColumbariumCentreId == id && qt.DeleteDate == null).Any())
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


        public bool CreateArea(ColumbariumAreaDto columbariumAreaDto)
        {
            if (_area.Create(columbariumAreaDto))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool UpdateArea(ColumbariumAreaDto columbariumAreaDto)
        {
            var columbariumAreaInDB = _area.GetArea(columbariumAreaDto.Id);

            if (columbariumAreaInDB.ColumbariumCentreId != columbariumAreaDto.ColumbariumCentreDtoId
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.Niche.ColumbariumAreaId == columbariumAreaInDB.Id || qt.ShiftedNiche.ColumbariumAreaId == columbariumAreaInDB.Id) && qt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(columbariumAreaDto, columbariumAreaInDB);

            if (_area.Update(columbariumAreaInDB))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }

        public bool DeleteArea(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.Niche.ColumbariumAreaId == id || qt.ShiftedNiche.ColumbariumAreaId == id) && qt.DeleteDate == null).Any())
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