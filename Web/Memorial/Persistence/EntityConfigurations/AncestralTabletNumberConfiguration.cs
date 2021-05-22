using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletNumberConfiguration : EntityTypeConfiguration<AncestralTabletNumber>
    {
        public AncestralTabletNumberConfiguration()
        {
            Property(an => an.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}