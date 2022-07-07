using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SpaceItemConfiguration : EntityTypeConfiguration<SpaceItem>
    {
        public SpaceItemConfiguration()
        {
            Property(si => si.Code)
            .HasMaxLength(10);

            Property(si => si.FormView)
           .HasMaxLength(255);

            HasRequired(si => si.Space)
                .WithMany(s => s.SpaceItems)
                .HasForeignKey(si => si.SpaceId)
                .WillCascadeOnDelete(false);

            HasRequired(si => si.SubProductService)
                .WithMany(s => s.SpaceItems)
                .HasForeignKey(si => si.SubProductServiceId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}