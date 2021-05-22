using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestralTabletItemConfiguration : EntityTypeConfiguration<AncestralTabletItem>
    {
        public AncestralTabletItemConfiguration()
        {
            Property(ai => ai.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(ai => ai.Description)
                .HasMaxLength(255);

            Property(ai => ai.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(ai => ai.AncestralTabletArea)
                .WithMany(aa => aa.AncestralTabletItems)
                .HasForeignKey(ai => ai.AncestralTabletAreaId)
                .WillCascadeOnDelete(false);
        }
    }
}