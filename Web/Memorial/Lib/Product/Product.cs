using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Product
{
    public class Product : IProduct
    {
        private readonly string MyCemetery = "Cemetery";
        private readonly string MyCemeteries = "Cemeteries";
        private readonly string MyAncestralTablet = "AncestralTablet";
        private readonly string MyAncestralTablets = "AncestralTablets";
        private readonly string MyCremation = "Cremation";
        private readonly string MyCremations = "Cremations";
        private readonly string MyUrn = "Urn";
        private readonly string MyUrns = "Urns";
        private readonly string MyColumbarium = "Columbarium";
        private readonly string MyColumbariums = "Columbariums";
        private readonly string MySpace = "Space";
        private readonly string MySpaces = "Spaces";
        private readonly string MyMiscellaneous = "Miscellaneous";

        public string Cemetery { get { return MyCemetery; } }
        public string Cemeteries { get { return MyCemeteries; } }
        public string AncestralTablet { get { return MyAncestralTablet; } }
        public string AncestralTablets { get { return MyAncestralTablets; } }
        public string Cremation { get { return MyCremation; } }
        public string Cremations { get { return MyCremations; } }
        public string Urn { get { return MyUrn; } }
        public string Urns { get { return MyUrns; } }
        public string Columbarium { get { return MyColumbarium; } }
        public string Columbariums { get { return MyColumbariums; } }
        public string Space { get { return MySpace; } }
        public string Spaces { get { return MySpaces; } }
        public string Miscellaneous { get { return MyMiscellaneous; } }


        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.Product _product;

        public Product(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetProduct(int id)
        {
            _product = _unitOfWork.Products.Get(id);
        }

        public Core.Domain.Product GetProduct()
        {
            return _product;
        }

        public ProductDto GetProductDto()
        {
            return Mapper.Map<Core.Domain.Product, ProductDto>(_product);
        }

        public Core.Domain.Product GetProduct(int id)
        {
            return _unitOfWork.Products.Get(id);
        }

        public ProductDto GetProductDto(int id)
        {
            return Mapper.Map<Core.Domain.Product, ProductDto>(GetProduct(id));
        }

        public IEnumerable<Core.Domain.Product> GetProducts()
        {
            return _unitOfWork.Products.GetAll();
        }

        public IEnumerable<ProductDto> GetProductDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Product>, IEnumerable<ProductDto>>(GetProducts());
        }

    }
}