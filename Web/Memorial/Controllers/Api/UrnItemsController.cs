using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Urn;
using AutoMapper;

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
            return Mapper.Map<IEnumerable<UrnItemDto>>(_item.GetByUrn(id));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<UrnItemDto>(_item.GetById(id)));
        }

        [Route("~/api/urns/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByUrn(int id)
        {
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_item.GetAvailableItemByUrn(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(UrnItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = Mapper.Map<Core.Domain.UrnItem>(itemDto);
            var id = _item.Add(item);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateItem(int id, UrnItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.UrnItem>(itemDto);
            if (_item.Change(id, item))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            if (_item.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
