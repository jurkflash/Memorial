using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class MiscellaneousConfiguration : EntityTypeConfiguration<Miscellaneous>
    {
        public MiscellaneousConfiguration()
        {
            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(s => s.Remark)
                .HasMaxLength(255);

            HasRequired(m => m.Site)
                .WithMany(s => s.Miscellaneous)
                .HasForeignKey(m => m.SiteId)
                .WillCascadeOnDelete(false);
        }
    }
}