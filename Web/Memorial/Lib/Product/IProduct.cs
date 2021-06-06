using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Product
{
    public interface IProduct
    {
        string AncestralTablet { get; }
        string AncestralTablets { get; }
        string Cemeteries { get; }
        string Cemetery { get; }
        string Columbarium { get; }
        string Columbariums { get; }
        string Cremation { get; }
        string Cremations { get; }
        string Miscellaneous { get; }
        string Space { get; }
        string Spaces { get; }
        string Urn { get; }
        string Urns { get; }

        Core.Domain.Product GetProduct();
        Core.Domain.Product GetProduct(int id);
        ProductDto GetProductDto();
        ProductDto GetProductDto(int id);
        IEnumerable<ProductDto> GetProductDtos();
        IEnumerable<Core.Domain.Product> GetProducts();
        void SetProduct(int id);
    }
}