using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;
using Memorial.Lib;

namespace Memorial.Controllers.Api
{
    public class RelationshipTypesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetRelationshipTypes()
        {
            var result = _unitOfWork.RelationshipTypes.GetAllActive();

            return Ok(Mapper.Map<IEnumerable<Core.Domain.RelationshipType>, IEnumerable<RelationshipTypeDto>>(result));
        }

    }
}
