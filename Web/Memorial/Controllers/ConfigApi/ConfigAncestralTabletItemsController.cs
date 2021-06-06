using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    public class ConfigAncestralTabletItemsController : ApiController
    {
        private readonly IConfig _config;

        public ConfigAncestralTabletItemsController(IConfig config)
        {
            _config = config;
        }

        public IHttpActionResult GetItems(int areaId)
        {
            return Ok(_config.GetItemDtosByAreaId(areaId));
        }

        public IHttpActionResult GetItem(int id)
        {
            return Ok(_config.GetItemDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateItem(AncestralTabletItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _config.CreateItem(itemDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateItem(int id, AncestralTabletItemDto itemDto)
        {
            if (_config.UpdateItem(itemDto))
                return Ok();
            else
                return InternalServerError();
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            if (_config.DeleteItem(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
