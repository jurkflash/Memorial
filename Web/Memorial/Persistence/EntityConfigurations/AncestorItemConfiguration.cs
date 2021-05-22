using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorItemConfiguration : EntityTypeConfiguration<AncestorItem>
    {
        public AncestorItemConfiguration()
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
                .WithMany(aa => aa.AncestorItems)
                .HasForeignKey(ai => ai.AncestralTabletAreaId)
                .WillCascadeOnDelete(false);
        }
    }
}