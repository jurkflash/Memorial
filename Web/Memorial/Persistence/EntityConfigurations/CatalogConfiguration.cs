using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CatalogConfiguration : EntityTypeConfiguration<Catalog>
    {
        public CatalogConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(c => c.Description)
                .HasMaxLength(255);

            HasRequired(c => c.Site)
                .WithMany(s => s.Catalogs)
                .HasForeignKey(c => c.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}