using System.Data.Entity.ModelConfiguration;
using Memorial.Core.Domain;

namespace Memorial.Persistence.EntityConfigurations
{
    public class RelationshipTypeConfiguration : EntityTypeConfiguration<RelationshipType>
    {
        public RelationshipTypeConfiguration()
        {
            Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(255);
        }
    }
}