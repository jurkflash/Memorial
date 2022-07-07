using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumItemConfiguration : EntityTypeConfiguration<ColumbariumItem>
    {
        public ColumbariumItemConfiguration()
        {
            Property(qi => qi.Code)
                .HasMaxLength(10);

            HasRequired(qi => qi.ColumbariumCentre)
                .WithMany(qc => qc.ColumbariumItems)
                .HasForeignKey(qi => qi.ColumbariumCentreId)
                .WillCascadeOnDelete(false);

            HasRequired(qi => qi.SubProductService)
                .WithMany(qc => qc.ColumbariumItems)
                .HasForeignKey(qi => qi.SubProductServiceId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}