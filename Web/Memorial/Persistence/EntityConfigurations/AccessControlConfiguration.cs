using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AccessControlConfiguration : EntityTypeConfiguration<AccessControl>
    {
        public AccessControlConfiguration()
        {
            Property(ac => ac.AncestralTablet)
                .IsRequired();

            Property(ac => ac.Cemetery)
                .IsRequired();

            Property(ac => ac.Columbarium)
                .IsRequired();

            Property(ac => ac.Cremation)
                .IsRequired();

            Property(ac => ac.Space)
                .IsRequired();

            Property(ac => ac.Urn)
                .IsRequired();

            Property(ac => ac.Miscellaneous)
                .IsRequired();
        }
    }
}