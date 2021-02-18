using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleCentreConfiguration : EntityTypeConfiguration<QuadrangleCentre>
    {
        public QuadrangleCentreConfiguration()
        {
            Property(qc => qc.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(qc => qc.Site)
                .WithMany(s => s.QuadrangleCentres)
                .HasForeignKey(qc => qc.SiteId)
                .WillCascadeOnDelete(false);

        }
    }
}