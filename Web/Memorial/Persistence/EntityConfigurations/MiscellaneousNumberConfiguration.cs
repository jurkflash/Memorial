using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MiscellaneousNumberConfiguration : EntityTypeConfiguration<MiscellaneousNumber>
    {
        public MiscellaneousNumberConfiguration()
        {
            HasRequired(mn => mn.MiscellaneousItem)
                .WithMany(mi => mi.MiscellaneousNumbers)
                .HasForeignKey(mn => mn.MiscellaneousItemId)
                .WillCascadeOnDelete(false);
        }
    }
}