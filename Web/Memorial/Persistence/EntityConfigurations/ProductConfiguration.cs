using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(p => p.Area)
                .HasMaxLength(255);

            Property(p => p.Controller)
                .HasMaxLength(255);
        }
    }
}