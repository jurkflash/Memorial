using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cremation;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cremations/mains/items")]
    public class CremationItemsController : ApiController
    {
        private readonly IItem _item;

        public CremationItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/cremations/mains/{id:int}/items")]
        [HttpGet]
        public IEnumerable<CremationItemDto> GetItems(int id)
        {
            return _item.GetItemDtosByCremation(id);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            return Ok(_item.GetItemDto(id));
        }

        [Route("~/api/cremations/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByCremation(int id)
        {
            return _item.GetAvailableItemDtosByCremation(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateItem(CremationItemDto itemDto)
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
        public IHttpActionResult UpdateItem(int id, CremationItemDto itemDto)
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
