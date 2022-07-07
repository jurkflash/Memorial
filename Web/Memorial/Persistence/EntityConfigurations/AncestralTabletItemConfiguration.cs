using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletItemConfiguration : EntityTypeConfiguration<AncestralTabletItem>
    {
        public AncestralTabletItemConfiguration()
        {

            Property(ai => ai.Code)
                .HasMaxLength(10);

            HasRequired(ai => ai.AncestralTabletArea)
                .WithMany(aa => aa.AncestralTabletItems)
                .HasForeignKey(ai => ai.AncestralTabletAreaId)
                .WillCascadeOnDelete(false);

            HasRequired(ai => ai.SubProductService)
                .WithMany(aa => aa.AncestralTabletItems)
                .HasForeignKey(ai => ai.SubProductServiceId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}