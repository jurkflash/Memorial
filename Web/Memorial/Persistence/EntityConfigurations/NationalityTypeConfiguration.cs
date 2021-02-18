using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class NationalityTypeConfiguration : EntityTypeConfiguration<NationalityType>
    {
        public NationalityTypeConfiguration()
        {
            Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(255);
        }
    }
}