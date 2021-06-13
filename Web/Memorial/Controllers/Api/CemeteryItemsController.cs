using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;

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
        public IHttpActionResult UpdateItem(int id, CemeteryItemDto itemDto)
        {
            if (_item.Update(itemDto))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
