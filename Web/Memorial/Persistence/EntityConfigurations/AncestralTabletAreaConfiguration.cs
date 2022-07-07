using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletAreaConfiguration : EntityTypeConfiguration<AncestralTabletArea>
    {
        public AncestralTabletAreaConfiguration()
        {
            Property(aa => aa.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(aa => aa.Description)
                .HasMaxLength(255);

            HasRequired(aa => aa.Site)
                .WithMany(a => a.AncestralTabletAreas)
                .HasForeignKey(aa => aa.SiteId)
                .WillCascadeOnDelete(false);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}