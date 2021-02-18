using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleTypeConfiguration : EntityTypeConfiguration<QuadrangleType>
    {
        public QuadrangleTypeConfiguration()
        {
            Property(qt => qt.Name)
            .IsRequired()
            .HasMaxLength(255);
        }
    }
}