using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CemeteryLandscapeCompanyConfiguration : EntityTypeConfiguration<CemeteryLandscapeCompany>
    {
        public CemeteryLandscapeCompanyConfiguration()
        {
            Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(255);

            Property(f => f.ContactPerson)
            .HasMaxLength(255);

            Property(f => f.ContactNumber)
            .HasMaxLength(255);
        }
    }
}