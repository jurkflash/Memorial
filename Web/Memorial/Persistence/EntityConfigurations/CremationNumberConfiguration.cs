using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class CremationNumberConfiguration : EntityTypeConfiguration<CremationNumber>
    {
        public CremationNumberConfiguration()
        {
            HasRequired(cn => cn.CremationItem)
                .WithMany(ci => ci.CremationNumbers)
                .HasForeignKey(cn => cn.CremationItemId)
                .WillCascadeOnDelete(false);
        }
    }
}