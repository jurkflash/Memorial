using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SubProductServiceConfiguration : EntityTypeConfiguration<SubProductService>
    {
        public SubProductServiceConfiguration()
        {
            Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(sp => sp.Description)
                .HasMaxLength(255);

            Property(sp => sp.Code)
                .IsRequired()
                .HasMaxLength(10);

            Property(sp => sp.SystemCode)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}