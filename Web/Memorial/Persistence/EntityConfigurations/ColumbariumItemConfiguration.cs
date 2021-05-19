using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class ColumbariumItemConfiguration : EntityTypeConfiguration<ColumbariumItem>
    {
        public ColumbariumItemConfiguration()
        {
            Property(qi => qi.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(qi => qi.Description)
                .HasMaxLength(255);

            Property(qi => qi.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(qi => qi.ColumbariumCentre)
                .WithMany(qc => qc.ColumbariumItems)
                .HasForeignKey(qi => qi.ColumbariumCentreId)
                .WillCascadeOnDelete(false);
        }
    }
}