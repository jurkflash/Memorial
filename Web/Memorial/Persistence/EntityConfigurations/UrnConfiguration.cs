using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class UrnConfiguration : EntityTypeConfiguration<Urn>
    {
        public UrnConfiguration()
        {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(u => u.Description)
                .HasMaxLength(255);

            Property(u => u.Remark)
                .HasMaxLength(255);

            HasRequired(s => s.Site)
                .WithMany(c => c.Urns)
                .HasForeignKey(s => s.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}