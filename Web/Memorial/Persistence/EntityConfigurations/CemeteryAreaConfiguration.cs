using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryAreaConfiguration : EntityTypeConfiguration<CemeteryArea>
    {
        public CemeteryAreaConfiguration()
        {
            Property(pa => pa.Name)
                .IsRequired()
                .HasMaxLength(255);

            HasRequired(pa => pa.Site)
                .WithMany(s => s.CemeteryAreas)
                .HasForeignKey(pa => pa.SiteId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}