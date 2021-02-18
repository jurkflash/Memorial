using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class AncestorConfiguration : EntityTypeConfiguration<Ancestor>
    {
        public AncestorConfiguration()
        {
            Property(a => a.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Remark)
                .HasMaxLength(255);

            HasRequired(a => a.AncestorArea)
                .WithMany(aa => aa.Ancestors)
                .HasForeignKey(a => a.AncestorAreaId)
                .WillCascadeOnDelete(false);
        }
    }
}