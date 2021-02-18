using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class UrnItemConfiguration : EntityTypeConfiguration<UrnItem>
    {
        public UrnItemConfiguration()
        {
            Property(ui => ui.Name)
            .IsRequired()
            .HasMaxLength(255);

            Property(ui => ui.Description)
            .HasMaxLength(255);

            Property(ui => ui.Code)
            .IsRequired()
            .HasMaxLength(10);

            HasRequired(ui => ui.Urn)
                .WithMany(u => u.UrnItems)
                .HasForeignKey(ui => ui.UrnId)
                .WillCascadeOnDelete(false);
        }
    }
}