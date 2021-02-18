using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorAreaConfiguration : EntityTypeConfiguration<AncestorArea>
    {
        public AncestorAreaConfiguration()
        {
            Property(aa => aa.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(aa => aa.Description)
                .HasMaxLength(255);

            HasRequired(aa => aa.Site)
                .WithMany(a => a.AncestorAreas)
                .HasForeignKey(aa => aa.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}