using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProduct _product;

        public ProductsController(IProduct product)
        {
            _product = product;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return Mapper.Map<IEnumerable<ProductDto>>(_product.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<ProductDto>(_product.Get(id)));
        }

    }
}
