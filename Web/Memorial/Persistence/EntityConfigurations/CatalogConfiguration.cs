using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CatalogConfiguration : EntityTypeConfiguration<Catalog>
    {
        public CatalogConfiguration()
        {
            HasRequired(c => c.Product)
                .WithMany(s => s.Catalogs)
                .HasForeignKey(c => c.ProductId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.Site)
                .WithMany(s => s.Catalogs)
                .HasForeignKey(c => c.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}