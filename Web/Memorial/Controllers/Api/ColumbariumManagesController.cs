using System;
using System.Web.Http;
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    public class ColumbariumManagesController : ApiController
    {
        private readonly IManage _manage;

        public ColumbariumManagesController(IManage manage)
        {
            _manage = manage;
        }

        public IHttpActionResult GetAmount(int itemId, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _manage.GetAmount(itemId, from, to);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

    }
}
