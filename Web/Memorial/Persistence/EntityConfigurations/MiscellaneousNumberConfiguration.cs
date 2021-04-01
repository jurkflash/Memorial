using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MiscellaneousNumberConfiguration : EntityTypeConfiguration<MiscellaneousNumber>
    {
        public MiscellaneousNumberConfiguration()
        {
            Property(mn => mn.ItemCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}