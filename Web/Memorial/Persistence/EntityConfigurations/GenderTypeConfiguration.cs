using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class GenderTypeConfiguration : EntityTypeConfiguration<GenderType>
    {
        public GenderTypeConfiguration()
        {
            Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(255);
        }
    }
}