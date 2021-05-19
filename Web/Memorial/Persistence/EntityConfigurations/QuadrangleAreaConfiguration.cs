using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleAreaConfiguration : EntityTypeConfiguration<QuadrangleArea>
    {
        public QuadrangleAreaConfiguration()
        {
            Property(qa => qa.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(qa => qa.ColumbariumCentre)
                .WithMany(qc => qc.QuadrangleAreas)
                .HasForeignKey(qa => qa.ColumbariumCentreId)
                .WillCascadeOnDelete(false);
        }
    }
}