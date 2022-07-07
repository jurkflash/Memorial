using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumCentreConfiguration : EntityTypeConfiguration<ColumbariumCentre>
    {
        public ColumbariumCentreConfiguration()
        {
            Property(qc => qc.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(qc => qc.Site)
                .WithMany(s => s.ColumbariumCentres)
                .HasForeignKey(qc => qc.SiteId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}