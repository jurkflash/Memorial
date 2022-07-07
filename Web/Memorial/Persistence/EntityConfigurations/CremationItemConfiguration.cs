using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CremationItemConfiguration : EntityTypeConfiguration<CremationItem>
    {
        public CremationItemConfiguration()
        {
            Property(ci => ci.Code)
                .HasMaxLength(10);

            HasRequired(ci => ci.Cremation)
                .WithMany(c => c.CremationItems)
                .HasForeignKey(ci => ci.CremationId)
                .WillCascadeOnDelete(false);

            HasRequired(ci => ci.SubProductService)
                .WithMany(c => c.CremationItems)
                .HasForeignKey(ci => ci.SubProductServiceId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}