﻿using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.SubProductService
{
    public interface ISubProductService
    {
        Core.Domain.SubProductService GetSubProductService();
        Core.Domain.SubProductService GetSubProductService(int id);
        SubProductServiceDto GetSubProductServiceDto();
        SubProductServiceDto GetSubProductServiceDto(int id);
        IEnumerable<SubProductServiceDto> GetSubProductServiceDtos();
        IEnumerable<Core.Domain.SubProductService> GetSubProductServices();
        void SetSubProductService(int id);
        IEnumerable<Core.Domain.SubProductService> GetSubProductServicesByProduct(int productId);
        IEnumerable<SubProductServiceDto> GetSubProductServiceDtosByProduct(int productId);
        IEnumerable<Core.Domain.SubProductService> GetSubProductServicesByProductIdAndOtherId(int productId, int otherId);
        IEnumerable<SubProductServiceDto> GetSubProductServiceDtosByProductIdAndOtherId(int productId, int otherId);
    }
}