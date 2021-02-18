using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MiscellaneousItemConfiguration : EntityTypeConfiguration<MiscellaneousItem>
    {
        public MiscellaneousItemConfiguration()
        {
            Property(mi => mi.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(mi => mi.Description)
                .HasMaxLength(255);

            Property(mi => mi.Code)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(mi => mi.Miscellaneous)
                .WithMany(m => m.MiscellaneousItems)
                .HasForeignKey(mi => mi.MiscellaneousId)
                .WillCascadeOnDelete(false);
        }
    }
}