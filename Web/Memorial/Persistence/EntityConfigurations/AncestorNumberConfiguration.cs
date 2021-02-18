using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorNumberConfiguration : EntityTypeConfiguration<AncestorNumber>
    {
        public AncestorNumberConfiguration()
        {
            HasRequired(an => an.AncestorItem)
                .WithMany(ai => ai.AncestorNumbers)
                .HasForeignKey(an => an.AncestorItemId)
                .WillCascadeOnDelete(false);
        }
    }
}