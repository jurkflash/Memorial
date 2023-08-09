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
        Core.Domain.Product Get(int id);
        IEnumerable<Core.Domain.Product> GetAll();
        Core.Domain.Product GetAncestralTabletProduct();
        Core.Domain.Product GetCemeteryProduct();
        Core.Domain.Product GetColumbariumProduct();
        Core.Domain.Product GetCremationProduct();
        Core.Domain.Product GetMiscellaneousProduct();
        Core.Domain.Product GetSpaceProduct();
        Core.Domain.Product GetUrnProduct();
    }
}