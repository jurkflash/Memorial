using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CremationConfiguration : EntityTypeConfiguration<Cremation>
    {
        public CremationConfiguration()
        {
            Property(s => s.Name)
            .IsRequired()
                .HasMaxLength(255);

            Property(s => s.Description)
                .HasMaxLength(255);

            HasRequired(s => s.Site)
                .WithMany(c => c.Cremations)
                .HasForeignKey(s => s.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}