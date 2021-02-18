using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleItemConfiguration : EntityTypeConfiguration<QuadrangleItem>
    {
        public QuadrangleItemConfiguration()
        {
            Property(qi => qi.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(qi => qi.Description)
                .HasMaxLength(255);

            Property(qi => qi.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(qi => qi.QuadrangleCentre)
                .WithMany(qc => qc.QuadrangleItems)
                .HasForeignKey(qi => qi.QuadrangleCentreId)
                .WillCascadeOnDelete(false);
        }
    }
}