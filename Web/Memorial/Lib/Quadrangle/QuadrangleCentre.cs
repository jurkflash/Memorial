using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class QuadrangleCentre : IQuadrangleCentre
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.QuadrangleCentre _quadrangleCentre;

        public QuadrangleCentre(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetById(int id)
        {
            _quadrangleCentre = _unitOfWork.QuadrangleCentres.GetActive(id);
        }

        public IEnumerable<Core.Domain.QuadrangleCentre> GetBySite(byte sitId)
        {
            return _unitOfWork.QuadrangleCentres.GetBySite(sitId);
        }

        public IEnumerable<QuadrangleCentreDto> DtosGetBySite(byte sitId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleCentre>, IEnumerable<QuadrangleCentreDto>>(GetBySite(sitId));
        }
    }
}