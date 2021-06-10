using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;

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

        public IEnumerable<ProductDto> GetProducts()
        {
            return _product.GetProductDtos();
        }

        public IHttpActionResult GetProduct(int id)
        {
            return Ok(_product.GetProductDto(id));
        }

    }
}
