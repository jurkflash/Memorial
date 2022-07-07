using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class UrnItemConfiguration : EntityTypeConfiguration<UrnItem>
    {
        public UrnItemConfiguration()
        {
            Property(ui => ui.Code)
            .HasMaxLength(10);

            HasRequired(ui => ui.Urn)
                .WithMany(u => u.UrnItems)
                .HasForeignKey(ui => ui.UrnId)
                .WillCascadeOnDelete(false);

            HasRequired(ui => ui.SubProductService)
                .WithMany(u => u.UrnItems)
                .HasForeignKey(ui => ui.SubProductServiceId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}