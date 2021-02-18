using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class UrnNumberConfiguration : EntityTypeConfiguration<UrnNumber>
    {
        public UrnNumberConfiguration()
        {
            HasRequired(un => un.UrnItem)
                .WithMany(ui => ui.UrnNumbers)
                .HasForeignKey(un => un.UrnItemId)
                .WillCascadeOnDelete(false);
        }
    }
}