using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Urn;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/urns/mains/items")]
    public class UrnItemsController : ApiController
    {
        private readonly IItem _item;

        public UrnItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/urns/mains/{id:int}/items")]
        [HttpGet]
        public IEnumerable<UrnItemDto> GetItems(int id)
        {
            return _item.GetItemDtosByUrn(id);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            return Ok(_item.GetItemDto(id));
        }

        [Route("~/api/urns/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByUrn(int id)
        {
            return _item.GetAvailableItemDtosByUrn(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateItem(UrnItemDto itemDto)
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
        public IHttpActionResult UpdateItem(int id, UrnItemDto itemDto)
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
