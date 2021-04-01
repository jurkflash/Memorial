using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorNumberConfiguration : EntityTypeConfiguration<AncestorNumber>
    {
        public AncestorNumberConfiguration()
        {
            Property(an => an.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}