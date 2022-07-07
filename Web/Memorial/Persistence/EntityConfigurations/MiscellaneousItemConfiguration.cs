using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MiscellaneousItemConfiguration : EntityTypeConfiguration<MiscellaneousItem>
    {
        public MiscellaneousItemConfiguration()
        {
            Property(mi => mi.Code)
                .HasMaxLength(10);

            HasRequired(mi => mi.Miscellaneous)
                .WithMany(m => m.MiscellaneousItems)
                .HasForeignKey(mi => mi.MiscellaneousId)
                .WillCascadeOnDelete(false);

            HasRequired(mi => mi.SubProductService)
                .WithMany(m => m.MiscellaneousItems)
                .HasForeignKey(mi => mi.SubProductServiceId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}