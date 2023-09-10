using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cemeteries/plots/items")]
    public class CemeteryItemsController : ApiController
    {
        private readonly IItem _item;

        public CemeteryItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/cemeteries/plots/{plotId:int}/items")]
        [HttpGet]
        public IEnumerable<CemeteryItemDto> GetItemDtosByPlot(int plotId)
        {
            return _item.GetItemDtosByPlot(plotId);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, CemeteryItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.CemeteryItem>(itemDto);
            if (_item.Change(id, item))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
