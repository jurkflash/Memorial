using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class NicheTypeConfiguration : EntityTypeConfiguration<NicheType>
    {
        public NicheTypeConfiguration()
        {
            Property(qt => qt.Name)
            .IsRequired()
            .HasMaxLength(255);

            EntityTypeConfigurationExtension.ConfigureAuditColumns(this);
        }
    }
}