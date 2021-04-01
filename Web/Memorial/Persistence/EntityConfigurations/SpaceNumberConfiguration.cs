using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SpaceNumberConfiguration : EntityTypeConfiguration<SpaceNumber>
    {
        public SpaceNumberConfiguration()
        {
            Property(sn => sn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}