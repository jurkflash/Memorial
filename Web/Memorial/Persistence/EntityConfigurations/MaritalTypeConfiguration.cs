using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MaritalTypeConfiguration : EntityTypeConfiguration<MaritalType>
    {
        public MaritalTypeConfiguration()
        {
            Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(255);
        }
    }
}