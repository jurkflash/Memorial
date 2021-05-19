using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumNumberConfiguration : EntityTypeConfiguration<ColumbariumNumber>
    {
        public ColumbariumNumberConfiguration()
        {
            Property(qn => qn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}