using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SpaceConfiguration : EntityTypeConfiguration<Space>
    {
        public SpaceConfiguration()
        {
            Property(s => s.Name)
            .IsRequired()
                .HasMaxLength(255);

            Property(s => s.Description)
                .HasMaxLength(255);

            Property(s => s.Remark)
                .HasMaxLength(255);

            HasRequired(s => s.Site)
                .WithMany(s => s.Spaces)
                .HasForeignKey(s => s.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}