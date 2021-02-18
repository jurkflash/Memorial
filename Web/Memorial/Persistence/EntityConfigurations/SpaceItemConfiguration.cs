using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SpaceItemConfiguration : EntityTypeConfiguration<SpaceItem>
    {
        public SpaceItemConfiguration()
        {
            Property(si => si.Name)
            .IsRequired()
            .HasMaxLength(255);

            Property(si => si.Description)
            .HasMaxLength(255);

            Property(si => si.Code)
            .IsRequired()
            .HasMaxLength(10);

            HasRequired(si => si.Space)
                .WithMany(s => s.SpaceItems)
                .HasForeignKey(si => si.SpaceId)
                .WillCascadeOnDelete(false);
        }
    }
}