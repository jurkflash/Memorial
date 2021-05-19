using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumAreaConfiguration : EntityTypeConfiguration<ColumbariumArea>
    {
        public ColumbariumAreaConfiguration()
        {
            Property(qa => qa.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(qa => qa.ColumbariumCentre)
                .WithMany(qc => qc.ColumbariumAreas)
                .HasForeignKey(qa => qa.ColumbariumCentreId)
                .WillCascadeOnDelete(false);
        }
    }
}