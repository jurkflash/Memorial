using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryNumberConfiguration : EntityTypeConfiguration<CemeteryNumber>
    {
        public CemeteryNumberConfiguration()
        {
            Property(pn => pn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}