using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class QuadrangleNumberConfiguration : EntityTypeConfiguration<QuadrangleNumber>
    {
        public QuadrangleNumberConfiguration()
        {
            HasRequired(qn => qn.QuadrangleItem)
                .WithMany(qi => qi.QuadrangleNumbers)
                .HasForeignKey(qn => qn.QuadrangleItemId)
                .WillCascadeOnDelete(false);
        }
    }
}