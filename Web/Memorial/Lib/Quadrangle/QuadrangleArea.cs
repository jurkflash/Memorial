using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class QuadrangleArea : IQuadrangleArea
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.QuadrangleArea _quadrangleArea;

        public QuadrangleArea(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetById(int id)
        {
            _quadrangleArea = _unitOfWork.QuadrangleAreas.GetActive(id);
        }

        public IEnumerable<Core.Domain.QuadrangleArea> GetByCentre(int centreId)
        {
            return _unitOfWork.QuadrangleAreas.GetByCentre(centreId);
        }

        public IEnumerable<QuadrangleAreaDto> DtosGetByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleArea>, IEnumerable<QuadrangleAreaDto>>(GetByCentre(centreId));
        }
    }
}