using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.SubProductService
{
    public class SubProductService : ISubProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.SubProductService _subProductService;

        public SubProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetSubProductService(int id)
        {
            _subProductService = _unitOfWork.SubProductServices.Get(id);
        }

        public Core.Domain.SubProductService GetSubProductService()
        {
            return _subProductService;
        }

        public SubProductServiceDto GetSubProductServiceDto()
        {
            return Mapper.Map<Core.Domain.SubProductService, SubProductServiceDto>(_subProductService);
        }

        public Core.Domain.SubProductService GetSubProductService(int id)
        {
            return _unitOfWork.SubProductServices.Get(id);
        }

        public SubProductServiceDto GetSubProductServiceDto(int id)
        {
            return Mapper.Map<Core.Domain.SubProductService, SubProductServiceDto>(GetSubProductService(id));
        }

        public IEnumerable<Core.Domain.SubProductService> GetSubProductServices()
        {
            return _unitOfWork.SubProductServices.GetAll();
        }

        public IEnumerable<SubProductServiceDto> GetSubProductServiceDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(GetSubProductServices());
        }

        public IEnumerable<Core.Domain.SubProductService> GetSubProductServicesByProduct(int productId)
        {
            return _unitOfWork.SubProductServices.GetSubProductServicesByProductId(productId);
        }

        public IEnumerable<SubProductServiceDto> GetSubProductServiceDtosByProduct(int productId)
        {
            return
                Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>
                (GetSubProductServicesByProduct(productId));
        }

        public IEnumerable<Core.Domain.SubProductService> GetSubProductServicesByProductIdAndOtherId(int productId, int otherId)
        {
            return _unitOfWork.SubProductServices.GetSubProductServicesByProductIdAndOtherId(productId, otherId);
        }

        public IEnumerable<SubProductServiceDto> GetSubProductServiceDtosByProductIdAndOtherId(int productId, int otherId)
        {
            return
                Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>
                (GetSubProductServicesByProductIdAndOtherId(productId, otherId));
        }
    }
}