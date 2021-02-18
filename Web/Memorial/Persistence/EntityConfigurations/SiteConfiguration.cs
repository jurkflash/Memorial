using System.Data.Entity.ModelConfiguration;
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
        }
    }
}