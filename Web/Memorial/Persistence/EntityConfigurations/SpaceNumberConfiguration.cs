using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class SpaceNumberConfiguration : EntityTypeConfiguration<SpaceNumber>
    {
        public SpaceNumberConfiguration()
        {
            HasRequired(sn => sn.SpaceItem)
                .WithMany(si => si.SpaceNumbers)
                .HasForeignKey(sn => sn.SpaceItemId)
                .WillCascadeOnDelete(false);
        }
    }
}