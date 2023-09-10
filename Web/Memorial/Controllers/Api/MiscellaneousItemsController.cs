using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Miscellaneous;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/miscellaneous/mains/items")]
    public class MiscellaneousItemsController : ApiController
    {
        private readonly IItem _item;

        public MiscellaneousItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/miscellaneous/mains/{id:int}/items")]
        [HttpGet]
        public IEnumerable<MiscellaneousItemDto> GetItems(int id)
        {
            return Mapper.Map<IEnumerable<MiscellaneousItemDto>>(_item.GetByMiscellaneous(id));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<MiscellaneousItemDto>(_item.GetById(id)));
        }

        [Route("~/api/miscellaneous/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByMiscellaneous(int id)
        {
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_item.GetAvailableItemByMiscellaneous(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(MiscellaneousItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = Mapper.Map<Core.Domain.MiscellaneousItem>(itemDto);
            var id = _item.Add(item);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, MiscellaneousItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.MiscellaneousItem>(itemDto);
            if (_item.Change(id, item))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_item.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
