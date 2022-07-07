using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SiteConfiguration : EntityTypeConfiguration<Site>
    {
        public SiteConfiguration()
        {
            Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);

            Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(10);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}