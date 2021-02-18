using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ReligionTypeConfiguration : EntityTypeConfiguration<ReligionType>
    {
        public ReligionTypeConfiguration()
        {
            Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(255);
        }
    }
}