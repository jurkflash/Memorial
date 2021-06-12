using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/ancestraltablets/areas/items")]
    public class AncestralTabletItemsController : ApiController
    {
        private readonly IItem _item;

        public AncestralTabletItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/items")]
        [HttpGet]
        public IEnumerable<AncestralTabletItemDto> GetItems(int areaId)
        {
            return _item.GetItemDtosByArea(areaId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            return Ok(_item.GetItemDto(id));
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByArea(int areaId)
        {
            return _item.GetAvailableItemDtosByArea(areaId);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateItem(AncestralTabletItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _item.Create(itemDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateItem(int id, AncestralTabletItemDto itemDto)
        {
            if (_item.Update(itemDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            if (_item.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
