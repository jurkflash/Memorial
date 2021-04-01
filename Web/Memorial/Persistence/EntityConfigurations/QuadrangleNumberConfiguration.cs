using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleNumberConfiguration : EntityTypeConfiguration<QuadrangleNumber>
    {
        public QuadrangleNumberConfiguration()
        {
            Property(qn => qn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}