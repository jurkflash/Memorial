using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CremationItemConfiguration : EntityTypeConfiguration<CremationItem>
    {
        public CremationItemConfiguration()
        {
            Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(ci => ci.Description)
                .HasMaxLength(255);

            Property(ci => ci.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(ci => ci.Cremation)
                .WithMany(c => c.CremationItems)
                .HasForeignKey(ci => ci.CremationId)
                .WillCascadeOnDelete(false);
        }
    }
}