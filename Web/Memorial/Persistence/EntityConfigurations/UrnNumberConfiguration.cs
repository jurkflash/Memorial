using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class UrnNumberConfiguration : EntityTypeConfiguration<UrnNumber>
    {
        public UrnNumberConfiguration()
        {
            Property(un => un.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}