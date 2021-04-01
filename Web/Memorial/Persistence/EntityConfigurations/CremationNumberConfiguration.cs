using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CremationNumberConfiguration : EntityTypeConfiguration<CremationNumber>
    {
        public CremationNumberConfiguration()
        {
            Property(cn => cn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}